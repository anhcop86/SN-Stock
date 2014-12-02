using Business;
using Entities;
using MvcApplication4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PorfolioInvesment.Controllers
{
    public class IndexsController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "Please enter parameter", "null value" };
        }
        public List<INDEX> Post(UserAuthen userauthen)
        {
            if (ModelState.IsValid)
            {

                if (!Authentication.AuthenApi(userauthen.username, userauthen.password))
                {
                    return new List<INDEX>();
                }
                return INDEXServices.GetINDEXHNX() + INDEXServices.GetINDEXHSX(); // vnindex and hnxindex get from database                    //"TV3','KCE"
                
            }
            else
            {
                return new List<INDEX>();
            }
        }

        
    }
}
