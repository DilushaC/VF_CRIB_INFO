using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VF_CRIB_CREDITINFO.Data.Models;

namespace VF_CRIB_CREDITINFO.Business.SearchCRIBHandler
{
    public interface ICRIBService
    {
        Task<string> SearchCRIBData(string numberType, string number, bool isIndividual, string token);
    }
}