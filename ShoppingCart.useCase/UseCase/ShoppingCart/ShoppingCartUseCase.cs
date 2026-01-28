using AutoMapper;
using ShoppingCart.application.Dtos.GroupAttribute;
using ShoppingCart.application.Dtos.Product;
using ShoppingCart.application.Services;
using ShoppingCart.application.UseCase.ShoppingCart;
using ShoppingCart.application.Validators;
using ShoppingCart.domain.Entities;
using ShoppingCart.domain.Entities.ShoppingCart;
using ShoppingCart.domain.Enum;
using ShoppingCart.domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.useCase.UseCase.ShoppingCart
{
    public class ShoppingCartUseCase : IShoppingCartUseCase
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper _mapper;
        private readonly ValidatorsProduct _validatorsProduct;
        private readonly PriceCalculatorService _priceCalculatorService;

        public ShoppingCartUseCase(ICatalogRepository catalogRepository, IShoppingCartRepository shoppingCartRepository, IMapper mapper, ValidatorsProduct validatorsProduct,
            PriceCalculatorService priceCalculatorService)
        {
            _catalogRepository = catalogRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
            _validatorsProduct = validatorsProduct;
            _priceCalculatorService = priceCalculatorService;

        }

        public CartDto AddProduct(ProductRequestDto productRequest, int? cartId)
        {
            var catalogFromJson = _catalogRepository.GetCatalogFromJson();

            if (catalogFromJson == null || catalogFromJson.ProductId != productRequest.ProductId) throw new Exception("No se logró encontrar el producto en el catalogo.");
            if (productRequest.Quantity <= 0) throw new Exception("La cantidad del producto debe ser mayor a 0");

            _validatorsProduct.AllValidationData(catalogFromJson, productRequest);

            var cartProduct = MapCartProductModel(catalogFromJson, productRequest);

            if (cartId == null)
            {
                var cartModel = new CartModel()
                {
                    ListProduct = new List<CartProductModel>()
                };
                _shoppingCartRepository.CreateShoppingCart(cartModel);
                cartId = cartModel.CartId;
            }
            cartProduct.CartId = cartId.Value;

            _shoppingCartRepository.AddProduct(cartProduct, cartId.Value);
            return _mapper.Map<CartDto>(_shoppingCartRepository.GetShoppingCartById(cartId.Value));
        }

        public CartDto UpdateQuantityProduct(ProductUpdateQuantity productUpdateQuantity, int cartId)
        {
            if (productUpdateQuantity.Quantity <= 0) throw new Exception("La cantidad del producto debe ser mayor a 0");

            var productModel = _shoppingCartRepository.GetProductById(productUpdateQuantity.CartProductId, cartId);
            
            if(productModel == null) throw new Exception("Producto no encontrado en el carrito");
            productModel.Quantity = productUpdateQuantity.Quantity;
            _shoppingCartRepository.SaveChanges();

            return _mapper.Map<CartDto>(_shoppingCartRepository.GetShoppingCartById(cartId));
        }

        public CartDto UpdateQuantityProductAttribute( ProductAttributeUpdateQuantity productUpdateQuantity, int cartId)
        {
            var cartShopping = _shoppingCartRepository.GetShoppingCartByIdTracked(cartId)  ?? throw new Exception("Carrito no encontrado");

            var product = cartShopping?.ListProduct?.FirstOrDefault(x => x.CartProductId == productUpdateQuantity.CartProductId) ?? throw new Exception("Producto no encontrado en el carrito");
            var groupAttribute = product?.ProductGroup?.FirstOrDefault(x => x.CartProductGroupId == productUpdateQuantity.CartProductGroupId)  ?? throw new Exception("Grupo no encontrado en el producto");
            var attribute = groupAttribute?.Attributes?.FirstOrDefault(x => x.CartAttributeId == productUpdateQuantity.CartAttributeId) ?? throw new Exception("Atributo no encontrado en el grupo");

            var catalogProduct = _catalogRepository.GetCatalogFromJson() ?? throw new Exception("No se encontró el catálogo");

            if (catalogProduct.ProductId != product.ProductId)
            {
                throw new Exception("El producto no coincide con el catálogo");
            }

            var groupAttributeFromCatalog = catalogProduct.GroupAttribute.FirstOrDefault(x => x.GroupAttributeId == groupAttribute.GroupAttributeId) ?? throw new Exception("Grupo no encontrado en el catálogo");
            var attributeFromCatalog = groupAttributeFromCatalog.Attributes.FirstOrDefault(x => x.AttributeId == attribute.AttributeId)  ?? throw new Exception("Atributo no encontrado en el catálogo");


            _validatorsProduct.ValidateAttributeQuantityUpdate(attribute, attributeFromCatalog, groupAttribute,  groupAttributeFromCatalog,productUpdateQuantity.Quantity);

            if (productUpdateQuantity.Quantity == 0)
            {
                groupAttribute.Attributes.Remove(attribute);
            }
            else
            {
                attribute.Quantity = productUpdateQuantity.Quantity;
            }

            product.FinalPrice = _priceCalculatorService.CalculateFinalPrice(catalogProduct, product);
            _shoppingCartRepository.SaveChanges();
            return _mapper.Map<CartDto>(_shoppingCartRepository.GetShoppingCartById(cartId));
        }



        public CartDto UpdateProduct(ProductUpdatedDto productUpdated, int cartId)
        {
            if (productUpdated.CartProductId == null)
            {
                throw new Exception("Se requiere el CartProductId para actualizar el producto");
            }
            if (productUpdated.Quantity <= 0)
            {
                throw new Exception("La cantidad del producto debe ser mayor a 0");
            }

            var cart = _shoppingCartRepository.GetShoppingCartByIdTracked(cartId);
            if (cart == null) throw new Exception("Carrito no encontrado");
            var existProduct = cart.ListProduct.FirstOrDefault(p => p.CartProductId == productUpdated.CartProductId) ?? throw new Exception("Producto no encontrado en el carrito");

            var catalogFromJson = _catalogRepository.GetCatalogFromJson();
            if (catalogFromJson == null || catalogFromJson.ProductId != productUpdated.ProductId) throw new Exception("El producto no se encuentra en el catálogo");


            var productRequestDto = _mapper.Map<ProductRequestDto>(productUpdated);
            _validatorsProduct.AllValidationData(catalogFromJson, productRequestDto);

            existProduct.ProductName = catalogFromJson.ProductName;
            existProduct.BasePrice = catalogFromJson.Price;
            existProduct.Quantity = productUpdated.Quantity;

            UpdateProductGroups(existProduct, productUpdated, catalogFromJson);
            existProduct.FinalPrice = _priceCalculatorService.CalculateFinalPrice(catalogFromJson, existProduct);
            _shoppingCartRepository.SaveChanges();

            return _mapper.Map<CartDto>(_shoppingCartRepository.GetShoppingCartById(cartId));
        }

        private void UpdateProductGroups(CartProductModel existingProduct, ProductUpdatedDto productUpdated, ProductModel catalogProduct)
        {
            var requestGroupIds = productUpdated.GroupAttribute.Select(g => g.GroupAttributeId).ToList();
            var groupsToRemove = existingProduct.ProductGroup.Where(g => !requestGroupIds.Contains(g.GroupAttributeId)).ToList();

            foreach (var groupToRemove in groupsToRemove)
            {
                existingProduct.ProductGroup.Remove(groupToRemove);
            }

            foreach (var requestGroup in productUpdated.GroupAttribute)
            {
                var catalogGroup = catalogProduct.GroupAttribute.FirstOrDefault(g => g.GroupAttributeId == requestGroup.GroupAttributeId);

                if (catalogGroup == null) continue;

                var existingGroup = existingProduct.ProductGroup.FirstOrDefault(g => g.GroupAttributeId == requestGroup.GroupAttributeId);

                if (existingGroup != null)
                {
                    UpdateGroupAttributes(existingGroup, requestGroup, catalogGroup);
                }
                else
                {
                    var newGroup = CreateNewGroup(existingProduct.CartProductId, requestGroup, catalogGroup);
                    existingProduct.ProductGroup.Add(newGroup);
                }
            }
        }

        private void UpdateGroupAttributes(CartProductGroupModel existingGroup, GroupAttributeRequestDto requestGroup,GroupAttributeModel catalogGroup)
        {
            var requestAttrIds = requestGroup.Attributes.Select(a => a.AttributeId).ToList();
            var attrsToRemove = existingGroup.Attributes.Where(a => !requestAttrIds.Contains(a.AttributeId)).ToList();

            foreach (var attrToRemove in attrsToRemove)
            {
                existingGroup.Attributes.Remove(attrToRemove);
            }

            foreach (var requestAttr in requestGroup.Attributes)
            {
                var catalogAttr = catalogGroup.Attributes.FirstOrDefault(a => a.AttributeId == requestAttr.AttributeId);

                if (catalogAttr == null) continue;

                var existingAttr = existingGroup.Attributes.FirstOrDefault(a => a.AttributeId == requestAttr.AttributeId);

                if (existingAttr != null)
                {
                    existingAttr.Quantity = requestAttr.Quantity;
                    existingAttr.PriceImpactAmount = catalogAttr.PriceImpactAmount;
                    existingAttr.AttributeName = catalogAttr.AttributeName;
                }
                else
                {
                    var newAttr = new CartAttributeModel
                    {
                        CartProductGroupId = existingGroup.CartProductGroupId,
                        AttributeId = catalogAttr.AttributeId,
                        AttributeName = catalogAttr.AttributeName,
                        Quantity = requestAttr.Quantity,
                        PriceImpactAmount = catalogAttr.PriceImpactAmount
                    };
                    existingGroup.Attributes.Add(newAttr);
                }
            }
        }

        private CartProductGroupModel CreateNewGroup(int cartProductId, GroupAttributeRequestDto requestGroup,  GroupAttributeModel catalogGroup)
        {
            var newGroup = new CartProductGroupModel
            {
                CartProductId = cartProductId,
                GroupAttributeId = catalogGroup.GroupAttributeId,
                GroupAttributeName = catalogGroup.GroupAttributeDescription,
                Attributes = new List<CartAttributeModel>()
            };

            foreach (var requestAttr in requestGroup.Attributes)
            {
                var catalogAttr = catalogGroup.Attributes.FirstOrDefault(a => a.AttributeId == requestAttr.AttributeId);

                if (catalogAttr == null) continue;

                var newAttr = new CartAttributeModel
                {
                    CartProductGroupId = newGroup.CartProductGroupId,
                    AttributeId = catalogAttr.AttributeId,
                    AttributeName = catalogAttr.AttributeName,
                    Quantity = requestAttr.Quantity,
                    PriceImpactAmount = catalogAttr.PriceImpactAmount
                };

                newGroup.Attributes.Add(newAttr);
            }

            return newGroup;
        }



        public CartDto GetShoppingCartById(int cartId)
        {
            return _mapper.Map<CartDto>(_shoppingCartRepository.GetShoppingCartById(cartId));
        }

        public CartDto RemoveProduct(int cartProductId, int cartId)
        {
            _shoppingCartRepository.RemoveProduct(cartProductId, cartId);
            return _mapper.Map<CartDto>(_shoppingCartRepository.GetShoppingCartById(cartId));
        }

        public CartDto RemoveProductAttribute(ProductAttributeRemoved productRemoved, int cartId)
        {

            var cartShopping = _shoppingCartRepository.GetShoppingCartByIdTracked(cartId) ?? throw new Exception("Carrito no encontrado");
            var product = cartShopping.ListProduct.FirstOrDefault(x=>x.CartProductId == productRemoved.CartProductId) ?? throw new Exception("Producto no encontrado en el carrito");
            var groupAttribute = product.ProductGroup.FirstOrDefault(x=>x.CartProductGroupId == productRemoved.CartProductGroupId) ?? throw new Exception("Grupo no encontrado en el producto del carrito");
            var attribute = groupAttribute.Attributes.FirstOrDefault(x => x.CartAttributeId == productRemoved.CartAttributeId) ?? throw new Exception("Attributo no encontrado en el producto del carrito");



            var catalogShopping = _catalogRepository.GetCatalogFromJson() ?? throw new Exception("No se encontro el catalogo");
            var groupAttributeFromCatalog = catalogShopping.GroupAttribute.FirstOrDefault(x => x.GroupAttributeId == groupAttribute.GroupAttributeId) ?? throw new Exception("Grupo no encontrado en el catologo");
            var attributeFromCatalog = groupAttributeFromCatalog.Attributes.FirstOrDefault(x => x.AttributeId == attribute.AttributeId) ?? throw new Exception("Attributo no encontrado en el catologo");


            if (attributeFromCatalog.IsRequired == true) throw new Exception("Attributo es requerido, no se puede eliminar");

            var quantityByGroup = groupAttribute.Attributes.Where(x => x.CartAttributeId != productRemoved.CartAttributeId).Sum(x => x.Quantity);

            _validatorsProduct.ValidateAttributeQuantityUpdate(attribute, attributeFromCatalog, groupAttribute, groupAttributeFromCatalog, quantityByGroup);


            groupAttribute.Attributes.Remove(attribute);
            if (!groupAttribute.Attributes.Any())
            {
                product.ProductGroup.Remove(groupAttribute);
            }
            product.FinalPrice = _priceCalculatorService.CalculateFinalPrice(catalogShopping, product);
            _shoppingCartRepository.SaveChanges();
            return _mapper.Map<CartDto>(_shoppingCartRepository.GetShoppingCartById(cartId));

        }

        public CartProductModel MapCartProductModel(ProductModel catalogProduct, ProductRequestDto productRequest)
        {
            decimal totalPriceImpact = 0;
            var cartProduct = new CartProductModel
            {
                ProductId = catalogProduct.ProductId,
                ProductName = catalogProduct.ProductName, 
                BasePrice = catalogProduct.Price,   
                Quantity = productRequest.Quantity,
                ProductGroup = new List<CartProductGroupModel>()
            };

            foreach (var requestProductGroup in productRequest.GroupAttribute)
            {
                var catalogProductGroup = catalogProduct.GroupAttribute.FirstOrDefault(g => g.GroupAttributeId == requestProductGroup.GroupAttributeId);
                if (catalogProductGroup == null) continue;

                var cartProductGroupModel = new CartProductGroupModel
                {
                    GroupAttributeId = catalogProductGroup.GroupAttributeId,
                    GroupAttributeName = catalogProductGroup.GroupAttributeDescription,
                    Attributes = new List<CartAttributeModel>()
                };

                foreach (var requestAttribute in requestProductGroup.Attributes)
                {
                    var catalogAttr = catalogProductGroup.Attributes.FirstOrDefault(a => a.AttributeId == requestAttribute.AttributeId);
                    if (catalogAttr == null) continue;

                    var cartAttributeModel = new CartAttributeModel
                    {
                        AttributeId = catalogAttr.AttributeId,
                        AttributeName = catalogAttr.AttributeName,  
                        Quantity = requestAttribute.Quantity,
                        PriceImpactAmount = catalogAttr.PriceImpactAmount  
                    };

                    totalPriceImpact += catalogAttr.PriceImpactAmount * requestAttribute.Quantity;
                    cartProductGroupModel.Attributes.Add(cartAttributeModel);
                }

                cartProduct.ProductGroup.Add(cartProductGroupModel);
            }

            cartProduct.FinalPrice = catalogProduct.Price + totalPriceImpact;
            return cartProduct;
        }

    }

}
