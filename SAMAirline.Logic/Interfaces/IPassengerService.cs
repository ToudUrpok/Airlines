using SAMAirline.Model.Models;
using SAMAirline.Model.ModelsDto;
using System.Collections.Generic;

namespace SAMAirline.Logic.Interfaces
{
    public interface IPassengerService
    {
        IEnumerable<UserDto> GetAll();
        UserDto GetById(int Id);
        UserDto GetByEmail(string email);
        LoginViewModel Login(LoginViewModel model);
        RegistrationViewModel Registration(RegistrationViewModel model);
        void Delete(int userId);
        UserDto EditInfo(EditUserInfoViewModel model);
        UserDto ChangePassword(EditUserInfoViewModel model);
        PaginationModel GetPassengersOffset(PaginationModel model);
        UserDto UploadProfileImage(int userId, byte[] image);
        bool IsExists(int id);
        bool IsExists(string email);
        bool IsAdmin(string email);
        void IsThereAccess(int userId);
        void ConfirmEmail(string email);
    }
}
