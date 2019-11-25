using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        //public IConfigurationRoot configurationRoot;
        //private IConfiguration config { get; }
        //private readonly IConfiguration _configuration;
        private HomePageConfiguration _homePageConfiguration;
        //private readonly IOptionsSnapshot<HomePageConfiguration> _homePageConfiguration;

        public NotesController(IOptionsSnapshot<HomePageConfiguration> options)
        //public NotesController(IOptionsMonitor<HomePageConfiguration> options)
        {
            _homePageConfiguration = options.Value;
            //_homePageConfiguration = options.CurrentValue;
            /*options.OnChange(config =>
            {
                _homePageConfiguration = config;
                //log that config has changed
            });*/
        }

        public string Get()
        {
            return _homePageConfiguration.SiebelUrl;

            

            //return "Recieving Messages!";
            //var configurationBuilder = new ConfigurationBuilder();
            /*var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            configurationRoot = configurationBuilder.Build();
            return configurationRoot.GetValue<string>("Message");*/
            /*
            string message = _configuration.GetValue<string>("Features:HomePage:SiebeldB");
            // OR
            var configurationSection = _configuration.GetSection("Features:HomePage");
            message = _configuration.GetValue<string>("SiebeldB");
            message = configurationSection["SiebeldB"];
            // OR
            
            return message;
            //return config["Message"];*/
        }
    }
}
