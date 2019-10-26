using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UTW_Project.Resources;

namespace UTW_Project.ViewModels
{
    public class UNameEmail
    {
        [Display(Name = "UserName", ResourceType = typeof(MyTexts))]
        public string Username { get; set; }
        [Display(Name = "EmailAddress", ResourceType = typeof(MyTexts))]
        public string Email { get; set; }
    }
}