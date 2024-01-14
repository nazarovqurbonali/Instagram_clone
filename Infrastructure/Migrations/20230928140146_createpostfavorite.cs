using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class createpostfavorite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostFavorites",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "integer", nullable: false),
                    FavoriteCount = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostFavorites", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_PostFavorites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostFavorites_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostFavoriteUsers_PostFavoriteId",
                table: "PostFavoriteUsers",
                column: "PostFavoriteId");

            migrationBuilder.CreateIndex(
                name: "IX_PostFavorites_UserId",
                table: "PostFavorites",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostFavoriteUsers_PostFavorites_PostFavoriteId",
                table: "PostFavoriteUsers",
                column: "PostFavoriteId",
                principalTable: "PostFavorites",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostFavoriteUsers_PostFavorites_PostFavoriteId",
                table: "PostFavoriteUsers");

            migrationBuilder.DropTable(
                name: "PostFavorites");

            migrationBuilder.DropIndex(
                name: "IX_PostFavoriteUsers_PostFavoriteId",
                table: "PostFavoriteUsers");
        }
    }
}
