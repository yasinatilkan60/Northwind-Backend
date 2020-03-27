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
            // Dependency Inversion burda da set edilebilir. Daha do�ru olan ise Dependency y�netimini AutoFac ile business taraf�nda yapmakt�r.
            // Autofac'in yap�land�r�lmas�nda apiye .net core 3.0 dan �nce burada belirtiyorduk ama art�k Program.cs i�erisinde belirtiliyor.
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:3000")); // �rne�in bir react uygulamas�n�n yay�n adresi. Birden fazla ise ',' ile ay�r�r yazars�n.
            }); // Art�k Jsonwt i�in middlewear eklenmesi gerekir.

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>(); // appsettingsdeki TokenOptions bilgileri ile TokenOptions(core) nesnesi olu�turduk. 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Konfig�rasyon bilgileri;
                    ValidateIssuer = true, // tokenda �ssuer bilgisi olsun. (www.yasin.com)
                    ValidateAudience = true,
                    ValidateLifetime = true, // token'�n ya�am s�resi olsun mu ? Evet olsun dedik.
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    ValidateIssuerSigningKey = true, // anahtar kontrol edilsin mi ? Evet edilsin.
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey) // Signing key ayar� yap�ld�.
                };
            });
            services.AddDependencyResolvers(new ICoreModule[]
            {
                new CoreModule() // ba�ka bir core module olan yap�lar�m olursa onlar� da ekleyebilirim.
            }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.WithOrigins("http://localhost:3000").AllowAnyHeader()); // MiddleWear i�lemi / Gelen her istegi kabul et.
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); // Bir anahtar.

            app.UseAuthorization(); // Bir i�lemi yapabilmek i�in yetki.

            
            
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
