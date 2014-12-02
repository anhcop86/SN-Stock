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
    
    public class IPProxysController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "Please enter parameter", "null value" };
        }


        // POST api/IPProxys/all
        [ActionName("GetAll")]
        public List<IPProxy> Post(string getAll)
        {
            if (ModelState.IsValid)
            {
                return IPProxyServices.GetIPProxyListAll();
            }
            else
            {
                return new List<IPProxy>();
            }

        }
        // POST api/IPProxys/create
         [ActionName("Create")]
        public HttpResponseMessage Post(ParaProxy paraProxy)
        {
            if (ModelState.IsValid)
            {
                #region insert database here
                try
                {
                    IPProxy ipProxy = new IPProxy { IPAddress = paraProxy.IPAddress, IPPort = int.Parse(paraProxy.IPPort), StatusIP = bool.Parse(paraProxy.StatusIP), CreateDate = int.Parse(paraProxy.CreateDate) };
                    IPProxyServices.CreateIPProxy(ipProxy);
                }
                catch (Exception)
                {
                    
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                #endregion end
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, paraProxy);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

        }
    }
}
