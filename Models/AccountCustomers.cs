using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_S04.Models
{
    internal class AccountCustomers
    {
        // Composite Key
        public string AccountNumber { get; set; }
        public Account account { get; set; }
        public int CustomerId { get; set; }
        public Customer customer { get; set; }

        // Attributes of the relationship
        public DateTime OwnerShipStartDate { get; set; }
        public string OwnerShipType { get; set; }
        public string AccountStatus { get; set; }
    }
}
