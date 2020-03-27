using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encyption;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI
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
            // Dependency Inversion burda da set edilebilir. Daha doðru olan ise Dependency yönetimini AutoFac ile business tarafýnda yapmaktýr.
            // Autofac'in yapýlandýrýlmasýnda apiye .net core 3.0 dan önce burada belirtiyorduk ama artýk Program.cs içerisinde belirtiliyor.
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:3000")); // Örneðin bir react uygulamasýnýn yayýn adresi. Birden fazla ise ',' ile ayýrýr yazarsýn.
            }); // Artýk Jsonwt için middlewear eklenmesi gerekir.

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>(); // appsettingsdeki TokenOptions bilgileri ile TokenOptions(core) nesnesi oluþturduk. 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Konfigürasyon bilgileri;
                    ValidateIssuer = true, // tokenda ýssuer bilgisi olsun. (www.yasin.com)
                    ValidateAudience = true,
                    ValidateLifetime = true, // token'ýn yaþam süresi olsun mu ? Evet olsun dedik.
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    ValidateIssuerSigningKey = true, // anahtar kontrol edilsin mi ? Evet edilsin.
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey) // Signing key ayarý yapýldý.
                };
            });
            services.AddDependencyResolvers(new ICoreModule[]
            {
                new CoreModule() // baþka bir core module olan yapýlarým olursa onlarý da ekleyebilirim.
            }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.WithOrigins("http://localhost:3000").AllowAnyHeader()); // MiddleWear iþlemi / Gelen her istegi kabul et.
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); // Bir anahtar.

            app.UseAuthorization(); // Bir iþlemi yapabilmek için yetki.

            
            
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
