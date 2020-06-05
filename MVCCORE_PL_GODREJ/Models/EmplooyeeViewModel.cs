using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCORE_PL_GODREJ.Models
{
    // STEP1: CREATE MODEL CLASS - Employee
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }

        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }

        [DisplayName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DisplayName("Email Address")]
        public string Email { get; set; }


        public string Photo { get; set; }
    }
}
