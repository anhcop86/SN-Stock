namespace PhimHang.vn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAvataCover : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "AvataCover", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "AvataCover");
        }
    }
}
