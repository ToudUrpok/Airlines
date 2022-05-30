using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMAirline.Model.Models
{
    public class RegistrationViewModel
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "NameRequired")]
        [Display(Name = "Name", ResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "IncorrectName")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "SurnameRequired")]
        [Display(Name = "Surname", ResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessageResourceType = typeof(Resources.Resource),
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
        [StringLength(256, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordLenth")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Resource))]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordConfirmation")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; }

        public string Message { get; set; }
    }
}
