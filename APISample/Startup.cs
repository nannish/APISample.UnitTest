using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APISample.DbEntities;
using APISample.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace APISample
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<SampleDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ISampleDbContext, SampleDbContext>();
            services.AddScoped(typeof(ISampleDbContext), typeof(SampleDbContext));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped(typeof(IUserService), typeof(UserService));

            //services.AddCors(c =>
            //{
            //    c.AddPolicy("AllowLocalhost", options => options.WithOrigins("http://localhost:4200").AllowAnyOrigin()
            //    .AllowAnyHeader()
            //    .AllowAnyMethod()
            //    .AllowCredentials().SetPreflightMaxAge(TimeSpan.FromDays(5)));
            //});

            services.AddCors(o => o.AddPolicy("AllowLocalhost", builder =>
            {
                builder.AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowAnyOrigin()
                       .AllowCredentials();
            }));
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowLocalhost"));
            });
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowLocalhost",
            //    builder => builder
            //    .AllowAnyOrigin()
            //    .AllowAnyHeader()
            //    .AllowAnyMethod()
            //    .AllowCredentials()
            //    .SetPreflightMaxAge(TimeSpan.FromDays(5)));
            //});

            //services.Add(new ServiceDescriptor(typeof(IUserService), typeof(UserService)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseCors(options => options.WithOrigins("http://localhost:4200"));
            app.UseCors("AllowLocalhost");


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<SampleDbContext>();
                context.Database.EnsureCreated();
            }
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
