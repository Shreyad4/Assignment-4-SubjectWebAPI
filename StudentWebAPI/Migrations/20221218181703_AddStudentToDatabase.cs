using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Studentdata",
                columns: table => new
                {
                    StudID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studentdata", x => x.StudID);
                });

            migrationBuilder.CreateTable(
                name: "Subjectdata",
                columns: table => new
                {
                    SubID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudID = table.Column<int>(type: "int", nullable: false),
                    SubName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubMaxMarks = table.Column<int>(type: "int", nullable: false),
                    SubMarks1 = table.Column<int>(type: "int", nullable: false),
                    SubMarks2 = table.Column<int>(type: "int", nullable: false),
                    SubMarks3 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjectdata", x => x.SubID);
                    table.ForeignKey(
                        name: "FK_Subjectdata_Studentdata_StudID",
                        column: x => x.StudID,
                        principalTable: "Studentdata",
                        principalColumn: "StudID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subjectdata_StudID",
                table: "Subjectdata",
                column: "StudID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subjectdata");

            migrationBuilder.DropTable(
                name: "Studentdata");
        }
    }
}
