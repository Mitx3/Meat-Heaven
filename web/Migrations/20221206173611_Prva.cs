using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web.Migrations
{
    public partial class Prva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kmetija",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lastnik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lokacija = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kmetija", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Izdelek",
                columns: table => new
                {
                    IzdelekID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IzdelekIme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IzdelekVrsta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IzdelekCena = table.Column<double>(type: "float", nullable: false),
                    RokNakupa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KmetijaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Izdelek", x => x.IzdelekID);
                    table.ForeignKey(
                        name: "FK_Izdelek_Kmetija_KmetijaID",
                        column: x => x.KmetijaID,
                        principalTable: "Kmetija",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Izdelek_KmetijaID",
                table: "Izdelek",
                column: "KmetijaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Izdelek");

            migrationBuilder.DropTable(
                name: "Kmetija");
        }
    }
}
