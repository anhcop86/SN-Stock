using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareHolderCore.Domain.Model;

namespace ShareHoderFrontEndV2.Ext
{
    public static class DropDownList
    {
        public static IEnumerable<SelectListItem> ToSelectListItems(
              this IEnumerable<ShareHolderHoldSymbol> shareHolderHoldSymbol, int selectedId)
        {
            return
                shareHolderHoldSymbol.OrderBy(sh => sh.ShareSymbol)
                      .Select(sh =>
                          new SelectListItem
                          {
                              Selected = (sh.ShareHolderId == selectedId),
                              Text = sh.ShareSymbol,
                              Value = sh.ShareHolderId.ToString()
                          });
        }
    }
}