using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UTW_Project.Models
{
    public class OrdersData
    {

        public List<Person> user { get; set; }
        public List<Stock> stock { get; set; }
        public List<Order> order { get; set; }
    }
}