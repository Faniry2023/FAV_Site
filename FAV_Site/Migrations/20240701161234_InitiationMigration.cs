using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FAV_Site.Migrations
{
    /// <inheritdoc />
    public partial class InitiationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Commandes",
                columns: table => new
                {
                    Id_cmd = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_acheteur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id_uti_vendeur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id_produit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    Prix_a_payer = table.Column<double>(type: "float", nullable: false),
                    Date_achat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lieu_livraiseon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ref_commande = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commandes", x => x.Id_cmd);
                });

            migrationBuilder.CreateTable(
                name: "Commentaires",
                columns: table => new
                {
                    Id_com = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_utilisateur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id_produit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Commentaire = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commentaires", x => x.Id_com);
                });

            migrationBuilder.CreateTable(
                name: "Gestions",
                columns: table => new
                {
                    Id_gestion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NbTotal = table.Column<double>(type: "float", nullable: false),
                    Janvier = table.Column<double>(type: "float", nullable: false),
                    Fevrier = table.Column<double>(type: "float", nullable: false),
                    Mars = table.Column<double>(type: "float", nullable: false),
                    Avril = table.Column<double>(type: "float", nullable: false),
                    Mai = table.Column<double>(type: "float", nullable: false),
                    Juin = table.Column<double>(type: "float", nullable: false),
                    Juillet = table.Column<double>(type: "float", nullable: false),
                    Aout = table.Column<double>(type: "float", nullable: false),
                    Septembre = table.Column<double>(type: "float", nullable: false),
                    Octobre = table.Column<double>(type: "float", nullable: false),
                    Novembre = table.Column<double>(type: "float", nullable: false),
                    Decembre = table.Column<double>(type: "float", nullable: false),
                    Id_produit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gestions", x => x.Id_gestion);
                });

            migrationBuilder.CreateTable(
                name: "Historiques",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_acheteur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id_vendeur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id_produit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id_commande = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prix_a_payser = table.Column<double>(type: "float", nullable: false),
                    Date_achat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nom_produit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historiques", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Image_Produits",
                columns: table => new
                {
                    Id_Image = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_Produit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image_Couv = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Image_1 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Image_2 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Image_3 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Image_4 = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image_Produits", x => x.Id_Image);
                });

            migrationBuilder.CreateTable(
                name: "LoginUtilisateur",
                columns: table => new
                {
                    Id_Log = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginUtilisateur", x => x.Id_Log);
                });

            migrationBuilder.CreateTable(
                name: "Produits",
                columns: table => new
                {
                    Id_produit = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_vendeur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id_utilisateur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nom_produit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marque = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_produit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_pub = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nb_produit_reste = table.Column<int>(type: "int", nullable: false),
                    Nb_total_prod = table.Column<int>(type: "int", nullable: false),
                    Prix = table.Column<double>(type: "float", nullable: false),
                    Prix_promo = table.Column<double>(type: "float", nullable: false),
                    Autre_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Categorie = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produits", x => x.Id_produit);
                });

            migrationBuilder.CreateTable(
                name: "Publicites",
                columns: table => new
                {
                    Id_pub = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_utilisateur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nom_pub = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descri_pub = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Autre_descri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prix = table.Column<double>(type: "float", nullable: false),
                    Nb_total = table.Column<int>(type: "int", nullable: false),
                    Nb_reste = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publicites", x => x.Id_pub);
                });

            migrationBuilder.CreateTable(
                name: "Suggestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_utilisateur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nom_produit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id_ut = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nom_ut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prenom_ut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_ut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pays_ut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ville_ut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationnelite_ut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type_ut = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id_ut);
                });

            migrationBuilder.CreateTable(
                name: "Vendeurs",
                columns: table => new
                {
                    Id_uti_ven = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_uti = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nom_Societe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LienVen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefLogiciel = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendeurs", x => x.Id_uti_ven);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Commandes");

            migrationBuilder.DropTable(
                name: "Commentaires");

            migrationBuilder.DropTable(
                name: "Gestions");

            migrationBuilder.DropTable(
                name: "Historiques");

            migrationBuilder.DropTable(
                name: "Image_Produits");

            migrationBuilder.DropTable(
                name: "LoginUtilisateur");

            migrationBuilder.DropTable(
                name: "Produits");

            migrationBuilder.DropTable(
                name: "Publicites");

            migrationBuilder.DropTable(
                name: "Suggestions");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropTable(
                name: "Vendeurs");
        }
    }
}
