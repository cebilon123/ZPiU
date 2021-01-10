using Manage.Core.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Core.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryCreateResponse> Create(CategoryCreateRequest request);
        Task<CategoryDeleteResponse> Delete(long id);
        Task<CategoryGetResponse> Get(long id);
        Task<IEnumerable<CategoryDTO>> GetList();
    }
}
