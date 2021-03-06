using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
/*        public string Phone { get; set; }*/
        public string UserProfileImage { get; set; }
        public string UserFullName { get; set; }
        /*public DateTime DateOfBirth { get; set; }*/
        public string Password { get; set; }
    }
}
