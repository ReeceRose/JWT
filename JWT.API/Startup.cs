using System;
using System.Text;
using AutoMapper;
using FluentValidation.AspNetCore;
using JWT.Application.User.Command.RegisterUser;
using JWT.API.Filters;
using JWT.Common;
using JWT.Infrastructure.Notifications;
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
using Swashbuckle.AspNetCore.Swagger;

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

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<INotificationService, NotificationService>();

            services.AddScoped<IdentityDbContext, IdentityDbContext>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            services.AddSingleton(mappingConfig.CreateMapper());

            services.AddDbContext<IdentityDbContext>(options =>
                options.UseNpgsql(Configuration["ConnectionStrings:Postgres"],
                    optionsBuilder => { optionsBuilder.MigrationsAssembly("JWT.Persistence"); }));
            
            services
                .AddHealthChecks()
                .AddDbContextCheck<IdentityDbContext>();

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = true;
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
                    x.RequireHttpsMetadata = true;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SigningKey"])),
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = Configuration["JWT:Audience"]
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
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                //TODO: ADD A BASE VALIDATOR CLASS SO WE DON'T DEPEND ON ONE VALIDATOR TO REGISTER ALL VALIDATORS
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<RegisterUserCommandValidator>());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "JWT API", Version = "v1" });
            });

            services.AddCors();

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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "JWT API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHealthChecks("/ready");

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
