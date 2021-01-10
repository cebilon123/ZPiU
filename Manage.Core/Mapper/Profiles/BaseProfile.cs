using AutoMapper;
using Manage.Core.Models;
using Manage.Core.Models.Category;
using Manage.Core.Models.ContractorPrice;
using Manage.Core.Models.Product;
using Manage.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Core.Mapper.Profiles
{
    public class BaseProfile: Profile
    {
        public BaseProfile()
        {
            CreateMap<CategoryCreateRequest, Category>()
                .ForMember(c => c.Name, a => a.MapFrom(rq => rq.Name));

            CreateMap<Category, CategoryDTO>()
                .ForMember(c => c.Name, a => a.MapFrom(rq => rq.Name))
                .ForMember(c => c.Id, a => a.MapFrom(rq => rq.Id))
                .ReverseMap();

            CreateMap<ContractorPriceCreate, ContractorPrice>()
                .ForMember(c => c.Price, a => a.MapFrom(rq => rq.Price))
                .ForMember(c => c.ContractorId, a => a.MapFrom(rq => rq.ContractorId))
                .ForMember(c => c.ProductId, a => a.MapFrom(rq => rq.ProductId))
                .ReverseMap();

            CreateMap<ContractorPrice, ContractorPriceDTO>()
                .ForMember(c => c.Price, a => a.MapFrom(rq => rq.Price))
                .ForMember(c => c.ProductId, a => a.MapFrom(rq => rq.ProductId))
                .ForMember(c => c.ProductName, a => a.MapFrom(rq => rq.Product.Name))
                .ForMember(c => c.Id, a => a.MapFrom(rq => rq.Id))
                .ReverseMap();

            CreateMap<CreateProductRequest, Product>()
                .ForMember(c => c.CategoryId, a => a.MapFrom(rq => rq.CategoryId))
                .ForMember(c => c.GtuCode, a => a.MapFrom(rq => rq.GtuCode))
                .ForMember(c => c.Name, a => a.MapFrom(rq => rq.Name))
                .ForMember(c => c.PkwiuCode, a => a.MapFrom(rq => rq.PkwiuCode))
                .ForMember(c => c.Price, a => a.MapFrom(rq => rq.Price))
                .ForMember(c => c.Unit, a => a.MapFrom(rq => rq.Unit))
                .ForMember(c => c.Vat, a => a.MapFrom(rq => rq.Vat))
                .ReverseMap();

            CreateMap<Product, ProductDTO>()
                .ForPath(c => c.Category.Id, a => a.MapFrom(rq => rq.CategoryId))
                .ForPath(c => c.Category.Name, a => a.MapFrom(rq => rq.Category.Name))
                .ReverseMap();
        }
    }
}
