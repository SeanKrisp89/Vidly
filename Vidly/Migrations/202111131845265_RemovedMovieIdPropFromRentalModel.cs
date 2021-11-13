namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedMovieIdPropFromRentalModel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Rentals", name: "MovieId", newName: "Movie_Id");
            RenameIndex(table: "dbo.Rentals", name: "IX_MovieId", newName: "IX_Movie_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Rentals", name: "IX_Movie_Id", newName: "IX_MovieId");
            RenameColumn(table: "dbo.Rentals", name: "Movie_Id", newName: "MovieId");
        }
    }
}
