using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.domain.Entities
{
    public class AttributeModel
    {
        public int AttributeId { get; set; }
        public string? AttributeName { get; set; }
        public bool IsRequired { get; set; }
        public int MaxQuantity { get; set; }
        public decimal PriceImpactAmount { get; set; }

    }
}
