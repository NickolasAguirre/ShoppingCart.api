using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShoppingCart.infrastructure.Catalog.Entities
{
    public class QuantityInformationJsonModel
    {
        [JsonPropertyName("groupASributeQuanIty")]
        public int GroupAttributeQuantity { get; set; }

        [JsonPropertyName("showPricePerProduct")]
        public bool ShowPricePerProduct { get; set; }

        [JsonPropertyName("isShown")]
        public bool IsShown { get; set; }

        [JsonPropertyName("isEditable")]
        public bool IsEditable { get; set; }

        [JsonPropertyName("isVerified")]
        public bool IsVerified { get; set; }

        [JsonPropertyName("verifyValue")]
        public string VerifyValue { get; set; }
    }
}
