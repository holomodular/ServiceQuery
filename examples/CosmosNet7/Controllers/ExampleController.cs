using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Database;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    [Route("Example")]
    public class ExampleController : BaseExampleController<ExampleClass>
    {
        public override IQueryable<ExampleClass> GetExampleClassQueryable()
        {
            return SeedExampleDatabase.GetExamples().AsQueryable();
        }
    }
}