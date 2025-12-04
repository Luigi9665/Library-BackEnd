using Library_BackEnd.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Library_BackEnd.Services
{
    public class RentRecordService
    {

        public ApplicationDbContext _context;

        public RentRecordService(ApplicationDbContext context)
        {
            _context = context;
        }

        public RentRecord GetRentRecordsByBookId(Guid bookId)
        {
            return _context.RentRecords.Where(r => r.BookId == bookId)
                .FirstOrDefault();
        }

    }
}
