using SAMAirline.Common;
using SAMAirline.DataProvider.Entities;
using SAMAirline.DataProvider.Interfaces;
using SAMAirline.DataProvider.Repositories;
using SAMAirline.Logic.Interfaces;
using SAMAirline.Model.Models;
using SAMAirline.Model.ModelsDto;
using SAMAirline.Model.PaginationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMAirline.Logic.Services
{
    public class PassengerService : IPassengerService
    {
        public readonly IPassengerRepository _passengerRepository;
        public readonly IBookingRepository _bookingRepository;
        public readonly INotificationRepository _notificationRepository;
        private readonly IMapper<UserDto, User> _mapper;

        public PassengerService(
            IPassengerRepository passengerRepository,
            IBookingRepository bookingRepository,
            INotificationRepository notificationRepository,
            IMapper<UserDto, User> mapper)
        {
            _passengerRepository = passengerRepository;
            _bookingRepository = bookingRepository;
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public IEnumerable<UserDto> GetAll()
        {
            var list = _passengerRepository.GetDefaultUsers();
            var passengers = list.Select(p => _mapper.MapToDtoEntity(p)).ToList();

            return passengers;
        }

        public UserDto GetById(int Id)
        {
            var p = _passengerRepository.GetById(Id);
            var passenger = _mapper.MapToDtoEntity(p);
            return passenger;
        }

        public UserDto GetByEmail(string email)
        {
            var p = _passengerRepository.GetByEmail(email);
            var passenger = _mapper.MapToDtoEntity(p);
            return passenger;
        }

        public bool IsExists(string email)
        {
            return _passengerRepository.IsExists(email);
        }

        public bool IsExists(int id)
        {
            return _passengerRepository.IsExists(id);
        }

        public LoginViewModel Login(LoginViewModel model)
        {
            if (!_passengerRepository.Login(model.Email, model.Password))
                return new LoginViewModel() { Message = Resources.Resource.LogError };
            model.IsConfirmed = _passengerRepository.GetByEmail(model.Email).IsConfirmed;
            return model;
        }

        public RegistrationViewModel Registration(RegistrationViewModel model)
        {
            if (_passengerRepository.IsExists(model.Email))
                return new RegistrationViewModel() { Message = Resources.Resource.RegError };
            var p = _passengerRepository
                .Registration(new User
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    Password = model.Password,
                    Role = Constants.UserRole
                });
            model.Id = p.UserId;
            model.Role = p.Role;

            return model;
        }

        public UserDto EditInfo(EditUserInfoViewModel model)
        {
            var passenger = _passengerRepository.GetPassenger(model.Id);
            passenger.Name = model.Name;
            passenger.Surname = model.Surname;
            return _mapper.MapToDtoEntity(_passengerRepository.EditInfo(passenger));
        }

        public UserDto ChangePassword(EditUserInfoViewModel model)
        {
            var passenger = _passengerRepository.GetPassenger(model.Id);
            passenger.Password = model.NewPassword;
            return _mapper.MapToDtoEntity(_passengerRepository.ChangePassword(passenger));
        }

        public bool IsAdmin(string email)
        {
            return _passengerRepository.IsAdmin(email);
        }

        public void Delete(int userId)
        {
            if (!_passengerRepository.IsExists(userId))
                throw new ApplicationException($"Can't find user with id = {userId}");
            _bookingRepository.DeleteAllBookings(userId);
            _notificationRepository.DeleteAllNotifications(userId);
            _passengerRepository.Delete(userId);
        }

        public PaginationModel GetPassengersOffset(PaginationModel model)
        {
            var list = _passengerRepository.GetDefaultUsers();
            var pl = _passengerRepository.GetPagedList(list, model.UserList.PageNumber, model.UserList.PageSize, model.IsAdmin);

            var passengerList = new PagedList<UserDto>
            {
                RowsCount = pl.RowsCount,
                PagesCount = pl.PagesCount,
                PageSize = pl.PageSize,
                PageNumber = pl.PageNumber,
                Data = pl.Data.Select(p => _mapper.MapToDtoEntity(p)).ToList(),
                Message = pl.Message
            };

            model.UserList = passengerList;
            return model;
        }

        public void IsThereAccess(int userId)
        {
            _passengerRepository.IsThereAccess(userId);
        }

        public UserDto UploadProfileImage(int userId, byte[] image)
        {
            var p = _passengerRepository.UploadProfileImage(userId, image);
            var passenger = _mapper.MapToDtoEntity(p);
            return passenger;
        }

        public void ConfirmEmail(string email)
        {
            _passengerRepository.ConfirmEmail(email);
        }
    }
}
