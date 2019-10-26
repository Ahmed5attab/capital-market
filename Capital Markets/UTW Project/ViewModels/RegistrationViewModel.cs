using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UTW_Project.Models;
using UTW_Project.Resources;

namespace UTW_Project.ViewModels
{
    public class RegistrationViewModel
    {
        //public Person Person{ get; set; }

        public PersonRegModel Person { get; set; }
        public IEnumerable<Question> Questions { get; set; }
    }

}