using Azure.Data.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Database;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    [Route("AzureDataTables")]
    public class AzureDataTablesController : Controller
    {
        private readonly AzureDataTablesDatabaseContext _context = null;

        public AzureDataTablesController(AzureDataTablesDatabaseContext exampleDatabaseContext)
        {
            _context = exampleDatabaseContext;
        }

        [HttpGet]
        [Route("Api")]
        public IActionResult Api()
        {
            return View();
        }

        [HttpGet]
        [Route("Mapping")]
        public IActionResult Mapping()
        {
            return View();
        }
    }
}