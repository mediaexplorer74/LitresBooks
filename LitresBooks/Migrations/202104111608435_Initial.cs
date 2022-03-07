using System;
//using System.Data.Entity.Migrations;

namespace LitresBooks.Migrations
{
    
    /*
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        AuthorID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        Description = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.AuthorID);
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(nullable: false, unicode: false, storeType: "text"),
                        Description = c.String(nullable: false, unicode: false, storeType: "text"),
                        Price = c.Double(nullable: false),
                        Type = c.String(nullable: false, maxLength: 30, unicode: false),
                        LitresEstimate = c.Double(nullable: false),
                        PH_ID = c.Int(nullable: false),
                        Actual_date = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Publishing_house", t => t.PH_ID, cascadeDelete: true)
                .Index(t => t.PH_ID);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        GenreID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.GenreID);
            
            CreateTable(
                "dbo.Publishing_house",
                c => new
                    {
                        PH_ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.PH_ID);
            
            CreateTable(
                "dbo.Quote",
                c => new
                    {
                        QuoteID = c.Int(nullable: false, identity: true),
                        QuoteText = c.String(nullable: false, unicode: false),
                        BookID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuoteID)
                .ForeignKey("dbo.Book", t => t.BookID, cascadeDelete: true)
                .Index(t => t.BookID);
            
            CreateTable(
                "dbo.Series",
                c => new
                    {
                        SeriesID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.SeriesID);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
            CreateTable(
                "dbo.Book_Genre",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        GenreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID, t.GenreID })
                .ForeignKey("dbo.Book", t => t.ID, cascadeDelete: true)
                .ForeignKey("dbo.Genre", t => t.GenreID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.GenreID);
            
            CreateTable(
                "dbo.Series_Book",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        SeriesID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID, t.SeriesID })
                .ForeignKey("dbo.Book", t => t.ID, cascadeDelete: true)
                .ForeignKey("dbo.Series", t => t.SeriesID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.SeriesID);
            
            CreateTable(
                "dbo.Author_Book",
                c => new
                    {
                        AuthorID = c.Int(nullable: false),
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AuthorID, t.ID })
                .ForeignKey("dbo.Author", t => t.AuthorID, cascadeDelete: true)
                .ForeignKey("dbo.Book", t => t.ID, cascadeDelete: true)
                .Index(t => t.AuthorID)
                .Index(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Author_Book", "ID", "dbo.Book");
            DropForeignKey("dbo.Author_Book", "AuthorID", "dbo.Author");
            DropForeignKey("dbo.Series_Book", "SeriesID", "dbo.Series");
            DropForeignKey("dbo.Series_Book", "ID", "dbo.Book");
            DropForeignKey("dbo.Quote", "BookID", "dbo.Book");
            DropForeignKey("dbo.Book", "PH_ID", "dbo.Publishing_house");
            DropForeignKey("dbo.Book_Genre", "GenreID", "dbo.Genre");
            DropForeignKey("dbo.Book_Genre", "ID", "dbo.Book");
            DropIndex("dbo.Author_Book", new[] { "ID" });
            DropIndex("dbo.Author_Book", new[] { "AuthorID" });
            DropIndex("dbo.Series_Book", new[] { "SeriesID" });
            DropIndex("dbo.Series_Book", new[] { "ID" });
            DropIndex("dbo.Book_Genre", new[] { "GenreID" });
            DropIndex("dbo.Book_Genre", new[] { "ID" });
            DropIndex("dbo.Quote", new[] { "BookID" });
            DropIndex("dbo.Book", new[] { "PH_ID" });
            DropTable("dbo.Author_Book");
            DropTable("dbo.Series_Book");
            DropTable("dbo.Book_Genre");
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Series");
            DropTable("dbo.Quote");
            DropTable("dbo.Publishing_house");
            DropTable("dbo.Genre");
            DropTable("dbo.Book");
            DropTable("dbo.Author");
        }
    
    }
    */
}//namespace end
