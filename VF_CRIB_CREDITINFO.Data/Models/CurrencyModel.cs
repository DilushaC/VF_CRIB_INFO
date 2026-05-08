using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VF_CRIB_CREDITINFO.Data.Models
{
    public class CurrencyModel
    {
        public int Id { get; set; }
        public string Currency { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
