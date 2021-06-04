using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using Pixogram.Models;
using Pixogram.Models.DbClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Repository.LikeRepositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly IMongoCollection<Post> post;
        private readonly IMapper mapper;

        public LikeRepository(IDbClient dbClient, IMapper mapper)
        {
            this.mapper = mapper;
            this.post = dbClient.GetPostsCollection();

        }
        public async Task<ServiceResponse<string>> CreateAsync(Like like, string postid)
        {
            ServiceResponse<string> response = new();
            var userPOst = await post.Find(x => x.Id == postid).FirstOrDefaultAsync();
            var ifUserPost =  userPOst.likes.Where(m => m.User.Id == like.User.Id).FirstOrDefault();
            if(ifUserPost != null)
            {
               var unlike = Builders<Post>.Update.Pull(x => x.likes, like);
                post.UpdateOne(x => x.Id == postid, unlike);
                response.SuccessCode = 200;
                response.Success = true;
                response.Message = "Unliked";
                return response;
            }
            var likeTocreate = Builders<Post>.Update.Push(x => x.likes, like);
            post.UpdateOne(x => x.Id == postid, likeTocreate);
            response.SuccessCode = 200;
            response.Success = true;
            response.Message = "Liked";
            return response;


        }

        public async Task<bool> GetById(string userId, string postId)
        {
            var userPOst = await post.Find(x => x.Id == postId).SingleOrDefaultAsync();
          List<Like> ls = new();
            /*var z = userPOst.likes.Any(x => x.Id == userId);*/
            var w = userPOst.likes.Find(x =>x.Id == userId);
            /*List<Like> lk = new List<Like> { new Like { } };*/
            var u = Builders<Post>.Update.Pull(x=>x.likes,w);
/*            var findpost = Builders<BsonDocument>.Filter.Eq(x=>x.Id, postId) & Builders<Post>.Filter.Eq<Post>(x=>x.likes )
            var filter  = Builders<BsonDocument>.Filter.Eq(x =>x.)*/
            
            var asp = Builders<Post>.Filter;
           
            var asp1 = asp.Eq(x => x.Id, postId) & asp.ElemMatch(doc => doc.likes, el => el.User.Id == userId);
            
            
            if(asp1 == null)
            {
                return true;
            }
            return false;
        }
    }
}
