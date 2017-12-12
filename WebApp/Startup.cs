using Core.Entities;
using Core.IRepositories;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

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

            services.AddMvc();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "MedPortal API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Too V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
