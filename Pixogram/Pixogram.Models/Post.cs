using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string PostBody { get; set; }
/*        public string UserName { get; set; }
        public string UserId { get; set; }*/
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<string> Medias { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Like> likes { get; set; }
        public User User { get; set; }
    }
}
