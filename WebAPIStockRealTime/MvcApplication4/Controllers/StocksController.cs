using Business;
using Entities;
using MvcApplication4.Models;
using PorfolioInvesment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PorfolioInvesment.Controllers
{
    public class StocksController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "Please enter parameter", "null value" };
        }
        public List<Stock> Post(ParaStock paraStock)
        {
            if (ModelState.IsValid)
            {
                string listStock = ApplicationHelper.ParstToFormat_IN_SQL(paraStock.PI_tickerList);
                return StockServices.GetAllStock(listStock);
            }
            else
            {
                return new List<Stock>();
            }
            
        }
    }
}
