using ShoppingCart.domain.Entities;
using ShoppingCart.domain.Entities.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.application.Services
{
    public class PriceCalculatorService
    {
        public decimal CalculateFinalPrice(ProductModel catalogProduct, CartProductModel cartProduct)
        {
            decimal totalPriceImpact = 0;

            cartProduct.ProductGroup.ForEach(cartGroup =>
            {
                var catalogGroup = catalogProduct.GroupAttribute.FirstOrDefault(g => g.GroupAttributeId == cartGroup.GroupAttributeId);
                if (catalogGroup != null)
                {
                    cartGroup.Attributes.ForEach(cartAttr =>
                    {
                        var catalogAttr = catalogGroup.Attributes
                           .FirstOrDefault(a => a.AttributeId == cartAttr.AttributeId);

                        if (catalogAttr != null)
                        {
                            totalPriceImpact += catalogAttr.PriceImpactAmount * cartAttr.Quantity;
                        }
                    });
                    
                }
            });

            return cartProduct.BasePrice + totalPriceImpact;
        }
    }
}
