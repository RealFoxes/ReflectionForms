namespace TestReflectionForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SomeEntities", "SomeImage", c => c.Binary());
            DropColumn("dbo.SomeEntities", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SomeEntities", "Image", c => c.String(unicode: false));
            DropColumn("dbo.SomeEntities", "SomeImage");
        }
    }
}
