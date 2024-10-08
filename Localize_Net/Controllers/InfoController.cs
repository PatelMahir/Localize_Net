using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Localize_Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : Controller
    {
        private readonly IStringLocalizer<InfoController> _localizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public InfoController(IStringLocalizer<InfoController> localizer, 
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _localizer = localizer;
            _sharedLocalizer = sharedLocalizer;
        }
        [HttpGet]
        public string TestLoc()
        {
            string msg = "Shared resx:" + _sharedLocalizer["Hello!"] +
                "Info resx" + _localizer["Hello"];
            return msg;
        }
    }
}