using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using eCommerce.Ioc.ServiceCollectionExtensions;
using eCommerce.Web.Api.StartupConfiguration;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace eCommerce.Web.Api
{
    public class Startup
    {
        #region Properties
        public IConfiguration Configuration { get; }
        public string MyConnectionString
        {
            get
            {
                string myConn = Configuration.GetConnectionString("MyConnectionString");
                return myConn;
            }
        }
        #endregion

        #region Constructors
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region Methods
        public void ConfigureServices(IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes("SecureKeyRequiredforvalidationAdmin");

            services.AddMemoryCache();
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer("Bearer", options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddDbContextMyDatabase(MyConnectionString);
            services.AddAutoMapper(typeof(Startup));
            services.AddOptions();
            services.AddLocalization();
            services.AddSwaggerGen();
            services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            });

            services.ConfigureCors();
            services.ConfigureDependencyInjectioneCommerce();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureLocalization();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint(SwaggerConfiguration.EndpointSwaggerJson, SwaggerConfiguration.NameApiSwagger));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(SecurityConfiguration.CorsPolicyName);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        #endregion
    }
}