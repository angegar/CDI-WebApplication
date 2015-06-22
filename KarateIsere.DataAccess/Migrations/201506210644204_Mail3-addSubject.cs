namespace KarateIsere.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mail3addSubject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mails", "Subject", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mails", "Subject");
        }
    }
}
