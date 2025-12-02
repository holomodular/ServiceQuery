using Microsoft.Extensions.Configuration;

namespace ServiceQuery.Xunit.Integration
{
    public class AzureDataTablesHelper
    {
        public static string GetConnectionString()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return config.GetConnectionString("AzureDataTables");
        }

        public static IQueryable<AzureDataTablesTestClass> GetTestList()
        {
            return null;
        }
    }
}