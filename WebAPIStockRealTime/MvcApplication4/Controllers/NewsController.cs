using Business;
using Entities;
using MvcApplication4.Models;
using PorfolioInvesment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace PorfolioInvesment.Controllers
{
    public class NewsController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "GET no parameter", "value2" };
        }

        //public IEnumerable<string> Get(string username, string password)
        //{
        //    if (Authentication.AuthenApi(username, password))
        //    return new string[] { "YES", "value2" };
        //    else
        //    {
        //        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.Unauthorized)); 
        //    }
        //}



        public List<AGNews_Articles> Post(News tickerList)
        {
            if (tickerList == null) return null;
            int outputRownumber = 0;

            string tickerItem = ApplicationHelper.ParstToFormat_IN_SQL(tickerList.tickerList);

            if (Authentication.AuthenApi(tickerList.username, tickerList.password))
            {
                return AGNews_ArticlesServices.GetAGNews_ArticlesListByTickerList(tickerItem, 1, 100, out outputRownumber);
                //"TV3','KCE"
            }
            else
            {
                return new List<AGNews_Articles>();
            }
            //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            //return response;
        }

       
    }
}
