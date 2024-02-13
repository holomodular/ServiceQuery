using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Database;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    [Route("MongoDb")]
    public class MongoDbController : BaseExampleController<ExampleClass>
    {
        private readonly MongoDbDatabaseContext _context = null;

        public MongoDbController(MongoDbDatabaseContext exampleDatabaseContext)
        {
            _context = exampleDatabaseContext;
        }

        public override IQueryable<ExampleClass> GetExampleClassQueryable()
        {
            return _context.ExampleClassesAsQueryable();
        }
    }
}