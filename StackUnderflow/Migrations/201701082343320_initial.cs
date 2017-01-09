namespace StackUnderflow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        AnswerDate = c.DateTime(nullable: false),
                        Upvotes = c.Int(nullable: false),
                        Downvotes = c.Int(nullable: false),
                        Question_Id = c.Int(nullable: false),
                        PosterId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.PosterId)
                .ForeignKey("dbo.Questions", t => t.Question_Id, cascadeDelete: true)
                .Index(t => t.Question_Id)
                .Index(t => t.PosterId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DisplayName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AnswerVotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoteUpOrDown = c.Int(nullable: false),
                        Voter_Id = c.String(maxLength: 128),
                        Answer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.Answer_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Voter_Id)
                .Index(t => t.Voter_Id)
                .Index(t => t.Answer_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Body = c.String(),
                        QuestionDate = c.DateTime(nullable: false),
                        Upvotes = c.Int(nullable: false),
                        Downvotes = c.Int(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        PosterId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.PosterId)
                .Index(t => t.PosterId);
            
            CreateTable(
                "dbo.QuestionVotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoteUpOrDown = c.Int(nullable: false),
                        Voter_Id = c.String(maxLength: 128),
                        Question_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Voter_Id)
                .Index(t => t.Voter_Id)
                .Index(t => t.Question_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.QuestionVotes", "Voter_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.QuestionVotes", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Questions", "PosterId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Answers", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AnswerVotes", "Voter_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AnswerVotes", "Answer_Id", "dbo.Answers");
            DropForeignKey("dbo.Answers", "PosterId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.QuestionVotes", new[] { "Question_Id" });
            DropIndex("dbo.QuestionVotes", new[] { "Voter_Id" });
            DropIndex("dbo.Questions", new[] { "PosterId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AnswerVotes", new[] { "Answer_Id" });
            DropIndex("dbo.AnswerVotes", new[] { "Voter_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Answers", new[] { "PosterId" });
            DropIndex("dbo.Answers", new[] { "Question_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.QuestionVotes");
            DropTable("dbo.Questions");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AnswerVotes");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Answers");
        }
    }
}
