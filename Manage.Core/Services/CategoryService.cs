using AutoMapper;
using Manage.Core.Models.Category;
using Manage.Core.Services.Interfaces;
using Manage.Data.Models;
using Manage.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryService(ICategoryRepository categoryRepository,IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<CategoryCreateResponse> Create(CategoryCreateRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return new CategoryCreateResponse
                {
                    ErrorMessage = "Category name was empty"
                };

            var category = mapper.Map<Category>(request);

            categoryRepository.Insert(category);

            return await Task.FromResult(new CategoryCreateResponse
            {
                Id = category.Id,
                IsSuccessful = true,
                Name = category.Name
            });
        }

        public async Task<CategoryDeleteResponse> Delete(long id)
        {
            if (id <= 0)
                return await Task.FromResult(new CategoryDeleteResponse
                {
                    ErrorMessage = "Id must be greater than 0"
                });

            var category = categoryRepository.Get(id);

            if(category is null)
                return await Task.FromResult(new CategoryDeleteResponse
                {
                    ErrorMessage = "Category not found"
                });

            categoryRepository.Delete(category);

            return await Task.FromResult(new CategoryDeleteResponse
            {
                IsSuccessful = true,
                Id = category.Id
            });
        }

        public async Task<CategoryGetResponse> Get(long id)
        {
            if (id <= 0)
                return await Task.FromResult(new CategoryGetResponse
                {
                    ErrorMessage = "Id must be greater than 0"
                });

            var category = categoryRepository.Get(id);

            if(category is null)
                return await Task.FromResult(new CategoryGetResponse
                {
                    ErrorMessage = "Category not found"
                });

            return new CategoryGetResponse
            {
                IsSuccessful = true,
                Name = category.Name,
                Id = category.Id
            };
        }

        public async Task<IEnumerable<CategoryDTO>> GetList()
        {
            return await Task.Run(() =>
            {
                return mapper.Map<List<CategoryDTO>>(categoryRepository.GetAll());

            }) ?? new List<CategoryDTO>();
        }
    }
}
