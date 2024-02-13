namespace WebApp.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public const string APPSETTINGS_FILENAME = "appsettings.json";

        public static IConfigurationBuilder AddAppSettingsConfig(this IConfigurationBuilder builder)
        {
            builder.SetBasePath(AppContext.BaseDirectory);
            builder.AddJsonFile(APPSETTINGS_FILENAME, true);
            string[] fileSplit = APPSETTINGS_FILENAME.Split('.');
            if (fileSplit.Length == 2)
            {
                var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                builder.AddJsonFile($"{fileSplit[0]}.{environmentName}.{fileSplit[1]}", true);
            }
            builder.AddEnvironmentVariables();
            return builder;
        }
    }
}