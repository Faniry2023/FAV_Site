using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FAV_Site.Migrations
{
    /// <inheritdoc />
    public partial class Ajoutstat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "prixTotal",
                table: "Commandes",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Mois",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_produit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    jan = table.Column<int>(type: "int", nullable: false),
                    fev = table.Column<int>(type: "int", nullable: false),
                    mar = table.Column<int>(type: "int", nullable: false),
                    avr = table.Column<int>(type: "int", nullable: false),
                    mai = table.Column<int>(type: "int", nullable: false),
                    jui = table.Column<int>(type: "int", nullable: false),
                    juill = table.Column<int>(type: "int", nullable: false),
                    aou = table.Column<int>(type: "int", nullable: false),
                    sep = table.Column<int>(type: "int", nullable: false),
                    oct = table.Column<int>(type: "int", nullable: false),
                    nov = table.Column<int>(type: "int", nullable: false),
                    dec = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mois", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mois");

            migrationBuilder.AlterColumn<string>(
                name: "prixTotal",
                table: "Commandes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
