using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.CustomModels;

namespace WebApplication1.Models.services
{
    public interface ILoginRepository
    {
        Task<User> GetUserByUserNameandPassowrd(string UserName, string Password);
        Task<List<CustomLossTypes>> GetGetLossTypes();
        //Task<List<LossType>> GetGetLossTypes();
    }
}
