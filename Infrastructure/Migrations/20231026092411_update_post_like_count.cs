using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_post_like_count : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PostUserLikes_UserId",
                table: "PostUserLikes");

            migrationBuilder.CreateIndex(
                name: "IX_PostUserLikes_UserId_PostLikeId",
                table: "PostUserLikes",
                columns: new[] { "UserId", "PostLikeId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PostUserLikes_UserId_PostLikeId",
                table: "PostUserLikes");

            migrationBuilder.CreateIndex(
                name: "IX_PostUserLikes_UserId",
                table: "PostUserLikes",
                column: "UserId");
        }
    }
}
