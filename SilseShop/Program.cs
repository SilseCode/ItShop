using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SilseShop.Database;

namespace SilseShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

            CreateHostBuilder(args).Build().Run();
            }
            catch (System.Exception ex)
            {

                System.Console.WriteLine(ex.Message);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
