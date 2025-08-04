using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusiciansBlog.API.Migrations
{
    /// <inheritdoc />
    public partial class AuthTypeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "authType",
                schema: "MusiciansBlog",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "authType",
                schema: "MusiciansBlog",
                table: "Users");
        }
    }
}
