using Library_BackEnd.Models.Dto;
using Library_BackEnd.Models.Entity;
using Library_BackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library_BackEnd.Controllers
{
    public class LibraryController : Controller
    {

        private readonly BookService _bookService;

        private ManagementViewModel GetManageView(Book b)
        {
            return new ManagementViewModel
            {
                Books = _bookService.GetAllBooks().Select(book => new ProductViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Genre = book.Genre,
                    IsAvailable = book.IsAvailable,
                    CoverImageUrl = book.CoverImageUrl
                }).ToList(),

                BookToEdit = b
            };
        }
        private ManagementViewModel GetManageView()
        {
            return new ManagementViewModel
            {
                Books = _bookService.GetAllBooks().Select(book => new ProductViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Genre = book.Genre,
                    IsAvailable = book.IsAvailable,
                    CoverImageUrl = book.CoverImageUrl
                }).ToList()
            };
        }

        public LibraryController(BookService bookService)
        {
            _bookService = bookService;
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
                    Genre = book.Genre,
                    IsAvailable = book.IsAvailable,
                    CoverImageUrl = book.CoverImageUrl
                });
            }

            return View(bookViewModels);
        }

        public IActionResult BackOffice()
        {

            var ModelManage = new ManagementViewModel();
            ModelManage = GetManageView();

            return View(ModelManage);
        }

        public IActionResult ToUpdate(Guid id)
        {
            var bookToEdit = _bookService.GetBookById(id);

            var ModelManage = new ManagementViewModel();
            ModelManage = GetManageView(bookToEdit);
            return View("BackOffice", ModelManage);
        }

        [HttpPost]
        public IActionResult CreateOrUpdate(Book book)
        {
            var ModelManage = new ManagementViewModel();
            ModelManage = GetManageView(book);

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
        public IActionResult Delete(Guid id)
        {
            var ModelManage = new ManagementViewModel();
            ModelManage = GetManageView(_bookService.GetBookById(id));


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
