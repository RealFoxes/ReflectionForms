namespace TestReflectionForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MyProperty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SomeEntities",
                c => new
                    {
                        SomeId = c.Int(nullable: false, identity: true),
                        SomeProp = c.Int(nullable: false),
                        someEnum = c.Int(nullable: false),
                        SomeDate = c.DateTime(nullable: false, precision: 0),
                        SomeString = c.String(unicode: false),
                        Image = c.String(unicode: false),
                        SomeRef_Id = c.Int(),
                    })
                .PrimaryKey(t => t.SomeId)
                .ForeignKey("dbo.Classes", t => t.SomeRef_Id)
                .Index(t => t.SomeRef_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SomeEntities", "SomeRef_Id", "dbo.Classes");
            DropIndex("dbo.SomeEntities", new[] { "SomeRef_Id" });
            DropTable("dbo.SomeEntities");
            DropTable("dbo.Classes");
        }
    }
}
