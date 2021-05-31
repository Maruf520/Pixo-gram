using AutoMapper;
using MongoDB.Driver;
using Pixogram.Dtos.PostDtos;
using Pixogram.Models;
using Pixogram.Models.DbClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Repository.PostsRepository
{
    public class PostRepository : IPostRepository
    {
        private readonly IMongoCollection<Post> post;
        private readonly IMapper mapper;
        public PostRepository(IDbClient dbClient, IMapper mapper)
        {
            this.mapper = mapper;
            this.post = dbClient.GetPostsCollection();
        }
        public GetPostDto Create(Post posts)
        {
            var createPost = post.InsertOneAsync(posts);

            var createdPost = mapper.Map<GetPostDto>(posts);

            return createdPost;
        }

        public List<Post> GetAll()
        {
            List<Post> posts = new List<Post>();

            posts = post.Find(x => true).ToList();
            return posts;
        }

        public async Task<Post> GetbyId(string id)
        {
            var postById = await post.Find(x => x.Id == id).FirstOrDefaultAsync();
            return postById;
        }
    }
}
