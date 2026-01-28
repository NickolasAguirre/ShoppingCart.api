using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.domain.Entities.ShoppingCart
{
    public class CartProductGroupDto
    {
        public int CartProductGroupId { get; set; }
        public int CartProductId { get; set; }
        public int GroupAttributeId { get; set; }
        public string? GroupAttributeName { get; set; }
        public List<CartAttributeModel> Attributes { get; set; }

    }
}
