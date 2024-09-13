using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FAV_Site.Migrations
{
    /// <inheritdoc />
    public partial class changeImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image_Couv",
                table: "Image_Produits",
                newName: "Image_couv");

            migrationBuilder.RenameColumn(
                name: "Id_Produit",
                table: "Image_Produits",
                newName: "Id_produit");

            migrationBuilder.RenameColumn(
                name: "Id_Image",
                table: "Image_Produits",
                newName: "Id_image");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image_couv",
                table: "Image_Produits",
                newName: "Image_Couv");

            migrationBuilder.RenameColumn(
                name: "Id_produit",
                table: "Image_Produits",
                newName: "Id_Produit");

            migrationBuilder.RenameColumn(
                name: "Id_image",
                table: "Image_Produits",
                newName: "Id_Image");
        }
    }
}
