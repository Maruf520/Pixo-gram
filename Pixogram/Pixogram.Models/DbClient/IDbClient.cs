using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Models.DbClient
{
    public interface IDbClient
    {
        IMongoCollection<User> GetUsersCollection();
    }
}
