using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Database;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    [Route("EntityFrameworkCoreInMemory")]
    public class EntityFrameworkCoreInMemoryController : BaseExampleController<ExampleClass>
    {
        private readonly InMemoryDatabaseContext _context = null;

        public EntityFrameworkCoreInMemoryController(InMemoryDatabaseContext exampleDatabaseContext)
        {
            _context = exampleDatabaseContext;
        }

        public override IQueryable<ExampleClass> GetExampleClassQueryable()
        {
            return _context.ExampleClasses.AsQueryable();
        }
    }
}