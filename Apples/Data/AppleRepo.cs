using Microsoft.EntityFrameworkCore.ChangeTracking;
using Apple.Models;

namespace Apple.Data
{
    ////////////////////////////////////////////////////// DOCUMENT THIS
    public class AppleRepo : IAppleRepo
    {
        private AppleDBContext _dbContext;
        public AppleRepo(AppleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        ////////////////////////////////////////////////////// DOCUMENT THIS
        public User GetUser(string id)
        {
            User user = _dbContext.Users.FirstOrDefault(e => e.UserName == id);
            return user;
        }

        ////////////////////////////////////////////////////// DOCUMENT THIS
        public User AddUser(User user)
        {
            EntityEntry<User> e = _dbContext.Users.Add(user);
            User u = e.Entity;
            _dbContext.SaveChanges();
            return u;
        }

        ////////////////////////////////////////////////////// DOCUMENT THIS
        public bool ValidUser(string userName, string password)
        {
            User u = _dbContext.Users.FirstOrDefault(e => e.UserName == userName && e.Password == password);
            if (u == null)
                return false;
            else
                return true;
        }

        ////////////////////////////////////////////////////// DOCUMENT THIS
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
