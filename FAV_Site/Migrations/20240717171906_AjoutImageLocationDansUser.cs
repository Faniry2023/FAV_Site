using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FAV_Site.Migrations
{
    /// <inheritdoc />
    public partial class AjoutImageLocationDansUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgLocation",
                table: "Utilisateurs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgLocation",
                table: "Utilisateurs");
        }
    }
}
