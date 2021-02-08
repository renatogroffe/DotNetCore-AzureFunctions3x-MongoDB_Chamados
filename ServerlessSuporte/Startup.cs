using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ServerlessSuporte.Data;
using ServerlessSuporte.Business;

[assembly: FunctionsStartup(typeof(ServerlessSuporte.Startup))]
namespace ServerlessSuporte
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<ChamadoRepository>();
            builder.Services.AddScoped<ChamadoServices>();
        }
    }
}