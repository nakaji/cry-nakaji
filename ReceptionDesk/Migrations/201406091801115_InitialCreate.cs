namespace ReceptionDesk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CheckinHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CheckinTime = c.DateTime(nullable: false),
                        Participant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Participants", t => t.Participant_Id)
                .Index(t => t.Participant_Id);
            
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SocialGathering = c.Boolean(nullable: false),
                        IsMinors = c.Boolean(nullable: false),
                        IsStudent = c.Boolean(nullable: false),
                        IsInstructor = c.Boolean(nullable: false),
                        IsStaff = c.Boolean(nullable: false),
                        ImageUrl = c.String(),
                        CheckedIn = c.Boolean(nullable: false),
                        StudyMeeting_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudyMeetings", t => t.StudyMeeting_Id)
                .Index(t => t.StudyMeeting_Id);
            
            CreateTable(
                "dbo.StudyMeetings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Date = c.String(nullable: false),
                        SeminarFee = c.Int(nullable: false),
                        SocialGatheringFee = c.Int(nullable: false),
                        SocialGatheringFeeForStudent = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CheckinHistories", "Participant_Id", "dbo.Participants");
            DropForeignKey("dbo.Participants", "StudyMeeting_Id", "dbo.StudyMeetings");
            DropIndex("dbo.Participants", new[] { "StudyMeeting_Id" });
            DropIndex("dbo.CheckinHistories", new[] { "Participant_Id" });
            DropTable("dbo.StudyMeetings");
            DropTable("dbo.Participants");
            DropTable("dbo.CheckinHistories");
        }
    }
}
