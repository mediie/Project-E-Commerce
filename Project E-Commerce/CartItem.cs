using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_E_Commerce
{
    public class CartItem
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal => Price * Quantity;
    }



}