using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
namespace Localize_Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : Controller
    {
        private readonly IStringLocalizer<AboutController> _localizer;
        public AboutController(IStringLocalizer<AboutController> localizer)
        {
            _localizer = localizer;
        }
        [HttpGet]
        public string GetWay()
        {
            return _localizer["About Title"];
        }
    }
}
