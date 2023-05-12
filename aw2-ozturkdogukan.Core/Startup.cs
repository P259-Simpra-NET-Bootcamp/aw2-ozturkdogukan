using aw2_ozturkdogukan.Core.RestExtension;
using aw2_ozturkdogukan.Data.Context;
using aw2_ozturkdogukan.DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Builder;

namespace aw2_ozturkdogukan.Core
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
            services.AddControllers();
            services.AddDbContextExtension(Configuration);
            services.AddCustomSwaggerExtension();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DefaultModelsExpandDepth(-1);
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aw2-Ozturkdogukan");
                c.DocumentTitle = "Aw2-Ozturkdogukan";
            });

            app.UseHttpsRedirection();
            // Proje ilk ayağa kalktığında otomatik olarak migration aldıracak kod parçası.
            IServiceScopeFactory serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using IServiceScope serviceScope = serviceScopeFactory.CreateScope();
            Aw2DbContext dbContext = serviceScope.ServiceProvider.GetService<Aw2DbContext>();
            dbContext.Database.EnsureCreated();
            // add auth 
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
