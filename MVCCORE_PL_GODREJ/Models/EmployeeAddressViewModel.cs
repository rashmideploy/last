using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCORE_PL_GODREJ.Models
{
    // STEP1: CREATE MODEL CLASS - EmployeeAddress
    public class EmployeeAddressViewModel
    {
        [ForeignKey("Employee")]
        public int EmployeeAddressId { get; set; }
        [Column(TypeName = "varchar(60)")]
        [Required]
        public string Address { get; set; }
        [Column(TypeName = "varchar(20)")]
        [Required]
        public string City { get; set; }
        [Column(TypeName = "varchar(20)")]
        [Required]
        public string District { get; set; }
        [Column(TypeName = "varchar(20)")]
        [Required]
        public string State { get; set; }
        [Column(TypeName = "varchar(20)")]
        [Required]
        public string Country { get; set; }
        [Column(TypeName = "varchar(6)")]
        [Required]
        public string Pincode { get; set; }

        public virtual EmployeeViewModel Employee { get; set; }

    }
}
