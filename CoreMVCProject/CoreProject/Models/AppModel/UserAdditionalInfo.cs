using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Models.AppModel
{
    public class UserAdditionalInfo
    {
        [ForeignKey("User")]
        public int Id { get; set; }
        public string City { get; set; }        
      
        public User User { get; set; }
        
    }
}
