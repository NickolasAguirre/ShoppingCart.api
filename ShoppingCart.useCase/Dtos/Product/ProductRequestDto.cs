using ShoppingCart.application.Dtos.GroupAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.application.Dtos.Product
{
    public class ProductRequestDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public List<GroupAttributeRequestDto>? GroupAttribute { get; set; }
    }
}
