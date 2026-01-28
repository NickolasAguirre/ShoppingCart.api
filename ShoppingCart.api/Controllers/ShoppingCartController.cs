using Microsoft.AspNetCore.Mvc;
using ShoppingCart.application.Dtos.Product;
using ShoppingCart.application.UseCase.ShoppingCart;

namespace ShoppingCart.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartUseCase _shoppingCartUseCase;
        public ShoppingCartController(IShoppingCartUseCase shoppingCartUseCase)
        {
            _shoppingCartUseCase = shoppingCartUseCase;
        }

        [HttpPost]
        [Route("AddProduct")]
        public ActionResult AddProduct([FromBody] ProductRequestDto productRequest, [FromQuery] int? cartId = null)
        {
            var result = _shoppingCartUseCase.AddProduct(productRequest, cartId);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateProduct/{cartId}")]
        public ActionResult UpdateProduct([FromBody] ProductUpdatedDto productUpdated, [FromRoute] int cartId)
        {
            var result = _shoppingCartUseCase.UpdateProduct(productUpdated, cartId);
            return Ok(result);
        }

        [HttpPatch]
        [Route("UpdateQuantityProduct/{cartId}")]
        public ActionResult UpdateQuantityProduct([FromBody] ProductUpdateQuantity productUpdateQuantity, [FromRoute] int cartId)
        {
            var result = _shoppingCartUseCase.UpdateQuantityProduct(productUpdateQuantity, cartId);
            return Ok(result);
        }
        [HttpPatch]
        [Route("UpdateQuantityProductAttribute/{cartId}")]
        public ActionResult UpdateQuantityProductAttribute([FromBody] ProductAttributeUpdateQuantity productUpdateQuantity, [FromRoute] int cartId)
        {
            var result = _shoppingCartUseCase.UpdateQuantityProductAttribute(productUpdateQuantity, cartId);
            return Ok(result);
        }
        [HttpDelete]
        [Route("RemoveProduct/{cartProductId}/{cartId}")]
        public ActionResult RemoveProduct(int cartProductId, int cartId)
        {
            var result = _shoppingCartUseCase.RemoveProduct(cartProductId, cartId);
            return Ok(result);
        }
        [HttpDelete]
        [Route("RemoveProductAttribute/{cartId}")]
        public ActionResult RemoveProductAttribute([FromBody] ProductAttributeRemoved productRemoved, [FromRoute] int cartId)
        {
            var result = _shoppingCartUseCase.RemoveProductAttribute(productRemoved, cartId);
            return Ok(result);
        }

        [HttpGet]
        [Route("{cartId}")]
        public ActionResult GetShoppingCartById(int cartId)
        {
            var result = _shoppingCartUseCase.GetShoppingCartById(cartId);
            return Ok(result);
        }
    }
}
