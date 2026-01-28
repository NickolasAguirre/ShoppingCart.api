using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.domain.Entities.ShoppingCart
{
    [Table("CartAttribute")]
    public class CartAttributeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartAttributeId { get; set; }
        public int CartProductGroupId { get; set; }
        public int AttributeId { get; set; }
        public string? AttributeName { get; set; }
        public int Quantity { get; set; }
        public decimal PriceImpactAmount { get; set; }
        public CartProductGroupModel CartProductGroup { get; set; }
    }
}
