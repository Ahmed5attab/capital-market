using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UTW_Project.Resources;


namespace UTW_Project.Models
{
    public class AdminUsersFilterModel
    {
        [Display(Name = "Status", ResourceType = typeof(MyTexts))]
        public int? IsLocked { get; set; }
        [Display(Name = "EmailAddress", ResourceType = typeof(MyTexts))]
        public string Email { get; set; }
        [Display(Name = "UserName", ResourceType = typeof(MyTexts))]
        public string UserName { get; set; }
    }
}