using SAMAirline.Common;
using SAMAirline.DataProvider.Entities;
using SAMAirline.DataProvider.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAMAirline.DataProvider.Repositories
{
    public class PassengerRepository : BaseRepository<User>, IPassengerRepository
    {
        public PassengerRepository(AirlineDB context) : base(context)
        {
        }

        public IEnumerable<User> GetDefaultUsers()
        {
            return DbSet.Where(p => p.Role != Constants.AdminRole);
        }

        public User GetPassenger(int passengerId)
        {
            var passenger = DbSet
                .Where(p => p.UserId == passengerId)
                .FirstOrDefault();

            if (passenger == null)
                throw new ApplicationException($"Can't find passenger with id = {passengerId}");

            return passenger;
        }

        public User GetByEmail(string email)
        {
            var passenger = DbSet
                .Where(p => p.Email == email)
                .FirstOrDefault();

            if (passenger == null)
                throw new ApplicationException($"Can't find passenger with email = {email}");

            return passenger;
        }

        public bool Login(string email, string password)
        {
            string passwordHash = Crypto.Sha256(password + email);
            return DbSet.Any(p => p.Email == email && p.Password == passwordHash);
        }
        
        public User Registration(User model)
        {
            var passenger = DbSet.Add(new User
            {
                ProfileImage = null,
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Password = Crypto.Sha256(model.Password + model.Email),
                Role = Constants.UserRole,
                IsConfirmed = false
            });
            Context.SaveChanges();
            return passenger;
        }

        public User Delete(int userId)
        {
            var passenger = DbSet
                .Where(p => p.UserId == userId)
                .FirstOrDefault();

            if(passenger == null)
                throw new ApplicationException($"Can't find passenger with id = {userId}");

            DbSet.Remove(passenger);
            return passenger;
        }

        public User EditInfo(User model)
        {
            var passenger = DbSet
                .Where(p => p.UserId == model.UserId)
                .FirstOrDefault();

            if (passenger == null)
                throw new ApplicationException($"Can't find passenger with id = {model.UserId}");

            passenger.Name = model.Name;
            passenger.Surname = model.Surname;
            return passenger;
        }

        public User ChangePassword(User model)
        {
            var passenger = DbSet
                .Where(p => p.UserId == model.UserId)
                .FirstOrDefault();

            if (passenger == null)
                throw new ApplicationException($"Can't find passenger with id = {model.UserId}");

            passenger.Password = Crypto.Sha256(model.Password + passenger.Email);

            Context.SaveChanges();

            return passenger;
        }

        public bool IsAdmin(string email)
        {
            var passenger = DbSet
                .Where(p => p.Email == email)
                .FirstOrDefault();

            if (passenger == null)
                throw new ApplicationException($"Can't find passenger with email = {email}");

            return passenger.Role == Constants.AdminRole? true : false;
        }

        public bool IsExists(string email)
        {
            return DbSet.Any(p => p.Email == email);
        }

        public IEnumerable<User> GetPassengersOffset(int pageNumber, int pageSize)
        {
            var passengers = DbSet
                .Where(p => p.Role != Constants.AdminRole)
                .Skip((pageNumber) * pageSize)
                .Take(pageSize)
                .OrderBy(t => t.UserId);

            return passengers;
        }

        public void IsThereAccess(int userId)
        {
            if (userId != Constants.AdminId)
                throw new ApplicationException($"User with id = {userId} does not have access to work with passengers");
        }

        public User UploadProfileImage(int userId, byte[] image)
        {
            var passenger = DbSet
                .Where(p => p.UserId == userId)
                .FirstOrDefault();

            if (passenger == null)
                throw new ApplicationException($"Can't find passenger with id = {userId}");

            passenger.ProfileImage = image;
            Context.SaveChanges();
            return passenger;
        }

        public void ConfirmEmail(string email)
        {
            var passenger = DbSet
                .Where(p => p.Email == email)
                .FirstOrDefault();

            if (passenger == null)
                throw new ApplicationException($"Can't find passenger with email = {email}");

            passenger.IsConfirmed = true;

            Context.SaveChanges();
        }
    }
}
