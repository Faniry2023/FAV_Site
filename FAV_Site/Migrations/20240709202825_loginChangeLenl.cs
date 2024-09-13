using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FAV_Site.Migrations
{
    /// <inheritdoc />
    public partial class loginChangeLenl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id_Log",
                table: "LoginUtilisateur",
                newName: "Id_log");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id_log",
                table: "LoginUtilisateur",
                newName: "Id_Log");
        }
    }
}
