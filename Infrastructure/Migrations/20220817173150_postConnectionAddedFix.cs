using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class postConnectionAddedFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostConnections_Posts_NeedfulPostPostId",
                table: "PostConnections");

            migrationBuilder.DropForeignKey(
                name: "FK_PostConnections_Posts_VolunteerPostPostId",
                table: "PostConnections");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Password",
                table: "Users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddForeignKey(
                name: "FK_PostConnections_Posts_NeedfulPostPostId",
                table: "PostConnections",
                column: "NeedfulPostPostId",
                principalTable: "Posts",
                principalColumn: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostConnections_Posts_VolunteerPostPostId",
                table: "PostConnections",
                column: "VolunteerPostPostId",
                principalTable: "Posts",
                principalColumn: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostConnections_Posts_NeedfulPostPostId",
                table: "PostConnections");

            migrationBuilder.DropForeignKey(
                name: "FK_PostConnections_Posts_VolunteerPostPostId",
                table: "PostConnections");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_PostConnections_Posts_NeedfulPostPostId",
                table: "PostConnections",
                column: "NeedfulPostPostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostConnections_Posts_VolunteerPostPostId",
                table: "PostConnections",
                column: "VolunteerPostPostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
