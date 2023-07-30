using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace SupplyChain
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LogisticaDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews();
            
            // Configurar a geração de PDF sem usar o método AddRotativa
            // Você pode usar outro pacote ou biblioteca para gerar PDFs
            // ou implementar sua própria solução para gerar PDFs
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "selecionarDataExportacaoSaida",
                    pattern: "{controller=Saida}/{action=SelecionarDataExportacaoSaida}");

                endpoints.MapControllerRoute(
                    name: "exportarSaidasPorData",
                    pattern: "{controller=Saida}/{action=ExportarSaidaParaExcel}/{mes}/{ano}");

                endpoints.MapControllerRoute(
                    name: "exportarSaidasParaExcel",
                    pattern: "{controller=Saida}/{action=ExportarSaidasParaExcel}");

                endpoints.MapControllerRoute(
                    name: "selecionarDataExportacaoEntrada",
                    pattern: "{controller=Entrada}/{action=SelecionarDataExportacao}");

                endpoints.MapControllerRoute(
                    name: "exportarEntradasPorData",
                    pattern: "{controller=Entrada}/{action=ExportarEntradasPorData}/{mes}/{ano}");

                endpoints.MapControllerRoute(
                    name: "exportarEntradasParaExcel",
                    pattern: "{controller=Entrada}/{action=ExportarEntradasParaExcel}");

                endpoints.MapControllerRoute(
                    name: "relatorio",
                    pattern: "{controller=Relatorio}/{action=Index}");

                endpoints.MapControllerRoute(
                    name: "exportarEntradasESaidasParaExcel",
                    pattern: "{controller=Relatorio}/{action=ExportarEntradasESaidasParaExcel}");
            });
        }
    }
}

