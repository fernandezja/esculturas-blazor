using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Esculturas.Core.Business;
using Esculturas.Core.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;

namespace Esculturas.App.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }
        public ICurrentConfiguration CurrentConfiguration { get; private set; }
        public ILifetimeScope AutofacContainer { get; private set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;

            CurrentConfiguration = 
                    Core.Configuration.CurrentConfiguration.Build(configuration, 
                                                                  webHostEnvironment.ContentRootPath);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression();

            services.AddMemoryCache();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.AddSingleton<ICurrentConfiguration>(CurrentConfiguration);

            //services.AddHttpClient();

            services.AddControllers();


        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var logger = new LoggerConfiguration()
                                      .Enrich.WithExceptionDetails()
                                      .ReadFrom.Configuration(Configuration)
                                      .CreateLogger();

            builder.RegisterInstance(logger).As<Serilog.ILogger>();

            builder.RegisterModule(new BusinessModule()
            {
                CurrentConfiguration = CurrentConfiguration
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            //this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
