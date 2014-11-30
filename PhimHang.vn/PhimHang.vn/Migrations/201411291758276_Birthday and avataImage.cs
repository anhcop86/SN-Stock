namespace PhimHang.vn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BirthdayandavataImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "AvataImage", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "AvataImage");
            DropColumn("dbo.AspNetUsers", "BirthDate");
        }
    }
}
