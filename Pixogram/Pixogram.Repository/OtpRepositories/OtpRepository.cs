using MongoDB.Driver;
using Pixogram.Dtos.OtpDtos;
using Pixogram.Models;
using Pixogram.Models.DbClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Repository.OtpRepositories
{
    public class OtpRepository : IotpRepository
    {
        private readonly IMongoCollection<TempData> tempdata;
        public OtpRepository( IDbClient dbClient)
        {
            this.tempdata = dbClient.GetTempDataCollection();
        }
        public async Task<TempData> CreateOtp(CreateOtpDto createOtpDto)
        {
            TempData temp = new();
            temp.Email = createOtpDto.Email;
            temp.OTP = createOtpDto.OTP;
            await tempdata.InsertOneAsync(temp);
            return temp;
        }

        public async Task<string> RemoveOtp(string email, int Otp)
        {
            var useOtp = await tempdata.Find(x => x.Email == email && x.OTP == Otp).FirstOrDefaultAsync();
            tempdata.DeleteOne(x=>x.Email == email);
            return "Otp Deleted";
        }

        public bool CheckOtpIsExists(string email, int Otp)
        {
            var ifexists = tempdata.Find(x => x.Email == email && x.OTP == Otp).FirstOrDefaultAsync();
            if(ifexists.Result == null)
            {
                return false;
            }
            return true;
        }
    }
}
