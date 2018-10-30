using Microsoft.AspNetCore.Mvc;

namespace BasketApi.Controllers
{
    /// <summary>
    /// Main basket functionality
    /// </summary>
    [Route("Basket/{id}")]
    public class BasketController : Controller
    {
        /// <summary>
        /// Gets some dummy data
        /// </summary>
        /// <param name="id">Currently unused</param>
        /// <returns>DefaultItem as static text</returns>
        [HttpGet]
        public ActionResult<string> Get(string id)
        {
            return "DefaultItem";
        }
    }
}