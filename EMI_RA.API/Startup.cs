using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.HttpsPolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace EMI_RA.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EMI_RA.API", Version = "v1" });
            });

            services.AddSingleton(typeof(IProduitsService), new ProduitsServices());
            services.AddSingleton(typeof(IAssoProduitsFournisseursServices), new AssoProduitsFournisseursServices());
            services.AddSingleton(typeof(IFournisseursService), new FournisseursService());
            services.AddSingleton(typeof(ILignesPaniersGlobauxService), new LignesPaniersGlobauxService());
            services.AddSingleton(typeof(IOffresService), new OffresService());
            services.AddSingleton(typeof(IPaniersGlobauxService), new PaniersGlobauxService());
            services.AddSingleton(typeof(IAdherentsService), new AdherentsService());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EMI_RA.API v1"));
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
