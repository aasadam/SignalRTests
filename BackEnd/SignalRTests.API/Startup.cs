using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SignalRTests.API.Filters;
using SignalRTests.API.Hubs;

namespace SignalRTests.API
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
            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(pol =>
                                        {
                                            pol.WithOrigins("http://localhost:3000")
                                                //.AllowAnyOrigin()
                                               .AllowAnyHeader()
                                               .AllowAnyMethod()
                                               .AllowCredentials();
                                        });
            });

            services.AddControllers();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("LoggedUser", policy => policy.Requirements.Add(new LoggedUserRequirement()));
                options.AddPolicy("LoggedUserWithCity", policy => policy.Requirements.Add(new LoggedUserWithCityRequirement()));
                options.AddPolicy("LoggedUserWithCityHub", policy => policy.Requirements.Add(new LoggedUserWithCityHubRequirement()));
            });
            services.AddTransient<IAuthorizationHandler, LoggedUserHandler>();
            services.AddTransient<IAuthorizationHandler, LoggedUserWithCityHandler>();
            services.AddTransient<IAuthorizationHandler, LoggedUserWithCityHubHandler>();

            services.AddScoped<LoggedUser>();

            var key = Encoding.ASCII.GetBytes(Configuration["TokenConfig:Key"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                //Not Necessary
                //x.Events = new JwtBearerEvents
                //{
                //    OnMessageReceived = context =>
                //    {
                //        var token = context.Request.Query["token"];
                //        if (!string.IsNullOrEmpty(token))
                //            context.Token = token;


                //        return Task.CompletedTask;
                //    }
                //};
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Api Client",
                    Version = "v1",
                });
                c.AddSecurityDefinition("Bearer"
                    , new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                        Description = "Token",
                        Name = "Authorization",
                        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
                    });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            services.AddLogging(opt =>
            {
                opt.AddConsole();
            });

            services.AddSignalR();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api/swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "Ver. 1");
                c.RoutePrefix = "api/help";
            });

            app.UseHttpsRedirection();


            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<WeatherNowHub>("/api/WeatherNow");
                endpoints.MapHub<WeatherNowByCityHub>("/api/WeatherNowByCity");
            });
        }
    }
}
