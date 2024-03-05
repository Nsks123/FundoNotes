using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Fundo_Notes1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //
            var logPath = Path.Combine(Directory.GetCurrentDirectory(), "LogFile");
            NLog.GlobalDiagnosticsContext.Set("myvar",logPath); //set log path along with var name
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(config => {  
                    //configer information
                    config.ClearProviders();
                    config.SetMinimumLevel(LogLevel.Debug);
                }).UseNLog();
    }
}
