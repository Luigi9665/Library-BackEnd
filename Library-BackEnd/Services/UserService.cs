using Library_BackEnd.Models.Entity;

namespace Library_BackEnd.Services
{
    public class UserService
    {

        private ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }



    }
}
