using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.domain.Entities.ShoppingCart
{
    [Table("CartProductGroup")]
    public class CartProductGroupModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartProductGroupId { get; set; }
        public int CartProductId { get; set; }
        public int GroupAttributeId { get; set; }
        public string? GroupAttributeName { get; set; }
        public List<CartAttributeModel> Attributes { get; set; }
        public CartProductModel CartProduct { get; set; }

    }
}
