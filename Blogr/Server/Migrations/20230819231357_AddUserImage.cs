using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogr.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddUserImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Images_u_Photoi_Id",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "u_Photoi_Id",
                table: "AspNetUsers",
                newName: "u_PhotoId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_u_Photoi_Id",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_u_PhotoId");

            migrationBuilder.CreateTable(
                name: "UserImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImages", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserImages_u_PhotoId",
                table: "AspNetUsers",
                column: "u_PhotoId",
                principalTable: "UserImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserImages_u_PhotoId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "UserImages");

            migrationBuilder.RenameColumn(
                name: "u_PhotoId",
                table: "AspNetUsers",
                newName: "u_Photoi_Id");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_u_PhotoId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_u_Photoi_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Images_u_Photoi_Id",
                table: "AspNetUsers",
                column: "u_Photoi_Id",
                principalTable: "Images",
                principalColumn: "i_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
