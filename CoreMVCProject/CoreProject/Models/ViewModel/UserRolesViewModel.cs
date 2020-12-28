using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Models.ViewModel
{
    public class UserRolesViewModel
    {
        public int? UserID { get; set; }
        public string RoleName { get; set; }
        public  bool HasRole{ get; set; }
    }
}
