using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace CancunHotel.Api
{
    /// <summary>
    /// Point d'entrée
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Point d'entrée
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Création du host builder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
