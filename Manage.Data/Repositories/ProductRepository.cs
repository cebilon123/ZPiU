using Manage.Data.Models;
using Manage.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Data.Repositories
{
    public class ProductRepository: BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(BaseContext context): base(context)
        {

        }

        public Product GetIncludeCategory(long productId) => context.Products
            .Include(ctx => ctx.Category)
            .FirstOrDefault(a => a.Id == productId);

        public IEnumerable<Product> GetListIncludeCategory() => context.Products
            .Include(ctx => ctx.Category)
            .AsEnumerable();
    }
}
