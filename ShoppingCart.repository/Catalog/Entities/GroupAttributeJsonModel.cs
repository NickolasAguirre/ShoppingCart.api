using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShoppingCart.infrastructure.Catalog.Entities
{
    public class GroupAttributeJsonModel
    {
        [JsonPropertyName("groupASributeId")]
        public string GroupAttributeId { get; set; }

        [JsonPropertyName("groupASributeType")]
        public GroupAttributeTypeJsonModel GroupAttributeType { get; set; }

        [JsonPropertyName("descripIon")]
        public string Description { get; set; }

        [JsonPropertyName("quanItyInformaIon")]
        public QuantityInformationJsonModel QuantityInformation { get; set; }

        [JsonPropertyName("aSributes")]
        public List<AttributeJsonModel> Attributes { get; set; }

        [JsonPropertyName("order")]
        public int Order { get; set; }
    }
}
