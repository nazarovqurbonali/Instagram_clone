using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatefollowing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FollowingRelationShips_UserId",
                table: "FollowingRelationShips");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "FollowingRelationShips",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "FollowingId",
                table: "FollowingRelationShips",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.CreateIndex(
                name: "IX_FollowingRelationShips_UserId_FollowingId",
                table: "FollowingRelationShips",
                columns: new[] { "UserId", "FollowingId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FollowingRelationShips_UserId_FollowingId",
                table: "FollowingRelationShips");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "FollowingRelationShips",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "FollowingId",
                table: "FollowingRelationShips",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.CreateIndex(
                name: "IX_FollowingRelationShips_UserId",
                table: "FollowingRelationShips",
                column: "UserId");
        }
    }
}
