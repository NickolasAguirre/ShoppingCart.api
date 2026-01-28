using ShoppingCart.domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.domain.Entities
{
    public class GroupAttributeModel
    {
        public int GroupAttributeId { get; set; }
        public string? GroupAttributeDescription { get; set; }
        public int GroupAttributeQuantity { get; set; }
        public VerifyValueType VerifyValue { get; set; }
        public required List<AttributeModel> Attributes { get; set; }
        public bool IsRequired => VerifyValue == VerifyValueType.EQUAL_THAN;
        public bool IsValid(int quantity)
        {
            if (VerifyValue == VerifyValueType.EQUAL_THAN)
                return quantity == GroupAttributeQuantity; 

            if (VerifyValue == VerifyValueType.LOWER_EQUAL_THAN)
                return quantity <= GroupAttributeQuantity; 

            return false;
        }
    }
}
