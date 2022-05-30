using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMAirline.Model.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }

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

        public string Message { get; set; }

        public bool IsConfirmed { get; set; }
    }
}
