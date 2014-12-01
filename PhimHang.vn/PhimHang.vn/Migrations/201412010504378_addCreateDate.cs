namespace PhimHang.vn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCreateDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CreatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CreatedDate");
        }
    }
}
