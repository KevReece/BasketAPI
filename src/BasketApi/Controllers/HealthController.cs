using Microsoft.AspNetCore.Mvc;

namespace BasketApi.Controllers
{
    [Route("")]
    public class HealthController : Controller
    {
        [HttpGet]
        public ActionResult<string> Get(string id)
        {
            return "Healthy";
        }
    }
}