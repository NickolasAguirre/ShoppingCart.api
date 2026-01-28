using ShoppingCart.application.Dtos.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.application.Dtos.GroupAttribute
{
    public class GroupAttributeRequestDto
    {
        public int GroupAttributeId { get; set; }
        public required List<AttributeRequestDto> Attributes { get; set; }

    }
}
