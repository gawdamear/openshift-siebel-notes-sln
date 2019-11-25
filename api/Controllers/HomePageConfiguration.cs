using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    public class HomePageConfiguration
    {
        public string SiebeldB { get; set; }

        [Required]
        public string SiebelUrl { get; set; }
    }
}
