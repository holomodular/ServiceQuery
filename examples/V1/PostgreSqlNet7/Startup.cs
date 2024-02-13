using WebApp.Extensions;

namespace WebApp
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            if (Configuration == null)
                throw new ArgumentNullException(nameof(Configuration));

            services.AddCustomWebsite(Configuration);
        }

        public static string basedirectorypath;

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.StartCustomWebsite(env);

            basedirectorypath = env.WebRootPath;
            var logger = app.ApplicationServices.GetRequiredService<ILogger<Startup>>();
            logger.LogInformation("Application Started");
        }
    }
}