using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCORE_PL_GODREJ.Models
{
    public class AdminViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        //[RegularExpression(pattern: @"^[A-Z][a-z]+.[0-9]+$",
        //    ErrorMessage = "Invalid Password :- E.g: Admin@123")]

        public string Password { get; set; }
    }
}
