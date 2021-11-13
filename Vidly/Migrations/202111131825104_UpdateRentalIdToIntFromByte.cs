namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRentalIdToIntFromByte : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Rentals");
            AlterColumn("dbo.Rentals", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Rentals", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Rentals");
            AlterColumn("dbo.Rentals", "Id", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.Rentals", "Id");
        }
    }
}
