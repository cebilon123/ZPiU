using AutoMapper;
using FluentAssertions;
using Manage.Core.Mapper.Profiles;
using Manage.Core.Models.Category;
using Manage.Core.Services;
using Manage.Core.Services.Interfaces;
using Manage.Data;
using Manage.Data.Models;
using Manage.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Tests.Services
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private Mock<ICategoryRepository> categoryRepositoryMock;
        private ICategoryService categoryService;

        [SetUp]
        public void SetUp()
        {
            categoryRepositoryMock = new Mock<ICategoryRepository>();
            categoryService = new CategoryService(categoryRepositoryMock.Object, new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<BaseProfile>(); })));
        }

        public static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

            return dbSetMock;
        }

        [Test]
        public async Task CreateCategory_WhenNameNotEmpty_ReturnValidResult()
        {
            var result = await categoryService.Create(new CategoryCreateRequest
            {
                Name = "Valid name"
            });

            result.Name.Should().Be("Valid name");
        }

        [Test]
        public async Task CreateCategory_WhenNameEmpty_ReturnInValidResult()
        {
            var result = await categoryService.Create(new CategoryCreateRequest
            {
                Name = ""
            });

            result.Name.Should().BeEmpty();
            result.IsSuccessful.Should().BeFalse();
            result.ErrorMessage.Should().Be("Category name was empty");
        }
    }
}
