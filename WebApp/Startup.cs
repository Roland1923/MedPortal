using System;
using System.IO;
using Core.Entities;
using Core.IRepositories;
using FluentValidation;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Swashbuckle.AspNetCore.Swagger;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebApp.Common;
using WebApp.Models;
using WebApp.Models.Validations;

namespace WebApp
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
          services.AddAuthentication().AddJwtBearer(cfg =>
          {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;

            cfg.TokenValidationParameters = new TokenValidationParameters()
            {
              IssuerSigningKey = TokenAuthOption.Key,
              ValidAudience = TokenAuthOption.Audience,
              ValidIssuer = TokenAuthOption.Issuer,
              // When receiving a token, check that we've signed it.
              ValidateIssuerSigningKey = true,
              // When receiving a token, check that it is still valid.
              ValidateLifetime = true,
              // This defines the maximum allowable clock skew - i.e. provides a tolerance on the token expiry time 
              // when validating the lifetime. As we're creating the tokens locally and validating them on the same 
              // machines which should have synchronised time, this can be set to zero. and default value will be 5minutes
              ClockSkew = TimeSpan.FromMinutes(0)
            };

          });
            services.AddTransient<IDatabaseService, DatabaseService>();
            services.AddTransient<IEditableRepository<Patient>, PatientRepository>();
            services.AddTransient<IEditableRepository<Doctor>, DoctorRepository>();
            services.AddTransient<IEditableRepository<PatientHistory>, PatientHistoryRepository>();
            services.AddTransient<IEditableRepository<Appointment>, AppointmentRepository>();
            services.AddTransient<IEditableRepository<Feedback>, FeedbackRepository>();
            services.AddTransient<IEditableRepository<BloodDonor>, BloodDonorRepository>();

            


            //services.AddDbContext<DatabaseService>(opts => opts.UseInMemoryDatabase("MedPortal"));
            //var connection = @"Server = .\SQLEXPRESS; Database = MedPortal; Trusted_Connection = true;";
            var connection = Configuration.GetSection("ConnectionStrings:DefaultConnection");
            services.AddDbContext<DatabaseService>(option => option.UseSqlServer(connection.Value));

            services.AddMvc().AddFluentValidation(fv => { }).AddJsonOptions(options => {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            });


            services.AddTransient<IValidator<AppointmentModel>, AppointmentValidator>();
            services.AddTransient<IValidator<BloodDonorModel>, BloodDonorValidator>();
            services.AddTransient<IValidator<DoctorModel>, DoctorValidator>();
            services.AddTransient<IValidator<PatientModel>, PatientValidator>();
            services.AddTransient<IValidator<FeedbackModel>, FeedbackValidator>();
            services.AddTransient<IValidator<PatientHistoryModel>, PatientHistoryValidator>();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "MedPortal API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            IAntiforgery antiforgery)
        { 

            //Manually handle setting XSRF cookie. Needed because HttpOnly has to be set to false so that
            //Angular is able to read/access the cookie.
            app.Use((context, next) => {
                if (context.Request.Method == HttpMethods.Get &&
                    (string.Equals(context.Request.Path.Value, "/", StringComparison.OrdinalIgnoreCase) ||
                     string.Equals(context.Request.Path.Value, "/home/index", StringComparison.OrdinalIgnoreCase)))
                {
                    var tokens = antiforgery.GetAndStoreTokens(context);
                    context.Response.Cookies.Append("XSRF-TOKEN",
                        tokens.RequestToken,
                        new CookieOptions { HttpOnly = false });
                }

                return next();
            });

            // Serve wwwroot as root
            app.UseFileServer();

            // Serve /node_modules as a separate root (for packages that use other npm modules client side)
            // Added for convenience for those who don't want to worry about running 'gulp copy:libs'
            // Only use in development mode!!
            app.UseFileServer(new FileServerOptions
            {
                // Set root of file server
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules")),
                RequestPath = "/node_modules",
                EnableDirectoryBrowsing = false
            });

            //This would need to be locked down as needed (very open right now)
            app.UseCors(corsPolicyBuilder => {
                corsPolicyBuilder.AllowAnyOrigin();
                corsPolicyBuilder.AllowAnyMethod();
                corsPolicyBuilder.AllowAnyHeader();
                corsPolicyBuilder.WithExposedHeaders("X-InlineCount");
            });

            app.UseStaticFiles();


            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            // Visit http://localhost:port/swagger
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });

            });
        }
    }
}
