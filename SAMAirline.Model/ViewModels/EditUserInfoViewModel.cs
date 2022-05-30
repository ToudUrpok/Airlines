using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMAirline.Model.Models
{
    public class EditUserInfoViewModel
    {
        [Required]
        public int Id { get; set; }

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

        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordRequired")]
        [StringLength(256, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordLenth")]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(Resources.Resource))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Resource))]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordConfirmation")]
        public string ConfirmPassword { get; set; }
    }
}
