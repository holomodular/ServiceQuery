using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ServiceQuery.Xunit
{
    public class CosmosHelper
    {
        public static IQueryable<TestClass> GetTestList()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var builder = new DbContextOptionsBuilder<CosmosTestContext>();
            builder.UseCosmos(config.GetConnectionString("CosmosConnectionString"), config.GetConnectionString("CosmosDatabase"));
            var _context = new CosmosTestContext(builder.Options);
            //_context.Database.OpenConnection();
            _context.Database.EnsureCreated();

            if (_context.TestClasses.ToList().Count == 0)
            {
                var list = new TestClass().GetDefaultList();
                foreach (var item in list)
                {
                    _context.TestClasses.Add(item);
                }
                _context.SaveChanges();
            }
            return _context.TestClasses.AsQueryable<TestClass>();
        }
    }
}