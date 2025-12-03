using Library_BackEnd.Models.Dto;
using Library_BackEnd.Models.Entity;
using Library_BackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library_BackEnd.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var ModelCategory = new CategoryViewModal
            {
                Categories = await _categoryService.GetAllCategories()
            };

            return View(ModelCategory);
        }


        public async Task<IActionResult> ToUpdate(Guid id)
        {
            var categoryToEdit = await _categoryService.GetCategoryById(id);

            var ModelCategory = new CategoryViewModal
            {
                Categories = _categoryService.GetAllCategories().Result,
                CategoryToEdit = categoryToEdit
            };
            return View("Index", ModelCategory);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(Category category)
        {

            if (ModelState.IsValid)
            {
                // Code to save the category to the database would go here

                var ModelCategory = new CategoryViewModal
                {
                    Categories = _categoryService.GetAllCategories().Result,
                    CategoryToEdit = category
                };

                return View("Index", ModelCategory);
            }

            category.CreatedAt = DateTime.Now;

            bool isCreate = false;

            if (category.Id == Guid.Empty && category.Id == default)
            {
                isCreate = await _categoryService.CreateCategory(category);
                if (isCreate)
                {
                    TempData["Message"] = "Category created successfully!";
                }
            }
            else
            {
                isCreate = await _categoryService.UpdateCategory(category);
                if (isCreate)
                {
                    TempData["Message"] = "Category updated successfully!";
                }
            }


            if (isCreate == false)
            {
                TempData["Message"] = "Category not created!";
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool isDeleted = await _categoryService.DeleteCategory(id);
            if (isDeleted)
            {
                TempData["Message"] = "Category deleted successfully!";
            }
            else
            {
                TempData["Message"] = "Category not deleted!";
            }
            return RedirectToAction("Index");
        }

    }
}
