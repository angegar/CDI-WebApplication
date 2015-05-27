namespace KarateIsere.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adherents",
                c => new
                    {
                        AdherentID = c.Int(nullable: false),
                        Nom = c.String(nullable: false, maxLength: 50),
                        Prenom = c.String(nullable: false, maxLength: 50),
                        Email = c.String(maxLength: 100),
                        Tel = c.String(maxLength: 10),
                        Mobile = c.String(maxLength: 10),
                        Grade = c.String(maxLength: 50),
                        Diplome = c.String(maxLength: 50),
                        IsJugeGrade = c.Boolean(),
                        IsArbitreDept = c.Boolean(),
                        IsArbitreLig = c.Boolean(),
                        IsArbitreNat = c.Boolean(),
                        IsProfesseur = c.Boolean(),
                        NumLicence = c.String(maxLength: 50),
                        NumAffiliation = c.String(maxLength: 50),
                        CategorieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdherentID)
                .ForeignKey("dbo.Club", t => t.NumAffiliation)
                .ForeignKey("dbo.Categorie", t => t.CategorieID, cascadeDelete: true)
                .Index(t => t.NumAffiliation)
                .Index(t => t.CategorieID);
            
            CreateTable(
                "dbo.Categorie",
                c => new
                    {
                        CategorieID = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CategorieID);
            
            CreateTable(
                "dbo.Competiteur",
                c => new
                    {
                        NumLicence = c.String(nullable: false, maxLength: 50),
                        Nom = c.String(nullable: false, maxLength: 50),
                        Prenom = c.String(nullable: false, maxLength: 50),
                        CategorieID = c.Int(nullable: false),
                        Poids = c.String(nullable: false, maxLength: 50),
                        isHomme = c.Boolean(nullable: false),
                        NumAffiliation = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.NumLicence)
                .ForeignKey("dbo.Club", t => t.NumAffiliation, cascadeDelete: true)
                .ForeignKey("dbo.Categorie", t => t.CategorieID, cascadeDelete: true)
                .Index(t => t.CategorieID)
                .Index(t => t.NumAffiliation);
            
            CreateTable(
                "dbo.Club",
                c => new
                    {
                        NumAffiliation = c.String(nullable: false, maxLength: 50),
                        NomClub = c.String(nullable: false, maxLength: 100),
                        Correspondant = c.String(maxLength: 50),
                        Telephone = c.String(maxLength: 50),
                        Adr_Administrative = c.String(maxLength: 500),
                        Adr_Dojo = c.String(maxLength: 500),
                        Code_Postal = c.String(maxLength: 50),
                        Ville = c.String(maxLength: 50),
                        Site_Web = c.String(maxLength: 100),
                        Facebook = c.String(maxLength: 100),
                        President_Tel = c.String(maxLength: 10),
                        President_Nom = c.String(maxLength: 50),
                        President_Prenom = c.String(maxLength: 50),
                        President_Email = c.String(maxLength: 100),
                        President_Mobile = c.String(maxLength: 10),
                        Tresorier_Tel = c.String(maxLength: 10),
                        Tresorier_Nom = c.String(maxLength: 50),
                        Tresorier_Prenom = c.String(maxLength: 50),
                        Tresorier_Email = c.String(maxLength: 100),
                        Tresorier_Mobile = c.String(maxLength: 10),
                        Secretaire_Tel = c.String(maxLength: 10),
                        Secretaire_Nom = c.String(maxLength: 50),
                        Secretaire_Prenom = c.String(maxLength: 50),
                        Secretaire_Email = c.String(maxLength: 100),
                        Secretaire_Mobile = c.String(maxLength: 10),
                        Art_MartialID = c.Int(nullable: false),
                        Longitude = c.Double(),
                        Latitude = c.Double(),
                    })
                .PrimaryKey(t => t.NumAffiliation)
                .ForeignKey("dbo.Art_Martial", t => t.Art_MartialID, cascadeDelete: true)
                .Index(t => t.Art_MartialID);
            
            CreateTable(
                "dbo.Art_Martial",
                c => new
                    {
                        Art_MartialID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Art_MartialID);
            
            CreateTable(
                "dbo.ListeCompetiteur",
                c => new
                    {
                        ListeCompetiteurID = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 50),
                        NumAffiliation = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ListeCompetiteurID)
                .ForeignKey("dbo.Club", t => t.NumAffiliation, cascadeDelete: true)
                .Index(t => t.NumAffiliation);
            
            CreateTable(
                "dbo.NotificationEmails",
                c => new
                    {
                        NotificationEmailID = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        LastModif = c.DateTime(nullable: false),
                        Status = c.String(),
                        Message = c.String(),
                        Email = c.String(),
                        CompetitionID = c.Int(nullable: false),
                        NumAffiliation = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.NotificationEmailID)
                .ForeignKey("dbo.Club", t => t.NumAffiliation)
                .ForeignKey("dbo.Competition", t => t.CompetitionID, cascadeDelete: true)
                .Index(t => t.CompetitionID)
                .Index(t => t.NumAffiliation);
            
            CreateTable(
                "dbo.Competition",
                c => new
                    {
                        CompetitionID = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 100),
                        DateCompetition = c.DateTime(nullable: false, storeType: "date"),
                        FinInscription = c.DateTime(nullable: false, storeType: "date"),
                        Lieu = c.String(nullable: false, maxLength: 100),
                        IsKata = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CompetitionID);
            
            CreateTable(
                "dbo.Inscriptions",
                c => new
                    {
                        InscriptionId = c.Int(nullable: false, identity: true),
                        NumLicence = c.String(nullable: false, maxLength: 50),
                        CompetitionID = c.Int(nullable: false),
                        Classement = c.Int(),
                    })
                .PrimaryKey(t => t.InscriptionId)
                .ForeignKey("dbo.Competition", t => t.CompetitionID, cascadeDelete: true)
                .ForeignKey("dbo.Competiteur", t => t.NumLicence, cascadeDelete: true)
                .Index(t => t.NumLicence)
                .Index(t => t.CompetitionID);
            
            CreateTable(
                "dbo.Professeurs",
                c => new
                    {
                        ProfesseursID = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 50),
                        Prenom = c.String(nullable: false, maxLength: 50),
                        Email = c.String(maxLength: 100),
                        Mobile = c.String(maxLength: 10),
                        Grade = c.String(maxLength: 50),
                        IsJugeGrade = c.Boolean(),
                        IsArbitreDept = c.Boolean(),
                        IsArbitreLigue = c.Boolean(),
                        IsArbitreNational = c.Boolean(),
                        Diplome = c.String(nullable: false, maxLength: 50),
                        Telephone = c.String(maxLength: 10),
                        NumAffiliation = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ProfesseursID)
                .ForeignKey("dbo.Club", t => t.NumAffiliation)
                .Index(t => t.NumAffiliation);
            
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        Login = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 1000),
                        PasswordQuestion = c.String(maxLength: 1000),
                        PasswordAnswer = c.String(maxLength: 1000),
                        Nom = c.String(nullable: false, maxLength: 50),
                        Prenom = c.String(nullable: false, maxLength: 50),
                        LastAccess = c.DateTime(),
                    })
                .PrimaryKey(t => t.Login);
            
            CreateTable(
                "dbo.Creneaux",
                c => new
                    {
                        CreneauId = c.Int(nullable: false, identity: true),
                        NomClub = c.String(nullable: false, maxLength: 100),
                        Jour = c.String(nullable: false, maxLength: 50),
                        HeureDebut = c.String(nullable: false, maxLength: 50),
                        HeureFin = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.CreneauId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ListeCompetiteursCompetiteurs",
                c => new
                    {
                        NumLicence = c.Int(nullable: false),
                        ListeCompetiteurID = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.NumLicence, t.ListeCompetiteurID })
                .ForeignKey("dbo.ListeCompetiteur", t => t.NumLicence)
                .ForeignKey("dbo.Competiteur", t => t.ListeCompetiteurID)
                .Index(t => t.NumLicence)
                .Index(t => t.ListeCompetiteurID);
            
            CreateTable(
                "dbo.CompetitionCategories",
                c => new
                    {
                        CompetitionID = c.Int(nullable: false),
                        CategorieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CompetitionID, t.CategorieID })
                .ForeignKey("dbo.Competition", t => t.CompetitionID)
                .ForeignKey("dbo.Categorie", t => t.CategorieID)
                .Index(t => t.CompetitionID)
                .Index(t => t.CategorieID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Adherents", "CategorieID", "dbo.Categorie");
            DropForeignKey("dbo.Competiteur", "CategorieID", "dbo.Categorie");
            DropForeignKey("dbo.Inscriptions", "NumLicence", "dbo.Competiteur");
            DropForeignKey("dbo.Professeurs", "NumAffiliation", "dbo.Club");
            DropForeignKey("dbo.NotificationEmails", "CompetitionID", "dbo.Competition");
            DropForeignKey("dbo.Inscriptions", "CompetitionID", "dbo.Competition");
            DropForeignKey("dbo.CompetitionCategories", "CategorieID", "dbo.Categorie");
            DropForeignKey("dbo.CompetitionCategories", "CompetitionID", "dbo.Competition");
            DropForeignKey("dbo.NotificationEmails", "NumAffiliation", "dbo.Club");
            DropForeignKey("dbo.ListeCompetiteursCompetiteurs", "ListeCompetiteurID", "dbo.Competiteur");
            DropForeignKey("dbo.ListeCompetiteursCompetiteurs", "NumLicence", "dbo.ListeCompetiteur");
            DropForeignKey("dbo.ListeCompetiteur", "NumAffiliation", "dbo.Club");
            DropForeignKey("dbo.Competiteur", "NumAffiliation", "dbo.Club");
            DropForeignKey("dbo.Club", "Art_MartialID", "dbo.Art_Martial");
            DropForeignKey("dbo.Adherents", "NumAffiliation", "dbo.Club");
            DropIndex("dbo.CompetitionCategories", new[] { "CategorieID" });
            DropIndex("dbo.CompetitionCategories", new[] { "CompetitionID" });
            DropIndex("dbo.ListeCompetiteursCompetiteurs", new[] { "ListeCompetiteurID" });
            DropIndex("dbo.ListeCompetiteursCompetiteurs", new[] { "NumLicence" });
            DropIndex("dbo.Professeurs", new[] { "NumAffiliation" });
            DropIndex("dbo.Inscriptions", new[] { "CompetitionID" });
            DropIndex("dbo.Inscriptions", new[] { "NumLicence" });
            DropIndex("dbo.NotificationEmails", new[] { "NumAffiliation" });
            DropIndex("dbo.NotificationEmails", new[] { "CompetitionID" });
            DropIndex("dbo.ListeCompetiteur", new[] { "NumAffiliation" });
            DropIndex("dbo.Club", new[] { "Art_MartialID" });
            DropIndex("dbo.Competiteur", new[] { "NumAffiliation" });
            DropIndex("dbo.Competiteur", new[] { "CategorieID" });
            DropIndex("dbo.Adherents", new[] { "CategorieID" });
            DropIndex("dbo.Adherents", new[] { "NumAffiliation" });
            DropTable("dbo.CompetitionCategories");
            DropTable("dbo.ListeCompetiteursCompetiteurs");
            DropTable("dbo.Roles");
            DropTable("dbo.Creneaux");
            DropTable("dbo.Admin");
            DropTable("dbo.Professeurs");
            DropTable("dbo.Inscriptions");
            DropTable("dbo.Competition");
            DropTable("dbo.NotificationEmails");
            DropTable("dbo.ListeCompetiteur");
            DropTable("dbo.Art_Martial");
            DropTable("dbo.Club");
            DropTable("dbo.Competiteur");
            DropTable("dbo.Categorie");
            DropTable("dbo.Adherents");
        }
    }
}
