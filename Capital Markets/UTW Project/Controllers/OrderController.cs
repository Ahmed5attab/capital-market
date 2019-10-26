using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UTW_Project.Controllers
{
    public class OrderController : Controller
    {
        Capital_MarketEntities5 DataContext = new Capital_MarketEntities5();
        // GET: Order
        [HttpGet]
        public ActionResult OrderIndex(Order or)
        {
            if(Session["Role"]==null)
            {
                return RedirectToAction("LogIn", "Home");
            }

            
            SelectList St = new SelectList(DataContext.Stocks.ToList(), "ID", "CompanyName");
            ViewBag.Stocks = St;
            if(or==null)
            {
                Order r = new Order();
                
                return View(r);
            }
            
            
            return View(or);
            
        }
        [HttpPost]
        public ActionResult CreateOrder(Order r)
        {
            
            if (ModelState.IsValid)
            {
                r.ClientID = Convert.ToInt32(Session["ID"]);
                r.OrderDate = DateTime.Now;
                if (r.OrderType == 0)
                {
                    int Ordersbo = Convert.ToInt32(DataContext.OrdersBought(r.ClientID, r.StockID).FirstOrDefault());
                    int OrdersSe = Convert.ToInt32(DataContext.OrdersSold(r.ClientID, r.StockID).FirstOrDefault());
                    if (r.OrderQuantity < (Ordersbo - OrdersSe))
                    {
                        DataContext.Orders.Add(r);
                        DataContext.SaveChanges();
                        
                        return View("OrderCreated");
                    }
                    else
                    {
                        ViewBag.Q= (Ordersbo - OrdersSe);
                        return View("QuantityProb");
                    }
                }
                DataContext.Orders.Add(r);
                DataContext.SaveChanges();
                return View("OrderCreated");
            }
            
            return View("OrderFailed");
        }
        public double getPrice(string id)
        {
           
            if (id.Equals(""))
            {
                return 0;
            }
            int x = Convert.ToInt32(id);
            return DataContext.Stocks.Where(c => c.ID == x).FirstOrDefault().Price;
        }
        [HttpPost]
        public ActionResult Search(int OrderID)
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("LogIn", "Home");
            }
            Order or = DataContext.Orders.Where(m => m.OrderID == OrderID).FirstOrDefault();
            if(or!=null)
            {
                if (or.ClientID == Convert.ToInt32(Session["ID"]))
                {
                    return RedirectToAction("OrderIndex", or);
                }
            }
            
            return View("SearchFailed");
        }
       
        public ActionResult EditOrder(int OrderID, int ordertype, int StockID, string OrderQ)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("LogIn", "Home");
            }

            int OQ = Convert.ToInt32(OrderQ);
            OrderID = OrderID - 9876;
            int ClientID = Convert.ToInt32(Session["ID"]);
            StockID = StockID - 9876;


            int Ordersbo = Convert.ToInt32(DataContext.OrdersBought(ClientID, StockID).FirstOrDefault());
            int OrdersSe = Convert.ToInt32(DataContext.OrdersSold(ClientID, StockID).FirstOrDefault());
            Order x = DataContext.Orders.Where(o => o.OrderID == OrderID).FirstOrDefault();
            double b = (DateTime.Now - x.OrderDate).TotalSeconds; 
            if (b<0)
            {
                return View("OrderDateNotValid");
            }
                if (ordertype == 0)
                {

                    
                    if (OQ <= x.OrderQuantity)
                    {
                        x.OrderQuantity = OQ;
                        DataContext.SaveChanges();
                        return View("OrderCreated");
                    }
                    else
                    {
                        if ((Ordersbo - OrdersSe) > (OQ - x.OrderQuantity))
                        {
                            x.OrderQuantity = OQ;
                            DataContext.SaveChanges();
                            return View("OrderCreated");
                        }

                        else
                        {
                            ViewBag.Q = (Ordersbo - OrdersSe);
                            return View("QuantityProb");
                        }
                    }
                }
                else if(ordertype==1)
                {
                if (OQ >= x.OrderQuantity)
                {
                    x.OrderQuantity = OQ;
                    DataContext.SaveChanges();
                    return View("OrderCreated");
                }
                else
                {
                    if(Ordersbo-(x.OrderQuantity-OQ)>=OrdersSe )
                    {
                        x.OrderQuantity = OQ;
                        DataContext.SaveChanges();
                        return View("OrderCreated");
                    }
                    else
                    {
                        ViewBag.Q = (Ordersbo - OrdersSe);
                        return View("QuantityProb");
                    }
                }
                }
                return View("OrderCreated");

        }
                
            
            
        }
        

        
    }
