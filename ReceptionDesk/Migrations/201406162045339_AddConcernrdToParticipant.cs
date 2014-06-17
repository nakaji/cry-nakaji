namespace ReceptionDesk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConcernrdToParticipant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Participants", "IsConcerned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Participants", "IsConcerned");
        }
    }
}
