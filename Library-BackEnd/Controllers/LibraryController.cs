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
            return new ManagementViewModel
            {
                ToForm =
                {
                    Books= _bookService.GetAllBooks().Select(book => new ProductViewModel
                        {
                            Id = book.Id,
                            Title = book.Title,
                            IsAvailable = book.IsAvailable,
                            CoverImageUrl = book.CoverImageUrl
                         }).ToList(),
                    Categories = await _categoryService.GetAllCategories()
                },

                BookToEdit = b
            };
        }
        private async Task<ManagementViewModel> GetManageView()
        {
            return new ManagementViewModel
            {
                ToForm =
                {
                    Books= _bookService.GetAllBooks().Select(book => new ProductViewModel
                        {
                            Id = book.Id,
                            Title = book.Title,
                            IsAvailable = book.IsAvailable,
                            CoverImageUrl = book.CoverImageUrl
                         }).ToList(),
                    Categories = await _categoryService.GetAllCategories()
                }
            };
        }

        public LibraryController(BookService bookService, CategoryService categoryService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var books = _bookService.GetAllBooks();

            var bookViewModels = new List<ProductViewModel>();

            foreach (var book in books)
            {
                bookViewModels.Add(new ProductViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    IsAvailable = book.IsAvailable,
                    CoverImageUrl = book.CoverImageUrl
                });
            }

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
        public async Task<IActionResult> CreateOrUpdate(Book book)
        {
            var ModelManage = new ManagementViewModel();
            ModelManage = await GetManageView(book);

            if (!ModelState.IsValid)
            {


                return View("BackOffice", ModelManage);
            }

            if (book.Id == Guid.Empty && book.Id == default)
            {
                book.Id = Guid.NewGuid();

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
    }
}
