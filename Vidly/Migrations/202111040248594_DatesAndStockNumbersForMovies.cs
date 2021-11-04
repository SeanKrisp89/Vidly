namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatesAndStockNumbersForMovies : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Movies SET ReleaseDate = '2020/8/30 12:00:00 AM', DateAdded = '2021/1/31 12:00:00 AM', NumberInStock = 3 WHERE Id = 1");
            Sql("UPDATE Movies SET ReleaseDate = '2019/3/27 12:00:00 AM', DateAdded = '2020/2/19 12:00:00 AM', NumberInStock = 5 WHERE Id = 2");
            Sql("UPDATE Movies SET ReleaseDate = '2018/11/29 12:00:00 AM', DateAdded = '2019/4/14 12:00:00 AM', NumberInStock = 1 WHERE Id = 3");
        }
        
        public override void Down()
        {
        }
    }
}
