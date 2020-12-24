using CoreProject.Models.AppModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Models
{
    public class User
    {
        public int Id { get; set; }
        [StringLength(35)]
        [Required]
        public string Login { get; set; }
        [StringLength(30)]       
        [EmailAddress]
        public string Email { get; set; }    
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        
        [Display(Name = "Born date")]
        public DateTime? DateBorn { get; set; }
        public DateTime? LastLogin { get; set; }

        [StringLength(50)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
}
