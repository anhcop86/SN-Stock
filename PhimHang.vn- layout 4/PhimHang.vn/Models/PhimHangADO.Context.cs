﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PhimHang.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class testEntities : DbContext
    {
        public testEntities()
            : base("name=testEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<FollowStock> FollowStocks { get; set; }
        public virtual DbSet<FollowUser> FollowUsers { get; set; }
        public virtual DbSet<PostComment> PostComments { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<StockPrice> StockPrices { get; set; }
        public virtual DbSet<StockRelate> StockRelates { get; set; }
        public virtual DbSet<StockViewLastest> StockViewLastests { get; set; }
        public virtual DbSet<UserRelate> UserRelates { get; set; }
        public virtual DbSet<UserViewLastest> UserViewLastests { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<StockCode> StockCodes { get; set; }
        public virtual DbSet<FilterKeyWord> FilterKeyWords { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PinStock> PinStocks { get; set; }
    }
}
