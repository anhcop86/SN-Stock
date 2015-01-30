using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareHoderFrontEndV2.Models;

namespace ShareHoderFrontEndV2.Controllers
{
    public class ShoppingCartController : BaseController
    {
        //
        // GET: /ShoppingCart/

        public ActionResult Index()
        {
            //ShoppingCart.Instance.AddItem(1);

            List<CartItem> cartItem = ShoppingCart.Instance.Items;
            ViewData["NumberCartItem"] = cartItem.Count;
            return View(cartItem);
        }
        public ActionResult AddCart(Int64 idbooking, int noRoom)
        {
            ShoppingCart.Instance.AddItem(idbooking, noRoom, Session["txtFromDate"].ToString(), Session["txtToDate"].ToString());
            return RedirectToAction("BookingDetails", "Book", new { id = idbooking });
        }
        public ActionResult RemoveCart(Int64 idbooking)
        {
            ShoppingCart.Instance.RemoveItem(idbooking);
            return RedirectToAction("BookingDetails", "Book", new { id = idbooking });
        }

        public ActionResult RemoveShoppingCart(Int64 idbooking)
        {
            ShoppingCart.Instance.RemoveItem(idbooking);
            return RedirectToAction("", "ShoppingCart", new { id = idbooking });
        }
       

    }
}
