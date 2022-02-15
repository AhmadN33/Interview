using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.CustomModels;

namespace WebApplication1.Models.services
{
    public class LoginRepository : ILoginRepository
    {
        private readonly InsuranceClaimsDbContext _insuranceClaim;
        
        
        public LoginRepository(InsuranceClaimsDbContext InsuranceClaim)
        {
            this._insuranceClaim = InsuranceClaim;
        }

        public async Task<User> GetUserByUserNameandPassowrd(string UserName, string Password)
        {
            try
            {
            var result = await _insuranceClaim.Users.Where(x => x.UserName == UserName & x.Password == Password & x.Active == true).SingleOrDefaultAsync();
            return result;
            }
            catch (Exception ex)
            {
                string ErrorMessageToLog = ex.Message;
                throw new NotImplementedException();
            }
        }

        public async Task<List<CustomLossTypes>> GetGetLossTypes()
        {
            try
            {
                //var result = await _insuranceClaim.LossTypes.Where(x => x.Active == true).ToListAsync();

                var result = await (from p in _insuranceClaim.Users join
                                  q in _insuranceClaim.LossTypes on
                                  p.UserId equals q.LastUpdatedId
                                  where q.Active == true
                                  select new CustomLossTypes
                                  {
                                      LossTypeId= q.LossTypeId,
                                      Active=q.Active,
                                      CreatedDate=q.CreatedDate,
                                      DisplayName=p.DisplayName,
                                      CreatedId=q.CreatedId,
                                      LastUpdatedDate=q.LastUpdatedDate,
                                      LastUpdatedId=q.LastUpdatedId,
                                      LossTypeCode=q.LossTypeCode,
                                      LossTypeDescription=q.LossTypeDescription
                                  }).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                string ErrorMessageToLog = ex.Message;
                throw new NotImplementedException();
            }
        }

    }
}
