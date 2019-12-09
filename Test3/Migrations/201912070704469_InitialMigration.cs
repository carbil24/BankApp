namespace Test3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountNumber = c.Int(nullable: false, identity: true),
                        Balance = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.AccountNumber);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 20),
                        UserAccount_AccountNumber = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accounts", t => t.UserAccount_AccountNumber)
                .Index(t => t.UserAccount_AccountNumber);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserAccount_AccountNumber", "dbo.Accounts");
            DropIndex("dbo.Users", new[] { "UserAccount_AccountNumber" });
            DropTable("dbo.Users");
            DropTable("dbo.Accounts");
        }
    }
}
