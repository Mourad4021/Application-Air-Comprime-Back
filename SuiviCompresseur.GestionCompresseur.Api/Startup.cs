using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
//using SuiviCompresseur.Domain.Core.Bus;

using SuiviCompresseur.GestionCompresseur.Data.Context;
//using SuiviCompresseur.GestionCompresseur.Domain.EventHandlers;
//using SuiviCompresseur.GestionCompresseur.Domain.Events;
using SuiviCompresseur.Infra.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace SuiviCompresseur.GestionCompresseur.Api
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
            services.AddDbContext<CompresseurDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("GestionCompresseurDbConnection"));
            }
            );

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            ///Rached

            var key = Encoding.ASCII.GetBytes("Poulina-Auth-Service");

            ///////////////

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    ///////////////////////////////////////////////////////////////
                    ///Rached
                    ///
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,


                    };
                    /////////////////////////////////////////////////////
                    ///

                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Gestion Compresseur Microservice", Version = "v1" });
            });

            services.AddMediatR(typeof(Startup));
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            RegisterServices(services);
        }

        private void RegisterServices(IServiceCollection services)
        {
            DependencyContainer.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //added for file upload directory 
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });



            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestion Compresseur Microservice V1");
            });

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseMvc();

            //ConfigureEventBus(app);
        }

        //private void ConfigureEventBus(IApplicationBuilder app)
        //{
        //    var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
        //    eventBus.Subscribe<CreationDoneEvent, CreationEventHandler>();
        //    eventBus.Subscribe<SendListeCreateEvent, FilialeEventHandler>();
        //}
    }
}
