using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Reflection;
namespace Localize_Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        private readonly IStringLocalizer _localizer;
        private readonly IStringLocalizer _localizer2;
        public TestController(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create(type);
            _localizer2 = factory.Create("Shared resource", assemblyName.Name);
        }
        [HttpGet]
        public IActionResult About()
        {
            ViewData["Message"] = _localizer["Your application description page"] +
                "loc2:" + _localizer2["Your application description page"];
            return View();
        }
    }
}