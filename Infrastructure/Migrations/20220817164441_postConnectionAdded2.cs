using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class postConnectionAdded2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostConnections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VolunteerPostPostId = table.Column<int>(type: "int", nullable: false),
                    NeedfulPostPostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostConnections_Posts_NeedfulPostPostId",
                        column: x => x.NeedfulPostPostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PostConnections_Posts_VolunteerPostPostId",
                        column: x => x.VolunteerPostPostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostConnections_NeedfulPostPostId",
                table: "PostConnections",
                column: "NeedfulPostPostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostConnections_VolunteerPostPostId",
                table: "PostConnections",
                column: "VolunteerPostPostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostConnections");
        }
    }
}
