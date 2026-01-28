using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using ShoppingCart.domain.Entities;
using ShoppingCart.domain.Entities.ShoppingCart;
using ShoppingCart.domain.Repositories;
using ShoppingCart.infrastructure.DatabaseContext;
namespace ShoppingCart.infrastructure.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _dbContext;
        public ShoppingCartRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddProduct(CartProductModel product, int cartId)
        {
            //_dbContext.CartAttribute.AddRange(product.ProductGroup.SelectMany(x => x.Attributes));
            //_dbContext.CartProductGroup.AddRange(product.ProductGroup);
            _dbContext.CartProduct.Add(product);
            _dbContext.SaveChanges();

            //var productGroupCodes = product.ProductGroup.Select(x => x.CartProductGroupId).ToList();
            //var cartProductCodesUpdate = _dbContext.CartProductGroup.Where(x => productGroupCodes.Contains(x.CartProductGroupId)).ToList();
            //cartProductCodesUpdate.ForEach(x =>
            //{
            //    x.CartProductId = product.ProductId;
            //});

            //var attributesCodes = cartProductCodesUpdate.SelectMany(x => x.Attributes).Select(x => x.AttributeId).ToList();
            //var cartAttributesUpdate = _dbContext.CartAttribute.Where(x => attributesCodes.Contains(x.AttributeId)).ToList();
            //cartProductCodesUpdate.ForEach(x =>
            //{
            //    x.CartProductId = product.ProductId;
            //});

        }

        public void UpdateProduct(CartProductModel product, int cartId)
        {
            _dbContext.CartProduct.Update(product);
            _dbContext.SaveChanges();
        }

        public CartModel CreateShoppingCart(CartModel cartModel)
        {
            _dbContext.Cart.Add(cartModel);
            _dbContext.SaveChanges();
            return cartModel;
        }

        public CartModel GetShoppingCartById(int cartId)
        {
            var data =  _dbContext.Cart.AsNoTracking()
                        .Where(c => c.CartId == cartId)
                        .Select(c => new CartModel
                        {
                            CartId = c.CartId,
                            ListProduct = _dbContext.CartProduct
                            .Where(p => p.CartId == c.CartId)
                            .Select(p => new CartProductModel
                            {
                                CartProductId = p.CartProductId,
                                CartId = p.CartId,
                                ProductId = p.ProductId,
                                ProductName = p.ProductName,
                                Quantity = p.Quantity,
                                BasePrice = p.BasePrice,
                                FinalPrice = p.FinalPrice,
                                ProductGroup = _dbContext.CartProductGroup
                                .Where(g => g.CartProductId == p.CartProductId)
                                   .Select(g => new CartProductGroupModel
                                   {
                                       CartProductGroupId = g.CartProductGroupId,
                                       CartProductId = g.CartProductId,
                                       GroupAttributeId = g.GroupAttributeId,
                                       GroupAttributeName = g.GroupAttributeName,
                                       Attributes = _dbContext.CartAttribute
                                           .Where(a => a.CartProductGroupId == g.CartProductGroupId)
                                           .Select(a => new CartAttributeModel
                                           {
                                               CartAttributeId = a.CartAttributeId,
                                               CartProductGroupId = a.CartProductGroupId,
                                               AttributeId = a.AttributeId,
                                               AttributeName = a.AttributeName,
                                               Quantity = a.Quantity,
                                               PriceImpactAmount = a.PriceImpactAmount
                                           }) .ToList()
                                   }).ToList()
                                }).ToList()
                        })
                        .FirstOrDefault();
            return data;
        }

        public CartModel GetShoppingCartByIdTracked(int cartId)
        {
            var data = _dbContext.Cart.Include(c => c.ListProduct)
                       .ThenInclude(p => p.ProductGroup)
                       .ThenInclude(g => g.Attributes)
                       .FirstOrDefault(c => c.CartId == cartId);
            return data;
        }


        public CartProductModel GetProductById(int cartProductId, int cartId)
        {
            var cartProduct = _dbContext.CartProduct.FirstOrDefault(x => x.CartId == cartId && x.CartProductId == cartProductId);
            return cartProduct;
        }


        public void RemoveProduct(int cartProductId, int cartId)
        {
            var cartProduct = _dbContext.CartProduct.FirstOrDefault(x => x.CartId == cartId && x.CartProductId == cartProductId);
            var cartProductGroup = _dbContext.CartProductGroup.Where(x => x.CartProductId == cartProductId).ToList();
            var allCartProductGroupId = cartProductGroup.Select(x => x.CartProductGroupId).ToList();
            var cartAttribute = _dbContext.CartAttribute.Where(x => allCartProductGroupId.Contains(x.CartProductGroupId)).ToList();
            
            _dbContext.CartAttribute.RemoveRange(cartAttribute);
            _dbContext.CartProductGroup.RemoveRange(cartProductGroup);
            _dbContext.CartProduct.Remove(cartProduct);
            _dbContext.SaveChanges();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        //public CartModel RemoveProductAttribute(ProductCatalogModel product, Guid shoppingCartId)
        //{
        //    var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(24));
        //    _cache.TryGetValue(_cacheKey + shoppingCartId, out ShoppingCartModel shoppingCart);
        //    if (shoppingCart != null)
        //    {
        //        shoppingCart.ListProduct.RemoveAll(x => x.ProductId == productId);
        //        _cache.Set(_cacheKey + shoppingCartId, shoppingCart, cacheOptions);
        //    }
        //    return shoppingCart;
        //}
    }
}
