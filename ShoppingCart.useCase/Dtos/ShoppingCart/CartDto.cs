using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.domain.Entities.ShoppingCart
{
    public class CartDto
    {
        public int CartId { get; set; }
        public List<CartProductModel>? ListProduct { get; set; }
    }
}
