using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Models.AppModel
{
    public class MyProject
    {
        public int Id { get; set; }
        [StringLength(30)]  
        [Required(ErrorMessage ="Field name is requered")]
        public string Name { get; set; }
        [StringLength(2000)]
        [Required(ErrorMessage = "Field Decription is requered")]
        public string Decription { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name="Creation Date")]
        public DateTime CreateDate { get; set; }

        public bool External { get; set; }

        public string URL { get; set; }
    }
}
