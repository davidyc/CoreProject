using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Models.ViewModel
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Type Email")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Type password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Type password")]
        public string ConfirmPassword { get; set; }
    }
}
