using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityAndAuthorizationServer.Migrations
{
    public partial class UserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicConversation_Users_UserName",
                table: "PublicConversation");

            migrationBuilder.DropIndex(
                name: "IX_PublicConversation_UserName",
                table: "PublicConversation");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "PublicConversation");

            migrationBuilder.CreateIndex(
                name: "IX_PublicConversation_UserId",
                table: "PublicConversation",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicConversation_Users_UserId",
                table: "PublicConversation",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicConversation_Users_UserId",
                table: "PublicConversation");

            migrationBuilder.DropIndex(
                name: "IX_PublicConversation_UserId",
                table: "PublicConversation");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "PublicConversation",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PublicConversation_UserName",
                table: "PublicConversation",
                column: "UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicConversation_Users_UserName",
                table: "PublicConversation",
                column: "UserName",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
