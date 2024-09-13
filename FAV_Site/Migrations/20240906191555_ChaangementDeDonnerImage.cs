using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FAV_Site.Migrations
{
    /// <inheritdoc />
    public partial class ChaangementDeDonnerImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image_Produits");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_produit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrlCouv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl_4 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.CreateTable(
                name: "Image_Produits",
                columns: table => new
                {
                    Id_image = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_produit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image_1 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Image_2 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Image_3 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Image_4 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Image_couv = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ImgLocation1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgLocation2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgLocation3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgLocation4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgLocationCouv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image_Produits", x => x.Id_image);
                });
        }
    }
}
