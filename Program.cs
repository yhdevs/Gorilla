using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace TuvaletBekcisi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

var db = new DataAccess();

// var users = db.GetUsers();

// var us = db.GetUser(new Guid("c6b9252d-306d-4144-a360-47e7804c62be"));

var create = db.Create(new User{
UserId = new Guid("d1b441b8-27bf-44f4-8ee3-aeccbcdf9972"),
SystemId = "15",
UserName = "bla"

});


            host.Run();
        }
    }
}
