//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PorfolioInvesment.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class stox_tb_HOSE_TotalTrading
    {
        public long ID { get; set; }
        public Nullable<decimal> VNIndex { get; set; }
        public Nullable<decimal> TotalTrade { get; set; }
        public Nullable<double> TotalShares { get; set; }
        public Nullable<double> TotalValues { get; set; }
        public Nullable<double> UpVolume { get; set; }
        public Nullable<double> DownVolume { get; set; }
        public Nullable<double> NoChangeVolume { get; set; }
        public Nullable<int> Advances { get; set; }
        public Nullable<int> Declines { get; set; }
        public Nullable<int> NoChange { get; set; }
        public Nullable<decimal> VN50Index { get; set; }
        public string MarketID { get; set; }
        public string Filter { get; set; }
        public Nullable<int> Time { get; set; }
        public Nullable<System.DateTime> DateReport { get; set; }
        public Nullable<decimal> PreVNIndex { get; set; }
        public Nullable<decimal> IndexChange { get; set; }
        public Nullable<int> LiveID { get; set; }
        public Nullable<decimal> Lowest { get; set; }
        public Nullable<decimal> Hightest { get; set; }
        public Nullable<decimal> OpenIndex { get; set; }
    }
}
