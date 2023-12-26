using System;
using Business.ViewModels.Category;
using Common.Entities;

namespace Business.Services.Abstract
{
	public interface ICategoryService
	{
		Task<List<CategoryListItemVM>> GetAllAsync();
		Task<bool> CreateAsync(CategoryCreateVM model);
		Task<bool> DeleteAsync(int id);
	}
}

