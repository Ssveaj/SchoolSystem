using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileConversionMS.Migrations
{
    /// <inheritdoc />
    public partial class firstinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LetterTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Template = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LetterType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FontType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LetterTemplates", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LetterTemplates");
        }
    }
}
