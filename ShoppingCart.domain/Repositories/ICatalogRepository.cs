using ShoppingCart.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.domain.Repositories
{
    public interface ICatalogRepository
    {
        ProductModel GetCatalogFromJson();
    }
}
