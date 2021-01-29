namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertMovies : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO dbo.Movies VALUES('Shrek', 3, 15/09/2010, 20/11/2010, 3)");
            Sql("INSERT INTO dbo.Movies VALUES('Titanic', 4, 17/05/2005, 20/11/2006, 4)");
            Sql("INSERT INTO dbo.Movies VALUES('Die Hard', 1, 20/09/2015, 20/11/2016, 4)");
            Sql("INSERT INTO dbo.Movies VALUES('Toy Story', 3, 15/09/2010, 20/11/2010, 3)");
        }
        
        public override void Down()
        {
        }
    }
}
