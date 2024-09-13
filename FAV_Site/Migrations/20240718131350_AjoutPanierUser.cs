using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FAV_Site.Migrations
{
    /// <inheritdoc />
    public partial class AjoutPanierUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "id_acheteur",
                table: "PanierModels",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "id_acheteur",
                table: "PanierModels");
        }
    }
}
