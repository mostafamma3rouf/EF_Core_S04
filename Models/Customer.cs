using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_S04.Models
{
    internal class Customer
    {
        public int Id { get; set; } // Primary Key
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalId { get; set; }
        public string CustomerType { get; set; } // Individual or Corporate

        public ICollection<AccountCustomers> accountCustomers { get; set; } = new List<AccountCustomers>(); // Navigation Property, many-to-many relationship with Account
    }
}
