using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TesteEditoraLivros.CrossCutting.Ioc;
using AutoMapper;
using TesteEditoraLivros.Application.MapperConfig;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace TesteEditoraLivros
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
            services.AddControllersWithViews();
            //Registra todas as inversões da aplicação
            IocInjector.Register(services);
            //Regitrar todos os profiles que criei para mapear os objetos
            services.AddAutoMapper(x => x.AddProfile(new MapperProfiles()));
            //Adcionei esse serviço para trabalhar com autentiação em formato de cookies
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
            {
                opt.AccessDeniedPath = "/Login/DeniedLogon"; 
                opt.LoginPath = "/Login/Logon";
                opt.LogoutPath = "";
            });
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Book/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            //Criado para usar com o Cookie
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Book}/{action=Books}/{id?}");
            });

            
        }
    }
}
