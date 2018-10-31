using System;
using BasketApi.Models;
using BasketApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BasketApi.Controllers
{
    /// <summary>
    /// Main basket functionality - Start with a POST to /Basket/ to create a new basket
    /// </summary>
    public class BasketController : Controller
    {
        private readonly BasketService basketService;

        public BasketController(BasketService basketService)
        {
            this.basketService = basketService;
        }

        /// <summary>
        /// Creates a basket, and returns the basket ID for further usage
        /// </summary>
        /// <returns>A new basket ID</returns>
        [HttpPost]
        [Route("Basket")]
        public ActionResult<string> Post()
        {
            return basketService.Create().Id;
        }

        /// <summary>
        /// Gets an existing basket by ID
        /// </summary>
        /// <param name="id">As returned from the initial Basket POST</param>
        /// <returns>The basket or 404 when the ID is unknown</returns>
        [HttpGet]
        [Route("Basket/{id}")]
        public ActionResult<Basket> Get(string id)
        {
            var basket = basketService.Get(id);
            if (basket is null)
            {
                return NotFound();
            }
            return basket;
        }

        /// <summary>
        /// Puts an item in a basket
        /// </summary>
        /// <param name="id">As returned from the initial Basket POST</param>
        /// <param name="itemId">A unique identifier for the item type</param>
        /// <param name="item">The item to save/update</param>
        /// <returns>BadRequest for: unknown basket, invalid item ID, invalid item quantity</returns>
        [HttpPut]
        [Route("Basket/{id}/item/{itemId}")]
        public ActionResult PutItem(string id, string itemId, [FromBody]Item item)
        {
            try
            {
                basketService.AddItem(id, itemId, item);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            return Ok();
        }

        /// <summary>
        /// Deletes an item from a basket
        /// </summary>
        /// <param name="id">As returned from the initial Basket POST</param>
        /// <param name="itemId">A unique identifier for the item type</param>
        /// <returns>NotFound for: unknown basket ID or item ID</returns>
        [HttpDelete]
        [Route("Basket/{id}/item/{itemId}")]
        public ActionResult DeleteItem(string id, string itemId)
        {
            if (basketService.DeleteItem(id, itemId))
            {
                return Ok();
            }
            return NotFound();
        }

        /// <summary>
        /// Delete all items from a basket
        /// </summary>
        /// <param name="id">As returned from the initial Basket POST</param>
        /// <returns>NotFound for: unknown basket ID</returns>
        [HttpDelete]
        [Route("Basket/{id}/item")]
        public ActionResult DeleteItems(string id)
        {
            if (basketService.DeleteAllItems(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}