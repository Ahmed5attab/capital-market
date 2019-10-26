using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UTW_Project.Models;

namespace UTW_Project.ViewModels
{
    public class UsersFilterViewModel
    {
        public IEnumerable<Person> Persons { get; set; }
        public AdminUsersFilterModel Filter { get; set; }
    }
}