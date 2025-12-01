using Library_BackEnd.Models.Entity;
using Library_BackEnd.Models.ViewModel;
using Library_BackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library_BackEnd.Controllers
{
    public class LibraryController : Controller
    {

        private readonly BookService _bookService;

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
                    IsAvailable = book.IsAvailable,
                    CoverImageUrl = book.CoverImageUrl
                });
            }

            return View(bookViewModels);
        }

        public IActionResult Form(Book book)
        {
            return View(book);
        }

        [HttpPost]
        public IActionResult CreateBook(Book book)
        {

            if (!ModelState.IsValid)
            {
                return View("Form", book);
            }

            book.Id = Guid.NewGuid();

            _bookService.CreateBook(book);

            TempData["Message"] = "Prodotto Creato";
            return RedirectToAction("Index");
        }
    }
}
