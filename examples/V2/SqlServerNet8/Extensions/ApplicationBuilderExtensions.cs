using WebApp.Database;

namespace WebApp.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder RegisterMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            return app;
        }

        public static IApplicationBuilder StartCustomWebsite(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            // Do after UseAuth() so user context is available
            app.RegisterMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                });
            }

            // Seed database with data
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                // EntityFrameworkCore SqlServer
                var context = serviceScope.ServiceProvider.GetService<SqlServerDatabaseContext>();
                SeedExampleDatabase.Seed(context);
            }

            return app;
        }
    }
}