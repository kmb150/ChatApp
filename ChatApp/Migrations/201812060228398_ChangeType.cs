namespace ChatApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "FromUser", c => c.String());
            AlterColumn("dbo.Messages", "ToUser", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "ToUser", c => c.Int(nullable: false));
            AlterColumn("dbo.Messages", "FromUser", c => c.Int(nullable: false));
        }
    }
}
