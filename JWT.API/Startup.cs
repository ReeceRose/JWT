using System;
using System.Text;
using AutoMapper;
using FluentValidation.AspNetCore;
using JWT.Application.Users.Commands.RegisterUser;
using JWT.Application.Users.Queries.LoginUser;
using JWT.API.Filters;
using JWT.Common;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace JWT.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioning();

            services.AddScoped<IdentityDbContext, IdentityDbContext>();
            
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("Postgres"),
                    optionsBuilder => { optionsBuilder.MigrationsAssembly("JWT.Persistence"); }));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;
                    options.User.RequireUniqueEmail = true;
                    // I would recommend setting this to true and implementing an email sender
                    options.SignIn.RequireConfirmedEmail = false;
                })
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    //x.RequireHttpsMetadata = true;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("PLACE YOUR KEY HERE")),
                        ValidateIssuer = true,
                        ValidIssuer = "Issuer",
                        ValidateAudience = true,
                        ValidAudience = "Audience"
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdministratorOnly", policy => policy.RequireClaim("Administrator"));

            });
            
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            
            services.AddMvc(
                    options =>
                    {
                        options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                    })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                //TODO: ADD A BASE VALIDATOR CLASS SO WE DON'T DEPEND ON ONE VALIDATOR TO REGISTER ALL VALIDATORS
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<RegisterUserCommandValidator>());

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            services.AddCors();

            services.AddSingleton(mappingConfig.CreateMapper());

            services.AddMediatR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            }
            else
            {
                app.UseHsts();
                app.UseCors(builder => { builder.WithOrigins("https://YOURDOMAIN.com"); });
            }

            UpdateDatabase(app);

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<IdentityDbContext>())
                {
                    context.Database.EnsureCreated();
                    try
                    {
                        context.Database.Migrate();
                    }
                    catch (Exception e)
                    {
                        // Database is already migrated. No issues here
                    }
                    
                }
            }
        }
    }
}
