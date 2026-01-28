using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShoppingCart.infrastructure.Catalog.Entities
{
    public class AttributeJsonModel
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [JsonPropertyName("aSributeId")]
        public int AttributeId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("defaultQuanIty")]
        public int DefaultQuantity { get; set; }

        [JsonPropertyName("maxQuanIty")]
        public int MaxQuantity { get; set; }

        [JsonPropertyName("priceImpactAmount")]
        public decimal PriceImpactAmount { get; set; }

        [JsonPropertyName("isRequired")]
        public bool IsRequired { get; set; }

        [JsonPropertyName("negaIveASributeId")]
        public string NegativeAttributeId { get; set; }

        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("statusId")]
        public string StatusId { get; set; }

        [JsonPropertyName("urlImage")]
        public string UrlImage { get; set; }
    }
}
