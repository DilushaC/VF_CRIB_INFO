using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VF_CRIB_CREDITINFO.Business.CRIBHandler
{
    public interface ITokenService
    {
        Task<string> GetTokenDataAsync(string url, string username, string password);
    }
}
