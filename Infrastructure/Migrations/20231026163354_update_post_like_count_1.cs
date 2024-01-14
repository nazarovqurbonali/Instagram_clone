using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_post_like_count_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "PostLike",
                table: "PostLikes");

            migrationBuilder.AddCheckConstraint(
                name: "PostLikes",
                table: "PostLikes",
                sql: " \"LikeCount\" >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "PostLikes",
                table: "PostLikes");

            migrationBuilder.AddCheckConstraint(
                name: "PostLike",
                table: "PostLikes",
                sql: " \"LikeCount\" >= 0");
        }
    }
}
