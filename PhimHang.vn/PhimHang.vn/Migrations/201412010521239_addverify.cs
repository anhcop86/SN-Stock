namespace PhimHang.vn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addverify : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Verify", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Verify");
        }
    }
}
