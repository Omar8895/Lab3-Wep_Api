using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab3.Migrations
{
    public partial class changesecondtablename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "User's Claim");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "User's Claim",
                newName: "IX_User's Claim_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User's Claim",
                table: "User's Claim",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User's Claim_AspNetUsers_UserId",
                table: "User's Claim",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User's Claim_AspNetUsers_UserId",
                table: "User's Claim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User's Claim",
                table: "User's Claim");

            migrationBuilder.RenameTable(
                name: "User's Claim",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameIndex(
                name: "IX_User's Claim_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
