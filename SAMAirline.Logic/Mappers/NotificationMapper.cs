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
    public class NotificationMapper : IMapper<NotificationDto, Notification>
    {
        public Notification MapToDalEntity(NotificationDto dtoEntity)
        {
            return new Notification
            {
                NotificationId = dtoEntity.NotificationId,
                UserId = dtoEntity.UserId,
                Message = dtoEntity.Message
            };
        }

        public NotificationDto MapToDtoEntity(Notification dalEntity)
        {
            return new NotificationDto
            {
                NotificationId = dalEntity.NotificationId,
                UserId = dalEntity.UserId,
                Message = dalEntity.Message
            };
        }
    }
}
