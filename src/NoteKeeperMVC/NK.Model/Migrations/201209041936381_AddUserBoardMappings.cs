namespace NK.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserBoardMappings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserBoards",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserIsAdmin = c.Boolean(nullable: false),
                        User_Id = c.Guid(nullable: false),
                        Board_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Boards", t => t.Board_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Board_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserBoards", new[] { "Board_Id" });
            DropIndex("dbo.UserBoards", new[] { "User_Id" });
            DropForeignKey("dbo.UserBoards", "Board_Id", "dbo.Boards");
            DropForeignKey("dbo.UserBoards", "User_Id", "dbo.Users");
            DropTable("dbo.UserBoards");
        }
    }
}
