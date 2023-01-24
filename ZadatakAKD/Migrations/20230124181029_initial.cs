using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZadatakAKD.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Knjigas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutorId = table.Column<int>(type: "int", nullable: false),
                    Naslov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GodinaIzdavanja = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knjigas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Knjigas_Autors_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Autors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Knjigas_AutorId",
                table: "Knjigas",
                column: "AutorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Knjigas");

            migrationBuilder.DropTable(
                name: "Autors");
        }
    }
}
