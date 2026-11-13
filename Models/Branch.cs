using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_S04.Models
{
    internal class Branch
    {
        public string Code { get; set; } // Primary Key
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public Manager manager { get; set; } // Navigation Property, one-to-one relationship with Manager
        public ICollection<Account> accounts { get; set; } = new List<Account>(); // Navigation Properties, one-to-many relationship with Account
    }
}
