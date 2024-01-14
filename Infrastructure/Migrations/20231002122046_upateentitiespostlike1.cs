using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class upateentitiespostlike1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PostUserLikes",
                table: "PostUserLikes");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PostUserLikes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostUserLikes",
                table: "PostUserLikes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PostUserLikes_UserId",
                table: "PostUserLikes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PostUserLikes",
                table: "PostUserLikes");

            migrationBuilder.DropIndex(
                name: "IX_PostUserLikes_UserId",
                table: "PostUserLikes");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PostUserLikes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostUserLikes",
                table: "PostUserLikes",
                columns: new[] { "UserId", "PostLikeId" });
        }
    }
}
