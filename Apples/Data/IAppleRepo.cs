using Apple.Models;

namespace Apple.Data
{
    ////////////////////////////////////////////////////// DOCUMENT THIS
    public interface IAppleRepo
    {
        User GetUser(string userName);
        User AddUser(User user);
        bool ValidUser(string userName, string password);
        void SaveChanges();
    }
}
