using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Models.DbClient
{
    public class DbClient : IDbClient
    {
        private readonly IMongoCollection<User> _user;
        private readonly IMongoCollection<Post> _post;

        public DbClient(IOptions<PixoDbConfig> bloodDbconfig)
        {
            var client = new MongoClient();
            var database = client.GetDatabase("PixoDb");
            _user = database.GetCollection<User>("Users");
            _post = database.GetCollection<Post>("Posts");


        }

        public IMongoCollection<Post> GetPostsCollection()
        {
            return _post;
        }

        public IMongoCollection<User> GetUsersCollection()
        {
            return _user;
        }
    }
}
