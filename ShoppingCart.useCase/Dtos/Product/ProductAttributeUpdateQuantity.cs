using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.application.Dtos.Product
{
    public class ProductAttributeUpdateQuantity
    {
        public int CartProductId { get; set; }
        public int CartProductGroupId { get; set; }
        public int CartAttributeId { get; set; }
        public int Quantity { get; set; }
    }
}
