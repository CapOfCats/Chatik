using System;
using Npgsql;

namespace Nabrosok
{
    class Program
    {
        static void Main(string[] args)
             public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

        }
        /// <summary>
        /// Хостбилдер
        /// </summary>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
                    
      }
    }
}
