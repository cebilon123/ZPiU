using Manage.Core.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manage.Core.Models;

namespace Manage.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<BaseReponse> Create(CreateProductRequest request);
        Task<BaseReponse> Delete(long productId);
        Task<ProductDTO> Get(long id);
        Task<ICollection<ProductDTO>> GetAll();
        Task<ProductUpdateResponse> Update(CreateProductRequest request, long id);
        Task<ICollection<ProductDTO>> GetByNip(string nip);
    }
}
