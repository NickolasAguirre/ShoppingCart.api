using ShoppingCart.domain.Entities;
using ShoppingCart.domain.Entities.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.domain.Repositories
{
    public interface IShoppingCartRepository
    {
        void AddProduct(CartProductModel product, int cartId);
        void UpdateProduct(CartProductModel product, int cartId);
        CartModel CreateShoppingCart(CartModel shoppingCartModel);
        CartModel GetShoppingCartByIdTracked(int cartId);
        CartModel GetShoppingCartById(int cartId);
        CartProductModel GetProductById(int cartId, int cartProductId);
        void RemoveProduct(int cartProductId, int cartId);
        void SaveChanges();

        //CartModel RemoveProductAttribute(CartProductModel product, Guid shoppingCartId);

    }
}
