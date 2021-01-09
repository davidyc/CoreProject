using CoreProject.Models.Services.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Models.ViewModel
{
    public class UserAccountViewModel
    {
        public User User { get; set; }
        public Weather Weather { get; set; }

    }
}
