using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcore3._1
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
            services.AddSwaggerGen();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var key = "ceshikeyaaaaaaabac";
            var byteKey = Encoding.ASCII.GetBytes(key);
            var signKey = new SymmetricSecurityKey(byteKey);

            var validateParamers = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signKey,
                ValidateIssuer = true,
                ValidIssuer = "localhost",
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidAudience = "localhost",
                ClockSkew = TimeSpan.FromSeconds(30),
                RequireExpirationTime = true
            };


            services.AddAuthentication("Bearer").AddJwtBearer(j => j.TokenValidationParameters = validateParamers);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
                //app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseMiddleware<JwtTokenAuth>();


            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
