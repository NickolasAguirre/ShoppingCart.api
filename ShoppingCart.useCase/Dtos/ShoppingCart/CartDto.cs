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
        public decimal TotalAmount => ListProduct != null && ListProduct.Count > 0 ? ListProduct.Sum(x => x.TotalPrice) : 0;
        public List<CartProductModel>? ListProduct { get; set; }
    }
}
