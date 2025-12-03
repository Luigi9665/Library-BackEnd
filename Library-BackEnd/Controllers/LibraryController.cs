using Library_BackEnd.Models.Dto;
using Library_BackEnd.Models.Entity;
using Library_BackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library_BackEnd.Controllers
{
    public class LibraryController : Controller
    {

        private readonly BookService _bookService;

        private readonly CategoryService _categoryService;

        private async Task<ManagementViewModel> GetManageView(Book b)
        {

            var AllBooks = _bookService.GetAllBooks();

            var BooksToView = new List<ProductViewModel>();

            foreach (var book in AllBooks)
            {

                var category = await _categoryService.GetCategoryById(book.CategoryId);

                BooksToView.Add(new ProductViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    IsAvailable = book.IsAvailable,
                    CoverImageUrl = book.CoverImageUrl,
                    CategoryId = category.Id,
                    NameCategory = category.Name
                });
            }

            return new ManagementViewModel
            {
                ToForm =
                {
                    Categories = await _categoryService.GetAllCategories(),
                    BookToEdit = b
                },
                Books = BooksToView
            };
        }
        private async Task<ManagementViewModel> GetManageView()
        {
            var AllBooks = _bookService.GetAllBooks();

            var BooksToView = new List<ProductViewModel>();

            foreach (var book in AllBooks)
            {

                var category = await _categoryService.GetCategoryById(book.CategoryId);

                BooksToView.Add(new ProductViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    IsAvailable = book.IsAvailable,
                    CoverImageUrl = book.CoverImageUrl,
                    CategoryId = category.Id,
                    NameCategory = category.Name
                });
            }

            return new ManagementViewModel
            {
                ToForm =
                {
                    Categories = await _categoryService.GetAllCategories()
                },
                Books = BooksToView
            };
        }

        public LibraryController(BookService bookService, CategoryService categoryService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var books = _bookService.GetAllBooks();

            var Categorys = await _categoryService.GetAllCategories();

            var bookViewModels = new ListForIndex();

            foreach (var book in books)
            {

                var category = await _categoryService.GetCategoryById(book.CategoryId);

                bookViewModels.Books.Add(new ProductViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    IsAvailable = book.IsAvailable,
                    CoverImageUrl = book.CoverImageUrl,
                    CategoryId = category.Id,
                    NameCategory = category.Name
                });
            }

            bookViewModels.Categories = Categorys;

            return View(bookViewModels);
        }

        public async Task<IActionResult> BackOffice()
        {

            var ModelManage = new ManagementViewModel();
            ModelManage = await GetManageView();

            return View(ModelManage);
        }

        public async Task<IActionResult> ToUpdate(Guid id)
        {
            var bookToEdit = _bookService.GetBookById(id);

            var ModelManage = new ManagementViewModel();
            ModelManage = await GetManageView(bookToEdit);
            return View("BackOffice", ModelManage);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(FormBookViewModel fb)
        {

            var book = fb.BookToEdit;

            var ModelManage = new ManagementViewModel();
            ModelManage = await GetManageView(book);


            if (!ModelState.IsValid)
            {
                return View("BackOffice", ModelManage);
            }



            if (book.Id == Guid.Empty && book.Id == default)
            {
                book.Id = Guid.NewGuid();

                var category = await _categoryService.GetCategoryById(book.CategoryId);

                book.Category = category;

                _bookService.CreateBook(book);

                TempData["Message"] = "Prodotto Creato";

            }
            else
            {

                _bookService.UpdateBook(book);

                TempData["Message"] = "Prodotto Aggiornato";

            }


            return RedirectToAction("BackOffice");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ModelManage = new ManagementViewModel();
            ModelManage = await GetManageView(_bookService.GetBookById(id));


            if (_bookService.DeleteBook(id))
            {
                TempData["Message"] = "Libro Eliminato";
            }
            else
            {
                TempData["Message"] = "Errore nell'eliminare il libro";
            }

            return RedirectToAction("BackOffice");
        }


        [HttpPost]
        public async Task<IActionResult> FilterResult(ToFIlterModal filter)
        {

            var books = await _bookService.GetBooksByCategoryId(filter);

            var Categorys = await _categoryService.GetAllCategories();

            var bookViewModels = new ListForIndex();

            foreach (var book in books)
            {

                var category = await _categoryService.GetCategoryById(book.CategoryId);

                bookViewModels.Books.Add(new ProductViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    IsAvailable = book.IsAvailable,
                    CoverImageUrl = book.CoverImageUrl,
                    CategoryId = category.Id,
                    NameCategory = category.Name
                });
            }

            bookViewModels.Categories = Categorys;

            return View("Index", bookViewModels);

        }

    }
}
