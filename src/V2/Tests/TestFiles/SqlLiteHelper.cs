using Microsoft.EntityFrameworkCore;

namespace ServiceQuery.Xunit.Integration
{
    public class SqlLiteHelper
    {
        public static IQueryable<TestClass> GetTestList()
        {
            var builder = new DbContextOptionsBuilder<SqlLiteTestContext>();
            builder.UseSqlite("DataSource=testdb;mode=memory");
            var _context = new SqlLiteTestContext(builder.Options);
            _context.Database.OpenConnection();
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
                item.DatabaseKey = 0;
                _context.TestClasses.Add(item);
            }
            _context.SaveChanges();

            return _context.TestClasses.AsQueryable<TestClass>();
        }

        public static IQueryable<TestClass> GetTestNullCopyList()
        {
            var builder = new DbContextOptionsBuilder<SqlLiteTestContext>();
            builder.UseSqlite("DataSource=testdb;mode=memory");
            var _context = new SqlLiteTestContext(builder.Options);
            _context.Database.OpenConnection();
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
                item.DatabaseKey = 0;
                _context.TestClasses.Add(item);
            }
            _context.SaveChanges();

            return _context.TestClasses.AsQueryable<TestClass>();
        }
    }
}