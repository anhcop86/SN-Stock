using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShareHoderFrontEndV2.Ext;

namespace ShareHoderFrontEndV2.Models
{
    public class CartItem : IEquatable<CartItem>
    {
        #region Properties

        // A place to store the quantity in the cart  
        // This property has an implicit getter and setter.  
        public int Quantity { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int TotalDate { get; set; }
        public decimal? Subtotal { get; set; }
        

        private Int64 _productId;
        public Int64 ProductId
        {
            get { return _productId; }
            set
            {
                // To ensure that the Prod object will be re-created  
                _product = null;
                _productId = value;
            }
        }
        private int _NoRoom;
        public int NoRoom
        {
            get { return _NoRoom; }
            set
            {
                // To ensure that the Prod object will be re-created  
                _product = null;
                _NoRoom = value;
            }
        }
        private Product _product = null;
        public Product Prod
        {
            get
            {
                // Lazy initialization - the object won't be created until it is needed  
                if (_product == null)
                {
                    _product = new Product(ProductId, NoRoom);
                }
                return _product;
            }
        }

        //public string Description
        //{
        //    get { return Prod.Description; }
        //}

        //public decimal UnitPrice
        //{
        //    get { return Prod.Price; }
        //}

        //public decimal TotalPrice
        //{
        //    get { return UnitPrice * Quantity; }
        //}

        #endregion

        // CartItem constructor just needs a productId  
        public CartItem(Int64 productId, int noRoom, string fromDate, string toDate)
        {
            this.ProductId = productId;
            this.NoRoom = noRoom;
            this.FromDate = fromDate;
            this.ToDate = toDate;

            DateTime from = new DateTime(); 
            from = ApplicationHelper.ConvertStringToDate(fromDate);

            DateTime to = new DateTime(); 
            to = ApplicationHelper.ConvertStringToDate(toDate);

            TimeSpan span = to - from;


            this.TotalDate = span.Days;
            this.Subtotal = TotalDate * NoRoom * Prod.Availability.Price  ;

            //Prod = new Product(productId, noRoom);
        }

        /** 
         * Equals() - Needed to implement the IEquatable interface 
         *    Tests whether or not this item is equal to the parameter 
         *    This method is called by the Contains() method in the List class 
         *    We used this Contains() method in the ShoppingCart AddItem() method 
         */
        public bool Equals(CartItem item)
        {
            return item.ProductId == this.ProductId;
        }
    }
}