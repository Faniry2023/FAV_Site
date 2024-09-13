using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FAV_Site.Migrations
{
    /// <inheritdoc />
    public partial class AjoutImageLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgLocation1",
                table: "Image_Produits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgLocation2",
                table: "Image_Produits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgLocation3",
                table: "Image_Produits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgLocation4",
                table: "Image_Produits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgLocationCouv",
                table: "Image_Produits",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgLocation1",
                table: "Image_Produits");

            migrationBuilder.DropColumn(
                name: "ImgLocation2",
                table: "Image_Produits");

            migrationBuilder.DropColumn(
                name: "ImgLocation3",
                table: "Image_Produits");

            migrationBuilder.DropColumn(
                name: "ImgLocation4",
                table: "Image_Produits");

            migrationBuilder.DropColumn(
                name: "ImgLocationCouv",
                table: "Image_Produits");
        }
    }
}
