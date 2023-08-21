using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogr.Server.Migrations
{
    /// <inheritdoc />
    public partial class BlogContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "b_Content",
                table: "Blogs");

            migrationBuilder.AddColumn<int>(
                name: "b_ContentId",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BlogContent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    path = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogContent", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_b_ContentId",
                table: "Blogs",
                column: "b_ContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_BlogContent_b_ContentId",
                table: "Blogs",
                column: "b_ContentId",
                principalTable: "BlogContent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_BlogContent_b_ContentId",
                table: "Blogs");

            migrationBuilder.DropTable(
                name: "BlogContent");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_b_ContentId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "b_ContentId",
                table: "Blogs");

            migrationBuilder.AddColumn<string>(
                name: "b_Content",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
