using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FAV_Site.Migrations
{
    /// <inheritdoc />
    public partial class changetout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date_achat",
                table: "Commandes");

            migrationBuilder.DropColumn(
                name: "Id_produit",
                table: "Commandes");

            migrationBuilder.DropColumn(
                name: "Id_uti_vendeur",
                table: "Commandes");

            migrationBuilder.RenameColumn(
                name: "IdProduit",
                table: "PanierModels",
                newName: "id_vendeur");

            migrationBuilder.RenameColumn(
                name: "Quantite",
                table: "Commandes",
                newName: "quantite");

            migrationBuilder.RenameColumn(
                name: "Ref_commande",
                table: "Commandes",
                newName: "lesIdProduit");

            migrationBuilder.RenameColumn(
                name: "Prix_a_payer",
                table: "Commandes",
                newName: "prixTotal");

            migrationBuilder.RenameColumn(
                name: "Lieu_livraiseon",
                table: "Commandes",
                newName: "Id_vendeur");

            migrationBuilder.RenameColumn(
                name: "Id_cmd",
                table: "Commandes",
                newName: "Id_commande");

            migrationBuilder.AddColumn<string>(
                name: "Id_produit",
                table: "PanierModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "prix_total",
                table: "PanierModels",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "quantité",
                table: "PanierModels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id_produit",
                table: "PanierModels");

            migrationBuilder.DropColumn(
                name: "prix_total",
                table: "PanierModels");

            migrationBuilder.DropColumn(
                name: "quantité",
                table: "PanierModels");

            migrationBuilder.RenameColumn(
                name: "id_vendeur",
                table: "PanierModels",
                newName: "IdProduit");

            migrationBuilder.RenameColumn(
                name: "quantite",
                table: "Commandes",
                newName: "Quantite");

            migrationBuilder.RenameColumn(
                name: "prixTotal",
                table: "Commandes",
                newName: "Prix_a_payer");

            migrationBuilder.RenameColumn(
                name: "lesIdProduit",
                table: "Commandes",
                newName: "Ref_commande");

            migrationBuilder.RenameColumn(
                name: "Id_vendeur",
                table: "Commandes",
                newName: "Lieu_livraiseon");

            migrationBuilder.RenameColumn(
                name: "Id_commande",
                table: "Commandes",
                newName: "Id_cmd");

            migrationBuilder.AddColumn<string>(
                name: "Date_achat",
                table: "Commandes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id_produit",
                table: "Commandes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id_uti_vendeur",
                table: "Commandes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
