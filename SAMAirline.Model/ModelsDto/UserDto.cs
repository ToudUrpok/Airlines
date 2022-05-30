using SAMAirline.Model.Models;
using System.ComponentModel.DataAnnotations;

namespace SAMAirline.Model.ModelsDto
{
    public class UserDto : EditUserInfoViewModel
    {
        public int UserId { get; set; }

        public byte[] ProfileImage { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "NameRequired")]
        [Display(Name = "Name", ResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "IncorrectName")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "SurnameRequired")]
        [Display(Name = "Surname", ResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "IncorrectSurname")]
        public string Surname { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "EmailRequired")]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "IncorrectEmail")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "PasswordRequired")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Role { get; set; }
        public bool IsConfirmed { get; set; }

        public NotificationWindowViewModel Notifications { get; set; }
    }
}
