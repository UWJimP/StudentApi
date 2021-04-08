using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentApi.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classroom",
                columns: table => new
                {
                    EntityID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classroom", x => x.EntityID);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    EntityID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<short>(type: "smallint", nullable: false),
                    ClassroomEntityID = table.Column<long>(type: "bigint", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.EntityID);
                    table.ForeignKey(
                        name: "FK_Student_Classroom_ClassroomEntityID",
                        column: x => x.ClassroomEntityID,
                        principalTable: "Classroom",
                        principalColumn: "EntityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Classroom",
                columns: new[] { "EntityID", "ClassCode", "Description" },
                values: new object[,]
                {
                    { 1L, "COM101", null },
                    { 2L, "COM201", null },
                    { 3L, "ENG101", null },
                    { 4L, "CHI101", null },
                    { 5L, "MAT101", null },
                    { 6L, "MAT201", null }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "EntityID", "Age", "ClassroomEntityID", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1L, (short)16, null, "Moka", "Mogami" },
                    { 2L, (short)17, null, "Mary", "Jane" },
                    { 3L, (short)18, null, "Michael", "Vodka" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_ClassroomEntityID",
                table: "Student",
                column: "ClassroomEntityID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Classroom");
        }
    }
}
