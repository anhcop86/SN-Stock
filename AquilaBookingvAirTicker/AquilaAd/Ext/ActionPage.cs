using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AquilaAd.Ext
{
    public enum ActionPage
    {
        None, // none action
        Edit, // action edit item
        New, // action new item
        Delete, // action delete item
        SaveNew, // action save & new in this page
        Save // save
    }
}