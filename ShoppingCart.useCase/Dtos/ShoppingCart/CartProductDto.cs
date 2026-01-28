using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.domain.Entities.ShoppingCart
{
    public class CartProductDto
    {
        public int CartProductId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal BasePrice { get; set; }
        public decimal FinalPrice { get; set; }
        public List<CartProductGroupModel> GroupAttribute { get; set; }
    }
}
