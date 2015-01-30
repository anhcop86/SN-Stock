using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using ShareHolderInfrastructure;
using ShareHolderCore.Domain.Model;
using ShareHolderCore.Domain.Repositories;
using ShareHolderCore;
using ShareHoderFrontEndV2.Models;
using ShareHoderFrontEndV2.Ext;


namespace ShareHoderFrontEndV2.Controllers
{
    [Authorize]
    public class TradingTransactionController : BaseController
    {
        private const int DefaultPageSize = 1;
        //
        // GET: /TradingTransaction/
        [HttpGet]
        public ActionResult Index()
        {

            ITransactionDetailHistory<TransactionDetailHistory> tradingTransactionRepo = new TransactionDetailHistoryRepository();
            IShareHolderHoldSymbol<ShareHolderHoldSymbol> shareHolderHoldSymbolRepo = new ShareHolderHoldSymbolRepository();
            IRepository<TransactionCategory> transactionCategoryRepo = new TransactionCategoryRepository();
            IMembershipRepository<ShareHolderCore.Domain.Model.Membership> membershipRepo = new MembershipRepository();
           
            string shareHoldertype = "I";
            ShareHolderCore.Domain.Model.Membership member = membershipRepo.GetByLoginId(User.Identity.Name);
            IList<ShareHolderHoldSymbol> shareHolderHoldSymbol = shareHolderHoldSymbolRepo.GetShareHolderHoldSymbol(member.MemberId.ToString());
            ISubShareHolders<ShareHolderHoldSymbol> subShareHoldersRepo = new SubShareHoldersRepository();
           
            ViewData["transactionCategory"] = new SelectList(transactionCategoryRepo.GetAll(), "TransactionCategoryId", "Description");
            ViewData["fromdatepicker"] = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
            ViewData["todatepicker"] = DateTime.Now.ToString("dd/MM/yyyy");

            if (shareHolderHoldSymbol != null)
            {
                if(shareHolderHoldSymbol.Count == 1)
                {
                    shareHoldertype =shareHolderHoldSymbol[0].ShareHolderType;
                }
            }

            if(shareHoldertype == "I")    
            {
                ViewData["ShareHolderId"] = new SelectList(shareHolderHoldSymbolRepo.GetShareHolderHoldSymbol(member.MemberId.ToString()), "Id", "ShareSymbol");
            }
            else
            {
                ViewData["ShareHolderId"] = new SelectList(subShareHoldersRepo.GetSubShareHolders(shareHolderHoldSymbol[0].ShareHolderId), "Id", "ShareHolderCode");
            }

            List<TransactionDetailHistory> myModel = new List<TransactionDetailHistory>();
            return View(myModel);
            //return View(myModel.ToPagedList<TransactionDetailHistory>(1,10));
        }  
       
        public ActionResult SearchTradingTransaction(string Id, string TransactionCategoryId, string fromdatepicker, string todatepicker)
        {
            Int32 rows = 0;
            string shareHolderIdSelectedValue = Id;//frmCollection.Get("Id");
            string transactionCategoryIdSelectedValue = TransactionCategoryId;// frmCollection.Get("TransactionCategoryId");
            string[] shareHolderIdSelectedValues = shareHolderIdSelectedValue.Split('|');
            ITransactionDetailHistory<TransactionDetailHistory> tradingTransactionRepo = new TransactionDetailHistoryRepository();
            IShareHolderHoldSymbol<ShareHolderHoldSymbol> shareHolderHoldSymbolRepo = new ShareHolderHoldSymbolRepository();
            IRepository<TransactionCategory> transactionCategoryRepo = new TransactionCategoryRepository();
            IMembershipRepository<ShareHolderCore.Domain.Model.Membership> membershipRepo = new MembershipRepository();
            ISubShareHolders<ShareHolderHoldSymbol> subShareHoldersRepo = new SubShareHoldersRepository();
            
            ShareHolder shareHolder = new ShareHolder();        
            ShareHolderCore.Domain.Model.Membership member = membershipRepo.GetByLoginId(User.Identity.Name);
            IList<ShareHolderHoldSymbol> shareHolderHoldSymbol = shareHolderHoldSymbolRepo.GetShareHolderHoldSymbol(member.MemberId.ToString());
            string shareHoldertype = "I";
            Int32 holderId;
            string symbol;

            if (shareHolderHoldSymbol != null)
            {
                if(shareHolderHoldSymbol.Count == 1)
                {
                    shareHoldertype =shareHolderHoldSymbol[0].ShareHolderType;
                }
            }

            holderId = Convert.ToInt32(shareHolderIdSelectedValues[0]);
            //ShareHolder shareHolder = shareHolderRepo.GetById(User.Identity.i
            if(shareHoldertype == "I")    
            {      
                ViewData["ShareHolderId"] = new SelectList(shareHolderHoldSymbolRepo.GetShareHolderHoldSymbol(member.MemberId.ToString()), "Id", "ShareSymbol", shareHolderIdSelectedValue.Trim());
                
                symbol =  shareHolderIdSelectedValues[1];
            }
            else
            {
                ViewData["ShareHolderId"] = new SelectList(subShareHoldersRepo.GetSubShareHolders(shareHolderHoldSymbol[0].ShareHolderId), "Id", "ShareHolderCode", shareHolderIdSelectedValue.Trim());
                symbol =  "ALL";
            }

            ViewData["transactionCategory"] = new SelectList(transactionCategoryRepo.GetAll(), "TransactionCategoryId", "Description", transactionCategoryIdSelectedValue);
            ViewData["fromdatepicker"] = fromdatepicker;//frmCollection.Get("fromdatepicker");
            ViewData["todatepicker"] = todatepicker;//frmCollection.Get("todatepicker");
            //ViewData["debitHolderId"] ="";
            //ViewData["debitHolderName"] = "";
            //ViewData["crebitHolderId"] = "";
            //ViewData["crebitHolderName"] = "";
            Int16 category = 0;
            category = (Int16.TryParse(transactionCategoryIdSelectedValue, out category) == false ? category : Int16.Parse(transactionCategoryIdSelectedValue));
            IList<TransactionDetailHistory> myModel = new List<TransactionDetailHistory>();
            myModel = tradingTransactionRepo.Search(
                                ApplicationHelper.ConvertStringToDate(fromdatepicker),
                                ApplicationHelper.ConvertStringToDate(todatepicker),
                                holderId,
                                symbol,
                                category,
                                1,
                                100,
                                out rows,
                                "TransactionDate",
                                "ASC");

            rows = 10;         
            return View(myModel);            
        }        
 
        [HttpGet]
        public ActionResult CurrentBalance()
        {
          
            string shareHoldertype = "I";
            IShareHolderSummary<TransactionDetailOfShareHolder> tradingTransactionRepo = new ShareHolderSummaryRepository();
            IShareHolderHoldSymbol<ShareHolderHoldSymbol> shareHolderHoldSymbolRepo = new ShareHolderHoldSymbolRepository();
            IRepository<TransactionCategory> transactionCategoryRepo = new TransactionCategoryRepository();
            IMembershipRepository<ShareHolderCore.Domain.Model.Membership> membershipRepo = new MembershipRepository();
            ISubShareHolders<ShareHolderHoldSymbol> subShareHoldersRepo = new SubShareHoldersRepository();
            
            ShareHolderCore.Domain.Model.Membership member = membershipRepo.GetByLoginId(User.Identity.Name);
            IList<ShareHolderHoldSymbol> shareHolderHoldSymbol = shareHolderHoldSymbolRepo.GetShareHolderHoldSymbol(member.MemberId.ToString());

            ViewData["ShareHolderId"] = new SelectList(shareHolderHoldSymbolRepo.GetShareHolderHoldSymbol(member.MemberId.ToString()), "Id", "ShareSymbol");         
            ViewData["todatepicker"] = DateTime.Now.ToString("dd/MM/yyyy");

            if (shareHolderHoldSymbol != null)
            {
                if (shareHolderHoldSymbol.Count == 1)
                {
                    shareHoldertype = shareHolderHoldSymbol[0].ShareHolderType;
                }
            }

            if (shareHoldertype == "I")
            {
                ViewData["ShareHolderId"] = new SelectList(shareHolderHoldSymbolRepo.GetShareHolderHoldSymbol(member.MemberId.ToString()), "Id", "ShareSymbol");
            }
            else
            {
                ViewData["ShareHolderId"] = new SelectList(subShareHoldersRepo.GetSubShareHolders(shareHolderHoldSymbol[0].ShareHolderId), "Id", "ShareHolderCode");
            }

            List<TransactionDetailOfShareHolder> myModel = new List<TransactionDetailOfShareHolder>();
            return View(myModel);
        }
     
        public ActionResult SearchCurrentBalance(string Id, string todatepicker)
        {

            string shareHoldertype = "I";
            Int32 holderId;
            string symbol;
            Int32 rows = 0;
            string shareHolderIdSelectedValue = Id;//frmCollection.Get("Id");
            
            string[] shareHolderIdSelectedValues = shareHolderIdSelectedValue.Split('|');
            IShareHolderSummary<TransactionDetailOfShareHolder> shareHolderSummaryRepo = new ShareHolderSummaryRepository();
            IShareHolderHoldSymbol<ShareHolderHoldSymbol> shareHolderHoldSymbolRepo = new ShareHolderHoldSymbolRepository();            
            IMembershipRepository<ShareHolderCore.Domain.Model.Membership> membershipRepo = new MembershipRepository();
            ISubShareHolders<ShareHolderHoldSymbol> subShareHoldersRepo = new SubShareHoldersRepository();
            IRepository<ShareHolder> shRepo = new ShareHolderRepository();
            ShareHolder shareHolder = new ShareHolder();
            
            
            ShareHolderCore.Domain.Model.Membership member = membershipRepo.GetByLoginId(User.Identity.Name);
            IList<ShareHolderHoldSymbol> shareHolderHoldSymbol = shareHolderHoldSymbolRepo.GetShareHolderHoldSymbol(member.MemberId.ToString());
            
            if (shareHolderHoldSymbol != null)
            {
                if (shareHolderHoldSymbol.Count == 1)
                {
                    shareHoldertype = shareHolderHoldSymbol[0].ShareHolderType;
                }
            }

            shareHolder = shRepo.GetById(Convert.ToInt32(shareHolderIdSelectedValues[0]));
            
            holderId = Convert.ToInt32(shareHolderIdSelectedValues[0]);
 
            if (shareHoldertype == "I")
            {
                ViewData["ShareHolderId"] = new SelectList(shareHolderHoldSymbolRepo.GetShareHolderHoldSymbol(member.MemberId.ToString()), "Id", "ShareSymbol", shareHolderIdSelectedValue.Trim());
                if (shareHolderIdSelectedValues[1]  != null)  
                    symbol = shareHolderIdSelectedValues[1];
                else
                    symbol = string.Empty;
            } // if the shareholder is the type of enterprise
            else
            {
                ViewData["ShareHolderId"] = new SelectList(subShareHoldersRepo.GetSubShareHolders(shareHolderHoldSymbol[0].ShareHolderId), "Id", "ShareHolderCode", shareHolderIdSelectedValue.Trim());
                if (shareHolderIdSelectedValues[1] != null)
                    symbol = shareHolderIdSelectedValues[1];
                else
                    symbol = string.Empty;
            }
            
            ViewData["todatepicker"] = todatepicker;//frmCollection.Get("todatepicker");            
            ViewData["ShareHolderCode"] = shareHolder.ShareHolderCode;
            ViewData["ShareHolderName"] = shareHolder.Name;
            ViewData["Ssn"] = shareHolder.Ssn;
            ViewData["Address"] = shareHolder.Address;

            IList<TransactionDetailOfShareHolder> myModel = new List<TransactionDetailOfShareHolder>();
            myModel = shareHolderSummaryRepo.GetCurrentBalance(                                
                                ApplicationHelper.ConvertStringToDate(todatepicker),
                                shareHolder.ShareHolderCode,
                                symbol,
                                "TransactionDate",
                                "DESC",
                                1,
                                100                              
                                );

            
            return View(myModel);
        }
        //
        // GET: /TradingTransaction/Details/5 

        [HttpGet]
        public ActionResult SummaryCurrentBalance()
        {
            string shareHoldertype = "I";
            IShareHolderHoldSymbol<ShareHolderHoldSymbol> shareHolderHoldSymbolRepo = new ShareHolderHoldSymbolRepository();
            IMembershipRepository<ShareHolderCore.Domain.Model.Membership> membershipRepo = new MembershipRepository();
            ISubShareHolders<ShareHolderHoldSymbol> subShareHoldersRepo = new SubShareHoldersRepository();

            ShareHolderCore.Domain.Model.Membership member = membershipRepo.GetByLoginId(User.Identity.Name);
            IList<ShareHolderHoldSymbol> shareHolderHoldSymbol = shareHolderHoldSymbolRepo.GetShareHolderHoldSymbol(member.MemberId.ToString());
            ViewData["ShareHolderId"] = new SelectList(shareHolderHoldSymbolRepo.GetShareHolderHoldSymbol(member.MemberId.ToString()), "Id", "ShareSymbol");
            
            if (shareHolderHoldSymbol != null)
            {
                if (shareHolderHoldSymbol.Count == 1)
                {
                    shareHoldertype = shareHolderHoldSymbol[0].ShareHolderType;
                }
            }

            if (shareHoldertype == "I")
            {
                ViewData["ShareHolderId"] = new SelectList(shareHolderHoldSymbolRepo.GetShareHolderHoldSymbol(member.MemberId.ToString()), "Id", "ShareSymbol");
            }
            else
            {
                ViewData["ShareHolderId"] = new SelectList(subShareHoldersRepo.GetSubShareHolders(shareHolderHoldSymbol[0].ShareHolderId, "0", Resources.UserInterface.selectAllOption), "Id", "ShareHolderCode");
            }
            

            List<TransactionBalance> myModel = new List<TransactionBalance>();
            return View(myModel);
        }

        public ActionResult SearchSummaryCurrentBalance(string Id)
        {
            Int32 rows = 0;
            string shareHoldertype = "I";
            string shareHolderIdSelectedValue = Id;//frmCollection.Get("Id");
            ITransactinBalance<TransactionBalance> transactionBalanceRepository = new TransactionBalanceRepository();
            string symbol;
            string[] shareHolderIdSelectedValues = shareHolderIdSelectedValue.Split('|');
            //IShareHolderSummary<TransactionDetailOfShareHolder> shareHolderSummaryRepo = new ShareHolderSummaryRepository();
            IShareHolderHoldSymbol<ShareHolderHoldSymbol> shareHolderHoldSymbolRepo = new ShareHolderHoldSymbolRepository();
            ISubShareHolders<ShareHolderHoldSymbol> subShareHoldersRepo = new SubShareHoldersRepository();
            IMembershipRepository<ShareHolderCore.Domain.Model.Membership> membershipRepo = new MembershipRepository();
         //   ShareHolderHoldSymbol shareHolderHoldSymbol = new ShareHolderHoldSymbol();
            ShareHolder shareHolder = new ShareHolder();
            IRepository<ShareHolder> shRepo = new ShareHolderRepository();
           
            ShareHolderCore.Domain.Model.Membership member = membershipRepo.GetByLoginId(User.Identity.Name);

            string shareHolders = LoadShareHolder(member.MemberId.ToString());

            IList<ShareHolderHoldSymbol> shareHolderHoldSymbol = shareHolderHoldSymbolRepo.GetShareHolderHoldSymbol(member.MemberId.ToString());
            if (shareHolderHoldSymbol != null)
            {
                if (shareHolderHoldSymbol.Count == 1)
                {
                    shareHoldertype = shareHolderHoldSymbol[0].ShareHolderType;
                }
            }

            
            if (shareHoldertype == "I")
            {
                ViewData["ShareHolderId"] = new SelectList(shareHolderHoldSymbolRepo.GetShareHolderHoldSymbol(member.MemberId.ToString()), "Id", "ShareSymbol", shareHolderIdSelectedValue.Trim());
                shareHolders = LoadShareHolder(member.MemberId.ToString());               
            } // if the shareholder is the type of enterprise
            else
            {
                shareHolder = shRepo.GetById(Convert.ToInt32(shareHolderIdSelectedValues[0]));
                ViewData["ShareHolderId"] = new SelectList(subShareHoldersRepo.GetSubShareHolders(shareHolderHoldSymbol[0].ShareHolderId, "0", Resources.UserInterface.selectAllOption), "Id", "ShareHolderCode", shareHolderIdSelectedValue.Trim());
                if (shareHolderIdSelectedValues[0] == "0" )
                {
                    shareHolders = MakeSubShareHolderList(subShareHoldersRepo.GetSubShareHolders(shareHolderHoldSymbol[0].ShareHolderId));
                }
                else if (shareHolder != null)
                {
                    shareHolders = shareHolder.ShareHolderId.ToString();
                }
            }

            if (shareHolderIdSelectedValues[0] == "0")
            {
                symbol = "All";
            }
            else
            {
                if (shareHolderIdSelectedValues[1] != null)
                    symbol = shareHolderIdSelectedValues[1];
                else
                    symbol = "All";
            }     

            IList<TransactionBalance> myModel = new List<TransactionBalance>();
            myModel = transactionBalanceRepository.GetTransactionBalances(shareHolders,
                                symbol,
                                0,
                                "ShareHolderCode",
                                "DESC",
                                1,
                                1000, 
                                out rows
                                );

            rows = 10;
            return View(myModel);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /TradingTransaction/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /TradingTransaction/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /TradingTransaction/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /TradingTransaction/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /TradingTransaction/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /TradingTransaction/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        protected string LoadShareHolder(string userId)
        {
            IShareHolderHoldSymbol<ShareHolderHoldSymbol> shareHolderHoldSymbolRepo = new ShareHolderHoldSymbolRepository();
            string shareHolderid = string.Empty; // join chuoi id thanh mot day 12,12,145
            IList<ShareHolderHoldSymbol> companyList = shareHolderHoldSymbolRepo.GetShareHolderHoldSymbol(userId);//WebSession.Profile.ShareHolderHoldSymbol;
            foreach (ShareHolderHoldSymbol item in companyList)
            {
                shareHolderid = item.ShareHolderId.ToString() + "," + shareHolderid;
            }
            return shareHolderid.Remove(shareHolderid.Length - 1);
        }

        protected string MakeSubShareHolderList(IList<ShareHolderHoldSymbol> source)
        {
            string shareHolderid = string.Empty; // join chuoi id thanh mot day 12,12,145          
            foreach (ShareHolderHoldSymbol item in source)
            {
                shareHolderid = item.ShareHolderId.ToString() + "," + shareHolderid;
            }
            return shareHolderid.Remove(shareHolderid.Length - 1);
        }
    }
}
