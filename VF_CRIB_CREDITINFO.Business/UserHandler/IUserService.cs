using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VF_CRIB_CREDITINFO.Data.Models;
using VF_CRIB_CREDITINFO.Data.Models;

namespace VF_CRIB_CREDITINFO.Business.UserHandler
{
    public interface IUserService
    {
        Task<UserModel> ValidateUserAsync(string username, string password);

    }
}
