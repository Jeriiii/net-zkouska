namespace netZkouska.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exam",
                c => new
                    {
                        ExamID = c.Int(nullable: false, identity: true),
                        Points = c.Int(nullable: false),
                        ExamDate = c.DateTime(nullable: false),
                        Student_StudentID = c.Int(),
                    })
                .PrimaryKey(t => t.ExamID)
                .ForeignKey("dbo.Student", t => t.Student_StudentID)
                .Index(t => t.Student_StudentID);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        StudentID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        surname = c.String(),
                    })
                .PrimaryKey(t => t.StudentID);
            
            CreateTable(
                "dbo.StudentWork",
                c => new
                    {
                        StudentWorkID = c.Int(nullable: false, identity: true),
                        Points = c.Int(nullable: false),
                        Commnent = c.String(),
                        WorkDate = c.DateTime(nullable: false),
                        Student_StudentID = c.Int(),
                    })
                .PrimaryKey(t => t.StudentWorkID)
                .ForeignKey("dbo.Student", t => t.Student_StudentID)
                .Index(t => t.Student_StudentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentWork", "Student_StudentID", "dbo.Student");
            DropForeignKey("dbo.Exam", "Student_StudentID", "dbo.Student");
            DropIndex("dbo.StudentWork", new[] { "Student_StudentID" });
            DropIndex("dbo.Exam", new[] { "Student_StudentID" });
            DropTable("dbo.StudentWork");
            DropTable("dbo.Student");
            DropTable("dbo.Exam");
        }
    }
}
