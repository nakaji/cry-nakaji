namespace ReceptionDesk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredAttributeToStudyMeetingOnParticipant : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Participants", "StudyMeeting_Id", "dbo.StudyMeetings");
            DropIndex("dbo.Participants", new[] { "StudyMeeting_Id" });
            AlterColumn("dbo.Participants", "StudyMeeting_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Participants", "StudyMeeting_Id");
            AddForeignKey("dbo.Participants", "StudyMeeting_Id", "dbo.StudyMeetings", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Participants", "StudyMeeting_Id", "dbo.StudyMeetings");
            DropIndex("dbo.Participants", new[] { "StudyMeeting_Id" });
            AlterColumn("dbo.Participants", "StudyMeeting_Id", c => c.Int());
            CreateIndex("dbo.Participants", "StudyMeeting_Id");
            AddForeignKey("dbo.Participants", "StudyMeeting_Id", "dbo.StudyMeetings", "Id");
        }
    }
}
