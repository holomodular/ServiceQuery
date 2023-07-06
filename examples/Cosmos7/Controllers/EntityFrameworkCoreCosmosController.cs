using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Database;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    [Route("EntityFrameworkCoreCosmos")]
    public class EntityFrameworkCoreCosmosController : BaseExampleController<ExampleClass>
    {
        private readonly CosmosDatabaseContext _context = null;

        public EntityFrameworkCoreCosmosController(CosmosDatabaseContext exampleDatabaseContext)
        {
            _context = exampleDatabaseContext;
        }

        public override IQueryable<ExampleClass> GetExampleClassQueryable()
        {
            return _context.ExampleClasses.AsQueryable();
        }
    }
}