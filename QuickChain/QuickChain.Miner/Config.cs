namespace QuickChain.Miner
{
    using System;
    using System.IO;
    using Microsoft.Extensions.Configuration;
    
    public class Config
    {
        public static IConfiguration Configuration { get; set; }

        public static void Conf()
        {
            var bulder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json");

            Configuration = bulder.Build();


        }
    }
}
