using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusiciansBlog.API.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MusiciansBlog");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "MusiciansBlog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    passwordHash = table.Column<string>(type: "text", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    refreshToken = table.Column<string>(type: "text", nullable: false),
                    refreshTokenExpiry = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                schema: "MusiciansBlog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    authorId = table.Column<Guid>(type: "uuid", nullable: false),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    likeCount = table.Column<int>(type: "integer", nullable: false),
                    dislikeCount = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "varchar(200)", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.id);
                    table.ForeignKey(
                        name: "FK_Blogs_Users_authorId",
                        column: x => x.authorId,
                        principalSchema: "MusiciansBlog",
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                schema: "MusiciansBlog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    authorId = table.Column<Guid>(type: "uuid", nullable: false),
                    parentBlogId = table.Column<Guid>(type: "uuid", nullable: false),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    likeCount = table.Column<int>(type: "integer", nullable: false),
                    dislikeCount = table.Column<int>(type: "integer", nullable: false),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.id);
                    table.ForeignKey(
                        name: "FK_comments_Blogs_parentBlogId",
                        column: x => x.parentBlogId,
                        principalSchema: "MusiciansBlog",
                        principalTable: "Blogs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comments_Users_authorId",
                        column: x => x.authorId,
                        principalSchema: "MusiciansBlog",
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_authorId",
                schema: "MusiciansBlog",
                table: "Blogs",
                column: "authorId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_title",
                schema: "MusiciansBlog",
                table: "Blogs",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "IX_comments_authorId",
                schema: "MusiciansBlog",
                table: "comments",
                column: "authorId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_parentBlogId",
                schema: "MusiciansBlog",
                table: "comments",
                column: "parentBlogId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments",
                schema: "MusiciansBlog");

            migrationBuilder.DropTable(
                name: "Blogs",
                schema: "MusiciansBlog");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "MusiciansBlog");
        }
    }
}
