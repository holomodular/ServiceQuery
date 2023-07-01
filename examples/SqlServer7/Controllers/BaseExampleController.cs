using Microsoft.AspNetCore.Mvc;
using ServiceQuery;
using WebApp.Database;
using WebApp.ViewModel.Example;

namespace WebApp.Controllers
{
    public abstract class BaseExampleController<T> : Controller
    {
        public abstract IQueryable<T> GetExampleClassQueryable();

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            ExampleViewModel model = new ExampleViewModel();
            ServiceQueryRequest request = null;
            ServiceQueryResponse<T> response = null;
            IQueryable<T> queryable = GetExampleClassQueryable();

            // Get all records
            request = ServiceQueryRequestBuilder.New().Build();
            response = request.Execute(queryable);

            // Get all records and include the count in the response
            request = ServiceQueryRequestBuilder.New().IncludeCount().Build();
            response = request.Execute(queryable);

            // Get all paging, 2nd page
            request = ServiceQueryRequestBuilder.New().Paging(2, 5, true).Build();
            response = request.Execute(queryable);

            // Get records where name starts with J and IsConfirmed = true
            // sorted by name and only return the Id and Name properties
            // and include the count
            request = ServiceQueryRequestBuilder.New()
                .StartsWith(nameof(ExampleClass.Name), "J")
                .IsEqual(nameof(ExampleClass.IsConfirmed), "true")
                .Sort(nameof(ExampleClass.Name))
                .Select(nameof(ExampleClass.Name), nameof(ExampleClass.Id))
                .IncludeCount()
                .Build();
            response = request.Execute(queryable);

            // Expression building
            request = ServiceQueryRequestBuilder.New()
                .BeginExpression()
                    .IsEqual(nameof(ExampleClass.IsConfirmed), "true")
                    .And()
                    .IsGreaterThan(nameof(ExampleClass.Id), "10")
                .EndExpression()
                .Or()
                .BeginExpression()
                    .IsEqual(nameof(ExampleClass.Id), "1")
                    .Or()
                    .IsEqual(nameof(ExampleClass.Id), "2")
                .EndExpression()
                .Select(nameof(ExampleClass.Id), nameof(ExampleClass.Name))
                .IncludeCount()
                .Build();
            response = request.Execute(queryable);

            return View(model);
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