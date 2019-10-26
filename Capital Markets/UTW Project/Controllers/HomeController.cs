using CaptchaMvc.HtmlHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UTW_Project.Models;
using UTW_Project.ViewModels;

namespace UTW_Project.Controllers
{
    public class HomeController : Controller
    {
        static int num1 = 4561823;
        static int num2 = 9823571;
        private Capital_MarketEntities5 DataContext = new Capital_MarketEntities5();


        public int Encode(int Unique)
        {

            return num1 + Unique + num2;
        }
        public int decode(int Unique)
        {
            return (Unique - num1) - num2;
        }
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
        //public ActionResult Registration()
        //{
        //    var Questions = DataContext.Questions.ToList();
        //    var QuestionsList = new List<Question>();
        //    foreach (var item in Questions)
        //    {
        //        QuestionsList.Add(new Question()
        //        {
        //            ID = item.ID,
        //            Question1 = item.Question1
        //        });
        //    }
        //    var viewModel = new RegistrationViewModel()
        //    {
        //        Person = new Person(),
        //        Questions = QuestionsList
        //    };
        //    return View(viewModel);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Registration(Person person)
        //{
        //    person.UserTypeID = 2; // Person
        //    person.IsLocked = 0;
        //    person.Password = HashPass(person.Password);
        //    person.Answer = HashPass(person.Answer);


        //    DataContext.Persons.Add(person);
        //    DataContext.SaveChanges();
        //    return RedirectToAction("Index");

        public ActionResult Registration()
        {
            List<Question> Questions = DataContext.Questions.ToList();
            var QuestionsList = new List<Question>();
            foreach (var item in Questions)
            {
                QuestionsList.Add(new Question()
                {
                    ID = item.ID,
                    Question1 = item.Question1
                });
            }
            var viewModel = new RegistrationViewModel()
            {
                Person = new PersonRegModel(),
                Questions = QuestionsList
            };
            return View(viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(PersonRegModel Person)
        {

            var stringss = Person.Password;
            DataContext.Persons.Add(new Person()
            {
                UserTypeID = 2,
                IsLocked = 0,
                Password = HashPass(Person.Password),
                Answer = HashPass(Person.Answer),
                UserName = Person.UserName,
                QuestionID = Person.QuestionID,
                LastName = Person.LastName,
                Email = Person.Email,
                MobileNo = Person.MobileNo,
                FirstName = Person.FirstName,
                LoginCount = Person.LoginCount

            });
            DataContext.SaveChanges();

            ViewBag.MsgTrue = UTW_Project.Resources.MyTexts.RegisteredDone;
            return View("LogIn");
        }
        public string HashPass(string password)
        {

            byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

            return encoded;//returns hashed version of password
        }

        public ActionResult LogIn()
        {
            return View(new PersonLoginModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult LogIn(PersonLoginModel person)
        {
            if (ModelState.IsValid)
            {

                var obj = DataContext.Persons.Where(a => a.UserName.Equals(person.UserName)).FirstOrDefault();
                if (obj != null)
                {
                    if (obj.IsLocked == 1)
                    {
                        ViewData["Message"] = @UTW_Project.Resources.MyTexts.YouAreLocked;
                        return View();
                    }

                    if (obj.Password == HashPass(person.Password) && obj.LoginCount >= 3 && obj.IsLocked == 0)
                    {

                        if (this.IsCaptchaValid(""))
                        {
                            string type = signin(obj);
                            if (type == "Iser")
                                return RedirectToAction("Dashboard", "Dashboard");
                            else
                                return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewData["Message"] = @UTW_Project.Resources.MyTexts.InvaliedCaptcha;
                            obj.LoginCount++;
                            DataContext.SaveChanges();
                            ViewBag.show = 1;
                            return View();
                        }

                    }
                    if (obj.Password == HashPass(person.Password) && obj.IsLocked != 1)
                    {
                        string type = signin(obj);
                        if (type == "User")
                            return RedirectToAction("Dashboard", "Dashboard");
                        else
                            return RedirectToAction("Index");

                    }
                    else
                    {
                        obj.LoginCount++;
                        DataContext.SaveChanges();

                        if (obj.LoginCount >= 5)
                        {
                            obj.IsLocked = 1;
                            obj.LoginCount = 0;
                            DataContext.SaveChanges();
                            ViewData["Message"] = @UTW_Project.Resources.MyTexts.YouAreLocked;
                            return View();

                        }
                        if (obj.LoginCount >= 3)
                        {

                            ViewBag.show = 1;
                        }
                        ViewData["Message"] = @UTW_Project.Resources.MyTexts.InvaliedPass;
                        return View();
                    }


                }
            }
            ViewData["Message"] = @UTW_Project.Resources.MyTexts.InvalidUsernameorPassword;

            return View();
        }
        public string signin(Person obj)
        {
            Session["ID"] = obj.ID.ToString();
            Session["UserName"] = obj.UserName;
            UserType role = DataContext.UserTypes.Where(c => c.ID == obj.UserTypeID).FirstOrDefault();
            if (obj.LoginCount != 0)
            {
                obj.LoginCount = 0;

                DataContext.SaveChanges();
            }
            if (role.UserType1.Equals("Admin"))
            {
                Session["Role"] = "Admin";
                return "Admin";
            }
            else
            {
                Session["Role"] = "User";
                return "User";
            }
            
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return View("Index");
        }
        // password reset view (Entering the e-mail)
        public ActionResult PasswordReset()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PasswordReset(string Username, string Email)
        {

            Person user = DataContext.Persons.Where(a => a.UserName == Username).FirstOrDefault();
            if (user == null)
            {
                ViewBag.Msg = UTW_Project.Resources.MyTexts.UNNotExist ;
                return View("PasswordReset");
            }
            if (user.Email.Equals(Email))
            {
                ResetPasswordRequest n = new ResetPasswordRequest();

                n.ClientID = user.ID;
                n.RequestDate = DateTime.Now;
                // delete all requests for the same user
                DataContext.ResetPasswordRequests.RemoveRange(DataContext.ResetPasswordRequests.Where(a => a.ClientID == n.ClientID).ToList());
                //add the new request
                DataContext.ResetPasswordRequests.Add(n);
                DataContext.SaveChanges();
                SendMail(user, n.id);
                ViewBag.MsgTrue = UTW_Project.Resources.MyTexts.CheckSent;
                return View("PasswordReset");
            }
            else
            {
                ViewBag.Msg = UTW_Project.Resources.MyTexts.WrongMail;
                return View("PasswordReset");
            }

        }

        private void SendMail(Person c, int identifier)
        {


            string encoded = Convert.ToString(Encode(identifier));

            SmtpClient Conn = new SmtpClient("smtp.gmail.com", 587);
            Conn.DeliveryMethod = SmtpDeliveryMethod.Network;
            Conn.UseDefaultCredentials = false;
            Conn.EnableSsl = true;
            Conn.Credentials = new NetworkCredential("mohamedwalaa9229@gmail.com", "01118034187");
            string to = c.Email.ToString();
            string from = "mohamedwalaa9229@gmail.com";
            MailMessage ms = new MailMessage(from, to);
            ms.Subject = " Reset Password ";
            ms.Body = string.Format("Here is a link to Reset Your password " +
                                    "http://localhost:39134/Home/ChangePassword?id=" +
                                    encoded + "   Note that the Link will expire after it's opened or after 24 hours  "

                );


            Conn.Send(ms);
        }
        // after pressing the link of sent Mail
        public ActionResult ChangePassword(string id)
        {
            int decoded = decode(Convert.ToInt32(id));
            var Req = DataContext.ResetPasswordRequests.Where(a => a.id == decoded).FirstOrDefault();
            var z = DateTime.Now;
            if (Req == null)
            {
                return View("ChangePasswordExpire");
            }
            var y = Req.RequestDate;
            double x = (z - y).TotalHours;
            if (Req.Flag != 1 && x < 24)
            {
                Req.Flag = 1;
                Person b = DataContext.Persons.Find(Req.ClientID);
                Question q = DataContext.Questions.Where(m => m.ID == b.QuestionID).FirstOrDefault();
                ViewBag.Q = q.Question1;
                DataContext.SaveChanges();
                b.Answer = "";
                return View("ChangePasswordSucc", b);

            }
            else
            {
                return View("ChangePasswordExpire");
            }
        }
        //submit the new password
        public ActionResult updatepassword(Person C)
        {

            var user = DataContext.Persons.Find(C.ID);
            string ans = HashPass(C.Answer);
            if (!ans.Equals(user.Answer))
            {
                ViewBag.Msg = UTW_Project.Resources.MyTexts.WrongAnswer;
                return View("ChangePasswordSucc", C);
            }
            user.Password = HashPass(C.Password);
            DataContext.SaveChanges();
            ViewBag.MsgTrue = UTW_Project.Resources.MyTexts.PasswordChangedSuccessfully;
            return View("LogIn");
        }

    }
}