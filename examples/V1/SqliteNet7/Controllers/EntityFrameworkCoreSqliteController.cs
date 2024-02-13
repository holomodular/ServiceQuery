using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Database;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    [Route("EntityFrameworkCoreSqlite")]
    public class EntityFrameworkCoreSqliteController : BaseExampleController<ExampleClass>
    {
        private readonly SqliteDatabaseContext _context = null;

        public EntityFrameworkCoreSqliteController(SqliteDatabaseContext exampleDatabaseContext)
        {
            _context = exampleDatabaseContext;
        }

        public override IQueryable<ExampleClass> GetExampleClassQueryable()
        {
            return _context.ExampleClasses.AsQueryable();
        }
    }
}