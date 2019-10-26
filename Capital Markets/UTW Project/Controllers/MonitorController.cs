using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTW_Project.Models;

namespace UTW_Project.Controllers
{
    public class MonitorController : Controller
    {
        private Capital_MarketEntities5 DataContext = new Capital_MarketEntities5();

        // GET: Orders        
        public ActionResult Orders()
        {
            int ID = Convert.ToInt16(Session["ID"]);
            Person user = DataContext.Persons.Where(a => a.ID == ID).FirstOrDefault();
            if (user.UserTypeID == 1)
            {
                return View(new OrdersData { user = DataContext.Persons.Where(u => u.UserTypeID == 2).ToList(), stock = DataContext.Stocks.ToList(), order = DataContext.Orders.ToList() });
            }

            return View(new OrdersData { stock = DataContext.Stocks.ToList(), order = DataContext.Orders.Where(a => a.ClientID == user.ID).ToList() });

        }

        public ActionResult OrdersDate(DateTime? start, DateTime? end, string username, string stockName)
        {
            ViewBag.there = 1;
            ViewBag.start = start;
            ViewBag.end = end;
            ViewBag.username = username;
            ViewBag.stock = stockName;

            int ID = Convert.ToInt16(Session["ID"]);
            Person user = DataContext.Persons.Where(a => a.ID == ID).FirstOrDefault();
            OrdersData list = new OrdersData { };
            OrdersData data = new OrdersData { };

            if (user.UserTypeID == 1)
            {
                list = new OrdersData { user = DataContext.Persons.Where(u => u.UserTypeID == 2).ToList(), stock = DataContext.Stocks.ToList(), order = DataContext.Orders.ToList() };
            }
            else
            {
                list = new OrdersData { stock = DataContext.Stocks.ToList(), order = DataContext.Orders.Where(a => a.ClientID == user.ID).ToList() };
            }
            data.stock = list.stock;
            data.user = list.user;
            if (username != "All Users" && username != null)
            {
                foreach (var item in list.order)
                {
                    if (item.Person.UserName == username)
                    {
                        if (data.order == null)
                            data.order = new List<Order>();

                        data.order.Add(item);

                        if ((stockName != "All Stocks") && item.Stock.CompanyName != stockName)
                            data.order.Remove(item);

                    }
                }

            }
            if (stockName != "All Stocks" && (username == "All Users" || username == null))
            {
                data.order = null;
                foreach (var item in list.order)
                {
                    if (item.Stock.CompanyName == stockName)
                    {
                        if (data.order == null)
                            data.order = new List<Order>();
                        data.order.Add(item);
                    }
                }
            }
            if (data.order == null)
            {
                data.order = list.order;
            }
            if (start != null)
            {
                list.order = null;
                foreach (var item in data.order)
                {
                    if (item.OrderDate >= start)
                    {
                        if (list.order == null)
                            list.order = new List<Order>();
                        list.order.Add(item);
                    }
                }
                data.order = list.order;
            }
            if (end != null && data.order != null)
            {
                list.order = null;
                foreach (var item in data.order)
                {
                    if (item.OrderDate <= end)
                    {

                        if (list.order == null)
                            list.order = new List<Order>();
                        list.order.Add(item);
                    }
                }
                data.order = list.order;
            }
            return View("Orders", data);
        }



    }

}