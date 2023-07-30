using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SupplyChain;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseUrls("http://localhost:5009", "https://localhost:5001");
                webBuilder.UseStartup<Startup>();
            });
}


