namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializeNumAvailWithNumInStock : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Movies SET NumberAvailable = NumberInStock"); //Initialization here.
        }
        
        public override void Down()
        {
        }
    }
}
