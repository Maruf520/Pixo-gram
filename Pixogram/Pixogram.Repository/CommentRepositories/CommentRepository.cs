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

namespace Pixogram.Repository.CommentRepositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IMongoCollection<Post> post;
        private readonly IMapper mapper;
        public CommentRepository(IDbClient dbClient, IMapper mapper)
        {
            this.mapper = mapper;
            this.post = dbClient.GetPostsCollection();
        }
        public async Task<Post> CreateAsync(Comment comment)
        {
            var postData = await post.Find(a => a.Id == comment.PostId).FirstOrDefaultAsync();
            if(postData.Comments == null)
            {
                postData.Comments = new List<Comment>();
            }
            postData.Comments.Add(comment);
            post.ReplaceOneAsync(a=>a.Id==comment.PostId,postData);
            //var cmnt = await post.UpdateOneAsync(Builders<Post>.Filter.Eq(x => x.Id, comment.PostId), Builders<Post>.Update.Push(c => c.Comments, comment));
            Post posts = new Post
            {
                UserName = comment.UserName
            };
            return posts;
        }
    }
}
