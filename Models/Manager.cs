using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_S04.Models
{
    internal class Manager
    {
        public int Id { get; set; } // Primary Key
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }

        // one-to-one relationship with Branch
        public string branchCode { get; set; } // Foreign Key
        public Branch branch { get; set; } // Navigation Property
    }
}
