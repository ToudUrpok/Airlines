using SAMAirline.DataProvider.Entities;
using System.Collections.Generic;

namespace SAMAirline.DataProvider.Interfaces
{
    public interface IPassengerRepository : IRepository<User>
    {
        User GetByEmail(string email);
        User GetPassenger(int passengerId);
        bool Login(string email, string password);
        User Registration(User model);
        User Delete(int userId);
        User EditInfo(User model);
        User ChangePassword(User model);
        IEnumerable<User> GetDefaultUsers();
        IEnumerable<User> GetPassengersOffset(int pageNumber, int pageSize);
        User UploadProfileImage(int userId, byte[] image);
        bool IsExists(string email);
        bool IsAdmin(string email);
        void IsThereAccess(int userId);
        void ConfirmEmail(string email);
    }
}
