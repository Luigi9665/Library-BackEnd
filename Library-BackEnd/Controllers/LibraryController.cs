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

        private readonly RentRecordService _rentRecordService;

        private readonly IWebHostEnvironment _env;

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
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    IsAvailable = b.IsAvailable,
                    CreatedAt = b.CreatedAt,
                    CategoryId = b.CategoryId
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

        private async Task<string> SaveImage(IFormFile imageFile)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "Uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using FileStream fileStream = new FileStream(filePath, FileMode.Create);

            await imageFile.CopyToAsync(fileStream);

            return "/Uploads/" + uniqueFileName;
        }

        public LibraryController(BookService bookService, CategoryService categoryService, IWebHostEnvironment env, RentRecordService rentRecordService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _env = env;
            _rentRecordService = rentRecordService;
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

            Book book = new Book
            {
                Id = fb.Id,
                Title = fb.Title,
                Author = fb.Author,
                IsAvailable = fb.IsAvailable,
                CoverImageUrl = fb.CoverImage != null ? await SaveImage(fb.CoverImage) : "",
                CreatedAt = fb.CreatedAt,
                CategoryId = fb.CategoryId
            };

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

        public IActionResult Details(Guid id)
        {
            RentRecord rentRecord = _rentRecordService.GetRentRecordsByBookId(id);


            return View();
        }
    }
}
