using SAMAirline.DataProvider.Entities;
using SAMAirline.Logic.Interfaces;
using SAMAirline.Model.Models;
using SAMAirline.Model.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMAirline.Logic.Mappers
{
    public class UserMapper : IMapper<UserDto, User>
    {
        public User MapToDalEntity(UserDto dtoEntity)
        {
            return new User
            {
                UserId = dtoEntity.UserId,
                ProfileImage = dtoEntity.ProfileImage,
                Name = dtoEntity.Name,
                Surname = dtoEntity.Surname,
                Email = dtoEntity.Email,
                Password = dtoEntity.Password,
                Role = dtoEntity.Role,
                IsConfirmed = dtoEntity.IsConfirmed
            };
        }

        public UserDto MapToDtoEntity(User dalEntity)
        {
            return new UserDto
            {
                UserId = dalEntity.UserId,
                ProfileImage = dalEntity.ProfileImage,
                Name = dalEntity.Name,
                Surname = dalEntity.Surname,
                Email = dalEntity.Email,
                Password = dalEntity.Password,
                Role = dalEntity.Role,
                IsConfirmed = dalEntity.IsConfirmed
            };
        }
    }
}
