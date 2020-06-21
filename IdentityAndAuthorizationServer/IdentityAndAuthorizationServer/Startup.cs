using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IdentityAndAuthorizationServer.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using IdentityAndAuthorizationServer.SignalR;
using IdentityAndAuthorizationServer.Repositories;
using IdentityAndAuthorizationServer.Filters.ActionFilters;
using IdentityAndAuthorizationServer.Filters.ExceptionFilter;
using IdentityAndAuthorizationServer.RepositoriesInterfaces;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.Reflection;

namespace IdentityAndAuthorizationServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddTransient<IPublicConversationRepostory, PublicConversationRepository>();

            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            services.AddControllers(option =>
            {
                option.Filters.Add(typeof(AddActionLogsFilter));
            })
                .AddDataAnnotationsLocalization(options =>

                 options.DataAnnotationLocalizerProvider = (type, factory) =>
                 {
                     var assemblyName = new AssemblyName(typeof(ApplicationUserModel).GetTypeInfo().Assembly.FullName);
                     return factory.Create("RequestDtos.RequestDtos", assemblyName.Name);
                 }
                );

            services.AddDbContext<AuthenticationContext>(options=> {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            });
            services.AddTransient<ExceptionHandlerFilter>();
            
            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<AuthenticationContext>();

            services.Configure<IdentityOptions>(options=> {
                options.Password.RequiredLength=8;
            });
            services.AddCors(options =>
            {
                options.AddPolicy("angularClient",
                builder =>
                {
                    builder.WithOrigins(Configuration["ApplicationSettings:ClientAngular_URL"].ToString())
                        .AllowAnyHeader()
                         .AllowAnyMethod()
                         .AllowCredentials();
                });
            });

            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret".ToString()]);
            services.AddAuthentication(x=> {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x=> {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,              
                };          
            });
            services.AddSignalR();
            services.AddLocalization(opts => {opts.ResourcesPath = "Resources";});
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] {
                    new CultureInfo("en"),
                    new CultureInfo("pl")
                };
                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRequestLocalization();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("angularClient");
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<PublicChatHub>("/SignalR/PublicChat");
            });
        }
    }
}
