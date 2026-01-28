using ShoppingCart.application.Dtos.Product;
using ShoppingCart.domain.Entities;
using ShoppingCart.domain.Entities.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.application.UseCase.ShoppingCart
{
    public interface IShoppingCartUseCase
    {
        CartDto AddProduct(ProductRequestDto productRequest, int? cartId);
        CartDto UpdateProduct(ProductUpdatedDto productRequest, int cartId);
        CartDto UpdateQuantityProduct(ProductUpdateQuantity productUpdateQuantity, int cartId);
        CartDto UpdateQuantityProductAttribute(ProductAttributeUpdateQuantity productUpdateQuantity, int cartId);
        CartDto GetShoppingCartById(int cartId);
        CartDto RemoveProduct(int cartProductId, int cartId);
        CartDto RemoveProductAttribute(ProductAttributeRemoved productRemoved, int cartId);
    }
}
