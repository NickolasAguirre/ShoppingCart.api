using ShoppingCart.application.Dtos.GroupAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.application.Dtos.Product
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public decimal BasePrice { get; set; }
        public int Quantity { get; set; }
        public List<GroupAttributeDto>? GroupAttribute { get; set; }
    }

}
