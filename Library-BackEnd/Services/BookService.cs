using Library_BackEnd.Models.Entity;

namespace Library_BackEnd.Services
{
    public class BookService
    {

        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public bool CreateBook(Book book)
        {
            _context.Books.Add(book);
            return _context.SaveChanges() > 0;
        }

        public Book? GetBookById(Guid id)
        {
            return _context.Books.FirstOrDefault(b => b.Id == id);
        }

        public bool UpdateBook(Book book)
        {

            var existingBook = GetBookById(book.Id);

            if (existingBook == null)
            {
                return false;
            }

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Genre = book.Genre;
            existingBook.IsAvailable = book.IsAvailable;
            existingBook.CoverImageUrl = book.CoverImageUrl;
            existingBook.CreatedAt = book.CreatedAt;

            return _context.SaveChanges() > 0;
        }

        public bool DeleteBook(Guid id)
        {
            var book = GetBookById(id);
            if (book == null)
            {
                return false;
            }
            _context.Books.Remove(book);
            return _context.SaveChanges() > 0;
        }

    }
}
