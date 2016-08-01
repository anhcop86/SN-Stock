using PhimHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace PhimHang.Controllers
{
    public class BrokerController : Controller
    {
        private testEntities db = new testEntities();

        //
        // GET: /Broker/
        public async Task<ActionResult> Index(int numberBroker)
        {
            #region random dan phim chuyen nghiem
            var DanPhimRandom = (from u in db.UserLogins
                                      orderby Guid.NewGuid()
                                      where u.BrokerVIP == true
                                      select new UserRandom
                                      {
                                          Avata = string.IsNullOrEmpty(u.AvataImage) ? AppHelper.ImageURLAvataDefault : AppHelper.ImageURLAvata + u.AvataImage,
                                          UserName = u.UserNameCopy,
                                          FullName = u.FullName
                                      }).Take(numberBroker);
            #endregion
            return PartialView("_Partial_Area_Right_User1", await DanPhimRandom.ToListAsync());
        }
	}
    public class UserRandom
    {
        public string UserName { get; set; }
        public string Avata { get; set; }

        public string FullName { get; set; }
    }
}