using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogr.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBlogsAndUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "u_CreationDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "u_FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "u_LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "u_Photo",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    b_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    b_Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    b_UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    b_CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    b_UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    b_Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.b_ID);
                    table.ForeignKey(
                        name: "FK_Blogs_AspNetUsers_b_UserId",
                        column: x => x.b_UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    i_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    i_Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Blogb_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.i_Id);
                    table.ForeignKey(
                        name: "FK_Images_Blogs_Blogb_ID",
                        column: x => x.Blogb_ID,
                        principalTable: "Blogs",
                        principalColumn: "b_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_b_UserId",
                table: "Blogs",
                column: "b_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_Blogb_ID",
                table: "Images",
                column: "Blogb_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropColumn(
                name: "u_CreationDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "u_FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "u_LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "u_Photo",
                table: "AspNetUsers");
        }
    }
}
