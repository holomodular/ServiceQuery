using Microsoft.EntityFrameworkCore;

namespace ServiceQuery.Xunit
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
            foreach (var item in list)
                _context.TestClasses.Add(item);
            _context.SaveChanges();
            return _context.TestClasses.AsQueryable<TestClass>();
        }
    }
}