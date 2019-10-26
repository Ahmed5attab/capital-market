using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTW_Project.Models;

namespace UTW_Project.Controllers
{
    public class DashboardController : Controller
    {
        private Capital_MarketEntities5 DataContext = new Capital_MarketEntities5();

        // GET: Dashboard

        public ActionResult Dashboard()
        {
            int ID = Convert.ToInt16(Session["ID"]);
            Person user = DataContext.Persons.Where(a => a.ID == ID).FirstOrDefault();
            return View(new OrdersData { stock = DataContext.Stocks.ToList(), order = DataContext.Orders.Where(a => a.ClientID == user.ID).Where(b => b.OrderDate == DateTime.Today).ToList() });

        }
        public ActionResult Pie()
        {
            int ID = Convert.ToInt16(Session["ID"]);
            var orders = DataContext.Chart(ID).ToList();
            var stocks = DataContext.Stocks.ToList();
            List<ChartData> Result = new List<ChartData> { };
            foreach (var item in orders)
            {
                foreach (var stock in stocks)
                {

                    if (item.stockID == stock.ID)
                    {
                        ChartData ChartStock = new ChartData();
                        ChartStock.name = stock.CompanyName;
                        ChartStock.y = item.StockValue.Value;
                        if(ChartStock.y!=0)
                            Result.Add(ChartStock);
                        break;
                    }

                }
                
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }
    }
}