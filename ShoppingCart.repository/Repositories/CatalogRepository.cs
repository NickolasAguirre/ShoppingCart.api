using ShoppingCart.domain.Entities;
using ShoppingCart.domain.Enum;
using ShoppingCart.domain.Repositories;
using ShoppingCart.infrastructure.Catalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShoppingCart.infrastructure.Repositories
{
    public class CatalogRepository: ICatalogRepository
    {
        public ProductModel GetCatalogFromJson()
        {
            var jsonFile = File.ReadAllText("../ShoppingCart.repository/Catalog/catalog.json");
            var productJson = JsonSerializer.Deserialize<ProductJsonModel>(jsonFile, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var productModel = MapProductJsonToProductModel(productJson!);

            return productModel;
        }

        public ProductModel MapProductJsonToProductModel(ProductJsonModel productJson)
        {
            var productModel = new ProductModel
            {
                ProductId = productJson.ProductId,
                ProductName = productJson.Name,
                Price = productJson.Price,
                GroupAttribute = productJson.GroupAttributes?.Select(ga => new GroupAttributeModel
                {
                    GroupAttributeId = int.Parse(ga.GroupAttributeId),
                    GroupAttributeQuantity = ga.QuantityInformation.GroupAttributeQuantity,
                    GroupAttributeDescription = ga.Description,
                    VerifyValue = Enum.Parse<VerifyValueType>(ga.QuantityInformation.VerifyValue),
                    Attributes = ga.Attributes.Select(a => new AttributeModel
                    {
                        AttributeId = a.AttributeId,
                        AttributeName = a.Name,
                        IsRequired = a.IsRequired,
                        MaxQuantity = a.MaxQuantity,
                        PriceImpactAmount = a.PriceImpactAmount

                    }).ToList(),
                    
                }).ToList()

            };
            return productModel;
        }
    }
}
