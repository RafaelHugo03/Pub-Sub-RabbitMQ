using ApiPub.Entity;
using ApiPub.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace ApiPub.Controllers
{
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IMessageBusService messageBusService;

        public ProductController(IMessageBusService messageBusService)
        {
            this.messageBusService = messageBusService;
        }

        [HttpPost("Create")]
        public IActionResult Post([FromBody] Product product)
        {
            try
            {
            messageBusService.Publish(product, routingKey: "Product_Key");
            return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}