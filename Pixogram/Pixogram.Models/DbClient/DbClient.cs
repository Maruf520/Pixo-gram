using Microsoft.Extensions.Options;
using MongoDB.Bson;
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
        private readonly IMongoCollection<TempData> _temp;

        public DbClient(IOptions<PixoDbConfig> bloodDbconfig)
        {
            var client = new MongoClient();
            var database = client.GetDatabase("PixoDb");
            _user = database.GetCollection<User>("Users");
            _post = database.GetCollection<Post>("Posts");
            _temp = database.GetCollection<TempData>("TempDatas");

        }



        public IMongoCollection<Post> GetPostsCollection()
        {
            return _post;
        }

        public IMongoCollection<TempData> GetTempDataCollection()
        {
            return _temp;
        }

        public IMongoCollection<User> GetUsersCollection()
        {
            return _user;
        }
    }
}
