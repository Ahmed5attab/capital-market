using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using UTW_Project.Resources;

namespace UTW_Project.Models
{
    public class PersonRegModel
    {
        public int ID { get; set; }

        [Display (Name ="FirstName",ResourceType =typeof(MyTexts))]
        [Required(ErrorMessageResourceType =typeof(MyTexts),ErrorMessageResourceName ="FirstNameRequired")]
        [StringLength(20 ,ErrorMessageResourceType =typeof(MyTexts), ErrorMessageResourceName ="FirstNameLength")]
        public string FirstName { get; set; }

        [Display(Name = "MobileNo", ResourceType = typeof(MyTexts))]
        [Required(ErrorMessageResourceType = typeof(MyTexts), ErrorMessageResourceName = "MobileNoRequired")]
        [RegularExpression(@"^\d{3}-\d{4}-\d{4}$", ErrorMessageResourceType = typeof(MyTexts), ErrorMessageResourceName = "MobileNoisNotValied")]
        public string MobileNo { get; set; }

        [Display(Name = "EmailAddress", ResourceType = typeof(MyTexts))]
        [Required(ErrorMessageResourceType = typeof(MyTexts), ErrorMessageResourceName = "EmailRequired")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessageResourceType = typeof(MyTexts), ErrorMessageResourceName = "EmailisNotValied")]
        [EmailAddress(ErrorMessageResourceType = typeof(MyTexts), ErrorMessageResourceName = "EmailisNotValied")]
        public string Email { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(MyTexts))]
        [Required(ErrorMessageResourceType = typeof(MyTexts), ErrorMessageResourceName = "LastNameRequired")]
        [StringLength(20, ErrorMessageResourceType = typeof(MyTexts), ErrorMessageResourceName = "LastNameLength")]
        public string LastName { get; set; }

        [Display(Name = "UserName", ResourceType = typeof(MyTexts))]
        [Required(ErrorMessageResourceType = typeof(MyTexts), ErrorMessageResourceName = "UserNameRequired")]
        [StringLength(20, ErrorMessageResourceType = typeof(MyTexts), ErrorMessageResourceName = "UserNameLength")]
        public string UserName { get; set; }

        [Display(Name = "ChooseQuestion", ResourceType = typeof(MyTexts))]
        [Required(ErrorMessageResourceType = typeof(MyTexts), ErrorMessageResourceName = "QuestionRequired")]
        public int QuestionID { get; set; }

        [Display(Name = "Answer", ResourceType = typeof(MyTexts))]
        [Required(ErrorMessageResourceType = typeof(MyTexts), ErrorMessageResourceName = "AnswerRequired")]
        public string Answer { get; set; }

        [Display(Name = "Status", ResourceType = typeof(MyTexts))]
        public int IsLocked { get; set; }

        [Required(ErrorMessageResourceType = typeof(MyTexts), ErrorMessageResourceName = "PasswordRequired")]
        [Display(Name = "Password", ResourceType = typeof(MyTexts))]
        //[RegularExpression(@" ^ (?=.*[a - z])(?=.*[A - Z])(?=.*\d)(?=.*[$@$!% *? &])[A - Za - z\d$@$!% *? &]{8, 10}", ErrorMessageResourceType = typeof(MyTexts), ErrorMessageResourceName = "PasswordNotValied")]
        public string Password { get; set; }
        public int UserTypeID { get; set; }
        public int LoginCount { get; set; }
        public string languageAbbreviation { get; set; }

    }
}


