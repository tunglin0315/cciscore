using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;

namespace CCISWebCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .ConfigureLogging((builder, logging) =>
            {
                //開發環境使用不同的nlog設定
                string _appset = builder.HostingEnvironment.EnvironmentName.Equals("Development") ?
                                "appsettings.Development.json" : "appsettings.json";
                var config = new ConfigurationBuilder()
                   .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                   .AddJsonFile(_appset, optional: true, reloadOnChange: true)
                   .Build();
                LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));

                //logging.ClearProviders();               
                //NLog.LogManager.LoadConfiguration($"nlog.config");
            }).UseNLog()
            .UseDefaultServiceProvider(options =>
            options.ValidateScopes = false);
    }
}