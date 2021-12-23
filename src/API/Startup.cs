using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;
using Application.Interfaces;
using Application.AppServices;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Contexts;

namespace API
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
            // Banco de dados
            services.AddDbContext<appContext>((option) => option.UseSqlServer("Server=localhost;Database=actv1;User Id=usuario_actv1;Password=usuarioactv1123"));

            // Controladores 
            services.AddControllers();

            //  Serviços de aplicativo
            services.AddTransient<IClienteAppService, ClienteAppService>();
            services.AddTransient<IProdutoAppService, ProdutoAppService>();
            services.AddTransient<IVendaAppService, VendaAppService>();

            // Serviços 
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IProdutoService, ProdutoService>();
            services.AddTransient<IVendaService, VendaService>();

            // Repositórios
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<IVendaRepository, VendaRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
