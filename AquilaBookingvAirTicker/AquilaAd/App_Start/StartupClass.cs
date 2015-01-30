using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ShareHolderCore;
using ShareHolderCore.Domain.Repositories;
using ShareHolderCore.Domain.Model;

namespace AquilaAd.App_Start
{
    public class StartupClass : Controller
    {
        public static void Init()
        {
            // whatever code you need

        }
        public void start()
        {
            if (User!= null && User.Identity.IsAuthenticated)
            {
                IMembershipRepository<ShareHolderCore.Domain.Model.Membership> userRep = new MembershipRepository();
                ShareHolderCore.Domain.Model.Membership membership = null;
                membership = userRep.GetByLoginId(User.Identity.Name);
                @Session["LoginObject"] = membership;
                @Session["UserType"] = checkUserType(membership.MemberId);
            }
        }
        private HotelOwner checkUserType(int MemberId)
        {
            IHotelOwnerRepository irpHower = new HotelOwnerRepository();
            HotelOwner hotelOwner = irpHower.getHotelOwnerByMemberShipId(MemberId);

            if (hotelOwner == null)
            {
                return null;
            }
            else
            {
                return hotelOwner;
            }

        }
    }
}