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
        public async Task<Like> CreateAsync(Like like)
        {
            var userPost = await post.Find(x => x.Id == like.PostId).FirstOrDefaultAsync();
            if(userPost.likes == null)
            {
                userPost.likes = new List<Like>();
            }
            userPost.likes.Add(like);
            post.ReplaceOne(x => x.Id == like.PostId, userPost);
            return like;
        }

        public async Task<bool> GetById(string userId, string postId)
        {
            var userPOst = await post.Find(x => x.Id == postId).SingleOrDefaultAsync();
/*            var findpost = Builders<BsonDocument>.Filter.Eq(x=>x.Id, postId) & Builders<Post>.Filter.Eq<Post>(x=>x.likes )
            var filter  = Builders<BsonDocument>.Filter.Eq(x =>x.)*/

            var asp = Builders<Post>.Filter;
            var asp1 = asp.Eq(x => x.Id, postId);
            var x = asp.ElemMatch(doc => doc.likes, el => el.UserId == userId);
            if(x == null)
            {
                return true;
            }
            return false;
        }
    }
}
