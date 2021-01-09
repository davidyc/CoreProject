using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Models.AppModel
{
    public class UserAdditionalInfo
    {
        public int Id { get; set; }
        public string City { get; set; }
        public List<User> Users { get; set; }
        public UserAdditionalInfo()
        {
            Users = new List<User>();
        }
    }
}
