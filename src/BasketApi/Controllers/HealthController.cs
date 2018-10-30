using Microsoft.AspNetCore.Mvc;

namespace BasketApi.Controllers
{
    /// <summary>
    /// Determines the state of the api instance, for determining instance availability and health
    /// </summary>
    [Route("")]
    public class HealthController : Controller
    {
        /// <summary>
        /// The presence of a "Healthy" response at this endpoint, indicates the api is up
        /// </summary>
        /// <returns>Healthy as static text</returns>
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Healthy";
        }
    }
}