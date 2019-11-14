using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        public string Get()
        {
            return "Recieving!";
        }
    }
}
