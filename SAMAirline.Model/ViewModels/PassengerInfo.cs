using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMAirline.Model.Models
{
    public class PassengerInfo
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "NameRequired")]
        [Display(Name = "PassName", ResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "IncorrectName")]
        public string PassengerName { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "SurnameRequired")]
        [Display(Name = "PassSurname", ResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "IncorrectSurname")]
        public string PassengerSurname { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "NationalityRequired")]
        [Display(Name = "PassNationality", ResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "IncorrectNationality")]
        public string PassengerNationality { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "BirthdateRequired")]
        [Display(Name = "PassBirthdate", ResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"[0-9]{2}/[0-9]{2}/[0-9]{4}", ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "IncorrectBirthdate")]
        public string PassengerBirthdate { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "PassportRequired")]
        [Display(Name = "PassPassport", ResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"[A-Z]{2}[0-9]{7}", ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "IncorrectPassport")]
        public string PassengerPassport { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "ExpireRequired")]
        [Display(Name = "PassExpire", ResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"[0-9]{2}/[0-9]{2}", ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "IncorrectExpire")]
        public string PassportExpire { get; set; }
    }
}
