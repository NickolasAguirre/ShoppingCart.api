using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShoppingCart.infrastructure.Catalog.Entities
{
    public class GroupAttributeTypeJsonModel
    {
        [JsonPropertyName("groupASributeTypeId")]
        public string GroupAttributeTypeId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
