using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FAV_Site.Migrations
{
    /// <inheritdoc />
    public partial class changeToutes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nb_reste",
                table: "Publicites");

            migrationBuilder.DropColumn(
                name: "Nb_total",
                table: "Publicites");

            migrationBuilder.DropColumn(
                name: "Prix",
                table: "Publicites");

            migrationBuilder.DropColumn(
                name: "Reservation",
                table: "Publicites");

            migrationBuilder.DropColumn(
                name: "Id_commande",
                table: "Historiques");

            migrationBuilder.DropColumn(
                name: "Quantite",
                table: "Historiques");

            migrationBuilder.RenameColumn(
                name: "Nom_produit",
                table: "Historiques",
                newName: "les_quantite");

            migrationBuilder.RenameColumn(
                name: "Id_produit",
                table: "Historiques",
                newName: "les_id_produit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "les_quantite",
                table: "Historiques",
                newName: "Nom_produit");

            migrationBuilder.RenameColumn(
                name: "les_id_produit",
                table: "Historiques",
                newName: "Id_produit");

            migrationBuilder.AddColumn<int>(
                name: "Nb_reste",
                table: "Publicites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Nb_total",
                table: "Publicites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Prix",
                table: "Publicites",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "Reservation",
                table: "Publicites",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Id_commande",
                table: "Historiques",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantite",
                table: "Historiques",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
