using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using Pixogram.Dtos.CommentDtos;
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
        public async Task<Post> CreateAsync(Comment comment, string postid)
        {
            var postData = await post.Find(a => a.Id == postid).FirstOrDefaultAsync();
            if(postData.Comments == null)
            {
                postData.Comments = new List<Comment>();
            }
            postData.Comments.Add(comment);
            await post.ReplaceOneAsync(a=>a.Id==postid,postData);
            //var cmnt = await post.UpdateOneAsync(Builders<Post>.Filter.Eq(x => x.Id, comment.PostId), Builders<Post>.Update.Push(c => c.Comments, comment));
            Post posts = new Post
            {
               
            };
            return posts;
        }

        public async Task<List<Comment>> GetById(string postid)
        {
            List<Comment> comments = new();
            var postComment = await post.Find(x => x.Id == postid).FirstOrDefaultAsync();
            comments = postComment.Comments.ToList();
            return comments;

        }


    }
}
