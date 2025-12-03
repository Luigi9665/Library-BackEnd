using Library_BackEnd.Models.Entity;

namespace Library_BackEnd.Services
{
    public class RentRecordService
    {

        public ApplicationDbContext _context;

        public RentRecordService(ApplicationDbContext context)
        {
            _context = context;
        }


    }
}
