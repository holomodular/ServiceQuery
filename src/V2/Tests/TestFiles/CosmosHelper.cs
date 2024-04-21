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

            var list = new TestClass().GetDefaultList();
            var templist = _context.TestClasses.ToList();

            // Delete previous records
            if (templist.Count > 0)
            {
                foreach (var item in templist)
                    _context.TestClasses.Remove(item);
                _context.SaveChanges();
            }

            // Add new records
            foreach (var item in list)
            {
                _context.TestClasses.Add(item);
            }
            _context.SaveChanges();

            return _context.TestClasses.AsQueryable<TestClass>();
        }

        public static IQueryable<TestClass> GetTestNullCopyList()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var builder = new DbContextOptionsBuilder<CosmosTestContext>();
            builder.UseCosmos(config.GetConnectionString("CosmosConnectionString"), config.GetConnectionString("CosmosDatabase"));
            var _context = new CosmosTestContext(builder.Options);
            //_context.Database.OpenConnection();
            _context.Database.EnsureCreated();

            var list = new TestClass().GetDefaultList();
            var templist = _context.TestClasses.ToList();

            // Delete previous records
            if (templist.Count > 0)
            {
                foreach (var item in templist)
                    _context.TestClasses.Remove(item);
                _context.SaveChanges();
            }

            // Add new records
            foreach (var item in list)
            {
                item.CopyToNullVals();
                _context.TestClasses.Add(item);
            }
            _context.SaveChanges();

            return _context.TestClasses.AsQueryable<TestClass>();
        }
    }
}