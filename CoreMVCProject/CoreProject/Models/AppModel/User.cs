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
        [Required]
        public string Email { get; set; }
        [StringLength(50)]
        [Required]
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
}
