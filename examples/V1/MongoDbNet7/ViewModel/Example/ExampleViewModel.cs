using ServiceQuery;
using WebApp.Database;

namespace WebApp.ViewModel.Example

{
    public class ExampleViewModel
    {
        public ServiceQueryResponse<ExampleClass> All { get; set; }
    }
}