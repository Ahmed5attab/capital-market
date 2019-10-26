using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTW_Project.Resources;

namespace UTW_Project.Models
{
    public class PersonLoginModel
    {
        [Display(Name = "UserName", ResourceType = typeof(MyTexts))]
        [Required(ErrorMessageResourceType = typeof(MyTexts), ErrorMessageResourceName = "UserNameRequired")]
        [StringLength(8, ErrorMessageResourceType = typeof(MyTexts), ErrorMessageResourceName = "UserNameLength")]
        public string UserName { get; set; }
        
        //[StringLength(50)]
        [Display(Name = "Password", ResourceType = typeof(MyTexts))]
        [Required(ErrorMessageResourceType = typeof(MyTexts), ErrorMessageResourceName = "PasswordRequired")]
        public string Password { get; set; }  
    }
}