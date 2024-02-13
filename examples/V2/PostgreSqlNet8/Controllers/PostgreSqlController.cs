using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Database;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    [Route("PostgreSql")]
    public class PostgreSqlController : BaseExampleController<ExampleClass>
    {
        private readonly PostgreSqlDatabaseContext _context = null;

        public PostgreSqlController(PostgreSqlDatabaseContext exampleDatabaseContext)
        {
            _context = exampleDatabaseContext;
        }

        public override IQueryable<ExampleClass> GetExampleClassQueryable()
        {
            return _context.ExampleClasses.AsQueryable();
        }
    }
}