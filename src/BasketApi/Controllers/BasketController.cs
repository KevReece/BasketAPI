using Microsoft.AspNetCore.Mvc;

namespace BasketApi.Controllers
{
    [Route("Basket/{id}")]
    public class BasketController : Controller
    {
        [HttpGet]
        public ActionResult<string> Get(string id)
        {
            return "DefaultItem";
        }
    }
}