using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Database;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    [Route("EntityFrameworkCoreSqlServer")]
    public class EntityFrameworkCoreSqlServerController : BaseExampleController<ExampleClass>
    {
        private readonly SqlServerDatabaseContext _context = null;

        public EntityFrameworkCoreSqlServerController(SqlServerDatabaseContext exampleDatabaseContext)
        {
            _context = exampleDatabaseContext;
        }

        public override IQueryable<ExampleClass> GetExampleClassQueryable()
        {
            return _context.ExampleClasses.AsQueryable();
        }
    }
}