using System;
using Business.Services.Abstract;
using Business.ViewModels.Category;
using Common.Entities;
using DataAccess.Repository.Abstract;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Business.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private ModelStateDictionary _modelState;

        public CategoryService(ICategoryRepository categoryRepository,
                               IUnitOfWork unitOfWork,
                               IActionContextAccessor actionContextAccessor)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<List<CategoryListItemVM>> GetAllAsync()
        {
            var dbCategories = await _categoryRepository.GetAllAsync();

            var model = new List<CategoryListItemVM>();
            foreach (var dbCategory in dbCategories)
            {
                model.Add(new CategoryListItemVM
                {
                    Id = dbCategory.Id,
                    Title = dbCategory.Title
                });
            }
            return model;
        }

        public async Task<bool> CreateAsync(CategoryCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var category = await _categoryRepository.GetByTitleAsync(model.Title);
            if (category is not null)
            {
                _modelState.AddModelError("Title", "Bu adda kateqoriya movcuddur");
                return false;
            }

            category = new Category
            {
                Title = model.Title,
                CreatedAt = DateTime.Now
            };

            await _categoryRepository.CreateAsync(category);
            await _unitOfWork.CommitAsync();
            return true;


        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetAsync(id);
            if (category is null)  return false;

            _categoryRepository.Delete(category);
            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}

