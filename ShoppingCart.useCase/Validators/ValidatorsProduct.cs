using ShoppingCart.application.Dtos.Product;
using ShoppingCart.domain.Entities;
using ShoppingCart.domain.Entities.ShoppingCart;
using ShoppingCart.domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.application.Validators
{
    public class ValidatorsProduct
    {
        public void ValidateIfHasRequired(ProductModel catalogFromJson, ProductRequestDto product)
        {

            var requiredGroupIds = catalogFromJson.GroupAttribute?.Where(x => x.IsRequired).Select(x => x.GroupAttributeId).ToList();
            var productGroupAttributeIds = product.GroupAttribute?.Select(x => x.GroupAttributeId).ToList();

            bool hasAllRequired = requiredGroupIds.All(id => productGroupAttributeIds != null && productGroupAttributeIds.Contains(id));
            if (!hasAllRequired)
            {
                throw new Exception("No se logran encontrar los grupos requeridos de este producto");
            }
        }

        public void ValidateQuantityGroupAttributes(ProductModel catalogFromJson, ProductRequestDto product)
        {
            product.GroupAttribute?.ForEach(groupAttr =>
            {
                var groupAttrFromJson = catalogFromJson.GroupAttribute?.FirstOrDefault(x => x.GroupAttributeId == groupAttr.GroupAttributeId);

                if (groupAttrFromJson != null)
                {
                    int totalSelectedInGroup = groupAttr.Attributes?.Sum(a => a.Quantity) ?? 0;

                    if (!groupAttrFromJson.IsValid(totalSelectedInGroup))
                    {
                        throw new Exception($"La cantidad de elementos en el grupo es incorrecta.");
                    }
                }
            });
        }

        public void ValidateAttributesMaxQuantity(ProductModel catalogFromJson, ProductRequestDto product)
        {
            var allAtributesFromJson = catalogFromJson.GroupAttribute?.SelectMany(x => x.Attributes).ToList();
            var allAttributesFromProduct = product.GroupAttribute?.SelectMany(x => x.Attributes).ToList();

            allAttributesFromProduct?.ForEach(attr =>
            {
                var attrFromJson = allAtributesFromJson?.FirstOrDefault(x => x.AttributeId == attr.AttributeId);
                if (attrFromJson != null)
                {
                    if (attr.Quantity > attrFromJson.MaxQuantity)
                    {
                        throw new Exception($"Attribute quantity for attribute ID {attr.AttributeId} exceeds the maximum allowed.");
                    }
                }
            });
        }

        public void ValidateExistGroup(ProductModel catalogFromJson, ProductRequestDto product)
        {
            var allGroupIdsFromJson = catalogFromJson.GroupAttribute?.Select(x => x.GroupAttributeId).ToList();
            var allGroupIdsFromProduct = product.GroupAttribute?.Select(x => x.GroupAttributeId).ToList();

            bool allGroupsExist = allGroupIdsFromProduct.All(id => allGroupIdsFromJson != null && allGroupIdsFromJson.Contains(id));
            if (!allGroupsExist)
            {
                throw new Exception("One or more group attributes do not exist.");
            }
        }
        public void ValidateExistAttribute(ProductModel catalogFromJson, ProductRequestDto product)
        {

            foreach (var userGroup in product.GroupAttribute)
            {
                var catalogGroup = catalogFromJson.GroupAttribute?.FirstOrDefault(g => g.GroupAttributeId == userGroup.GroupAttributeId);
                if (catalogGroup == null) throw new Exception("El grupo no existe.");

                foreach (var userAttr in userGroup.Attributes)
                {
                    bool existsInGroup = catalogGroup.Attributes.Any(a => a.AttributeId == userAttr.AttributeId);
                    if (!existsInGroup)

                    {
                        throw new Exception($"El atributo {userAttr.AttributeId} no pertenece al grupo {catalogGroup.GroupAttributeDescription ?? catalogGroup.GroupAttributeId.ToString()}");
                    }

                }

            }

        }
        public void ValidateAttributeQuantityUpdate(CartAttributeModel attribute, AttributeModel attributeFromCatalog,CartProductGroupModel groupAttribute,  
            GroupAttributeModel groupAttributeFromCatalog, int newQuantity)
        {
            if (newQuantity < 0)
            {
                throw new Exception("La cantidad no puede ser negativa");
            }

            if (newQuantity > attributeFromCatalog.MaxQuantity)
            {
                throw new Exception(
                    $"El atributo '{attributeFromCatalog.AttributeName}' permite máximo " +
                    $"{attributeFromCatalog.MaxQuantity} unidades. Intentas establecer {newQuantity}"
                );
            }

            var totalQuantityInGroup = groupAttribute.Attributes
                .Where(x => x.CartAttributeId != attribute.CartAttributeId)
                .Sum(x => x.Quantity) + newQuantity;

            if (!groupAttributeFromCatalog.IsValid(totalQuantityInGroup))
            {
                string message;
                if (groupAttributeFromCatalog.VerifyValue == VerifyValueType.EQUAL_THAN)
                {
                    message = $"El grupo '{groupAttributeFromCatalog.GroupAttributeDescription}' requiere " +
                              $"exactamente {groupAttributeFromCatalog.GroupAttributeQuantity} selección(es). " +
                              $"Con el cambio tendrías {totalQuantityInGroup}";
                }
                else
                {
                    message = $"El grupo '{groupAttributeFromCatalog.GroupAttributeDescription}' permite " +
                              $"máximo {groupAttributeFromCatalog.GroupAttributeQuantity} selección(es). " +
                              $"Con el cambio tendrías {totalQuantityInGroup}";
                }
                throw new Exception(message);
            }
        }

        public void AllValidationData(ProductModel catalogFromJson, ProductRequestDto productRequest)
        {
            ValidateIfHasRequired(catalogFromJson, productRequest);
            ValidateQuantityGroupAttributes(catalogFromJson, productRequest);
            ValidateAttributesMaxQuantity(catalogFromJson, productRequest);
            ValidateExistGroup(catalogFromJson, productRequest);
            ValidateExistAttribute(catalogFromJson, productRequest);
        }
    }
}
