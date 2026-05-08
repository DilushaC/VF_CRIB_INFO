using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VF_CRIB_CREDITINFO.Data.Models
{
    public class CribSearchResultModel
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public string WorkflowId { get; set; }
        public string WorkflowState { get; set; }
        public string Status { get; set; }

        public string ApplicationNumber { get; set; }
        public string CreditFacilityType { get; set; }
        public string CreditFacilityCurrency { get; set; }
        public decimal CreditFacilityAmount { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string NicNumber { get; set; }
        public string AltNicNumber { get; set; }

        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string DataAvailabilityJson { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
