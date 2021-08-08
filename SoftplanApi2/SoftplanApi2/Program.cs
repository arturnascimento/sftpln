using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using SoftplanApi2.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace SoftplanApi2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://localhost:44398/api/taxaJuros");
            var content = await response.Content.ReadAsStringAsync();
            var api1return = JsonConvert.DeserializeObject<Juros>(content);

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
