using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Models
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public string UserProfilePic { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PostId { get; set; }
        public User User { get; set; }
    }
}
