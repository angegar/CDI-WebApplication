namespace KarateIsere.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mail2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mails",
                c => new
                    {
                        MailId = c.Int(nullable: false, identity: true),
                        CommonIdentifer = c.String(nullable: false),
                        Message = c.String(),
                        Help = c.String(),
                        Header_MailId = c.Int(),
                    })
                .PrimaryKey(t => t.MailId)
                .ForeignKey("dbo.Mails", t => t.Header_MailId)
                .Index(t => t.Header_MailId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mails", "Header_MailId", "dbo.Mails");
            DropIndex("dbo.Mails", new[] { "Header_MailId" });
            DropTable("dbo.Mails");
        }
    }
}
