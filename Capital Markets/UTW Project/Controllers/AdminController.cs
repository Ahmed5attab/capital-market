using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTW_Project.Models;
using UTW_Project.ViewModels;

namespace UTW_Project.Controllers
{
    public class AdminController : Controller
    {
        private Capital_MarketEntities5 DataContext = new Capital_MarketEntities5();

        public ActionResult Index(AdminUsersFilterModel filter)
        {
            if(!Session["Role"].Equals("Admin"))
            {
                return View("notAuthorized");
            }
            var viewModel = new UsersFilterViewModel()
            {
                Persons = new List<Person>(),
                Filter = new AdminUsersFilterModel()
            }; // 

            var persons = DataContext.Persons.ToList(); 
            if(filter.IsLocked != null) 
            {

                persons = persons.Where(x=>x.IsLocked == filter.IsLocked).ToList();
            }
            if(!string.IsNullOrEmpty(filter.UserName))
            {

                persons = persons.Where(x => x.UserName.Contains(filter.UserName)).ToList();
            }
            if (!string.IsNullOrEmpty(filter.Email))
            {
                persons = persons.Where(x => x.Email.Contains(filter.Email)).ToList();
            } 
            viewModel.Persons = persons;
            viewModel.Filter = filter;
            return View(viewModel);


        }
        // GET: Admin
        //public ActionResult Index(string SearchBy, string Search)
        //{
        //    if (SearchBy == "Email")
        //    {
        //        return View(DataContext.Persons.Where(X => X.Email == Search || Search==null).ToList());
        //    }
        //    if(SearchBy == "UserName")
        //    {
        //        return View(DataContext.Persons.Where(X => X.UserName == Search || Search == null).ToList());
        //    }
        //    else
        //    {
        //        return View(DataContext.Persons.Where(X => X.IsLocked.ToString() == Search || Search == null).ToList());
        //    }
        //}

        public ActionResult Edit(int ID)
        {
            if (!Session["Role"].Equals("Admin"))
            {
                return View("notAuthorized");
            }
            var personInDB = DataContext.Persons.FirstOrDefault(o => o.ID == ID);

            if (personInDB.IsLocked == 0)
                personInDB.IsLocked = 1;
            else
                personInDB.IsLocked = 0;

            DataContext.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult Edit(Person person)
        {


            //DataContext.Entry(person.IsLocked.ToString()).State = System.Data.Entity.EntityState.Modified;
            //DataContext.SaveChanges();

           
            return RedirectToAction("Index");
        }



    }
}

