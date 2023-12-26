using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Services.Abstract;
using Business.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;


namespace Presentation.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var model = await _categoryService.GetAllAsync();
            return View(model);
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM model)
        {
            var isSucceeded = await _categoryService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(List));

            return View(model);
        }
        #endregion

        #region Delete

        public async Task<IActionResult> Delete(int id)
        {
            var isSucceeded = await _categoryService.DeleteAsync(id);
            if (isSucceeded) return RedirectToAction(nameof(List));

            
            return NotFound("Kateqoriya tapilmadi");
        }

        #endregion
    }
}

