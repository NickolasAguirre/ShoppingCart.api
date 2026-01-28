using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.domain.Entities.ShoppingCart
{
    public class CartAttributeDto
    {
        public int CartAttributeGroupId { get; set; }
        public int AttributeId { get; set; }
        public string? AttributeName { get; set; }
        public int Quantity { get; set; }
        public decimal PriceImpactAmount { get; set; }
    }
}
