using Manage.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Data.Repositories.Interfaces
{
    public interface IProductRepository: IBaseRepository<Product>
    {
        Product GetIncludeCategory(long productId);
        IEnumerable<Product> GetListIncludeCategory();
    }
}
