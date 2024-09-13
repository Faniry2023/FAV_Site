using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FAV_Site.Migrations
{
    /// <inheritdoc />
    public partial class suppression : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gestions");

            migrationBuilder.DropTable(
                name: "Suggestions");

            migrationBuilder.AddColumn<bool>(
                name: "is_livrer",
                table: "Commandes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_livrer",
                table: "Commandes");

            migrationBuilder.CreateTable(
                name: "Gestions",
                columns: table => new
                {
                    Id_gestion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Aout = table.Column<double>(type: "float", nullable: false),
                    Avril = table.Column<double>(type: "float", nullable: false),
                    Decembre = table.Column<double>(type: "float", nullable: false),
                    Fevrier = table.Column<double>(type: "float", nullable: false),
                    Id_produit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Janvier = table.Column<double>(type: "float", nullable: false),
                    Juillet = table.Column<double>(type: "float", nullable: false),
                    Juin = table.Column<double>(type: "float", nullable: false),
                    Mai = table.Column<double>(type: "float", nullable: false),
                    Mars = table.Column<double>(type: "float", nullable: false),
                    NbTotal = table.Column<double>(type: "float", nullable: false),
                    Novembre = table.Column<double>(type: "float", nullable: false),
                    Octobre = table.Column<double>(type: "float", nullable: false),
                    Septembre = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gestions", x => x.Id_gestion);
                });

            migrationBuilder.CreateTable(
                name: "Suggestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id_utilisateur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nom_produit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestions", x => x.Id);
                });
        }
    }
}
