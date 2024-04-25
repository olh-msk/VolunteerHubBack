using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class TestUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "Email", "Name", "Password", "PhoneNumber", "Role", "Surname" },
                values: new object[] { 1, "Admin street", "admin@example.com", "Admin", new byte[] { 218, 49, 142, 22, 217, 252, 247, 124, 109, 0, 230, 127, 13, 159, 153, 241, 198, 18, 231, 211, 121, 153, 219, 80, 72, 249, 255, 91, 137, 192, 210, 60, 237, 223, 205, 159, 252, 193, 17, 250, 226, 239, 169, 34, 40, 168, 23, 154, 1, 18, 145, 73, 83, 35, 91, 153, 4, 16, 72, 77, 43, 55, 31, 66 }, "88005553535", 2, "Test" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "Email", "Name", "Password", "PhoneNumber", "Role", "Surname" },
                values: new object[] { 2, "Volunteer street", "volunteer@example.com", "Volunteer", new byte[] { 99, 75, 46, 160, 118, 173, 166, 203, 149, 66, 168, 76, 228, 217, 62, 28, 122, 239, 119, 97, 193, 144, 144, 174, 126, 40, 85, 241, 126, 172, 52, 124, 193, 254, 164, 70, 89, 111, 57, 116, 123, 166, 87, 127, 182, 160, 34, 229, 49, 74, 86, 30, 159, 87, 242, 34, 173, 108, 90, 106, 103, 22, 75, 104 }, "88005553535", 0, "Test" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "Email", "Name", "Password", "PhoneNumber", "Role", "Surname" },
                values: new object[] { 3, "Needful street", "needful@example.com", "Needful", new byte[] { 27, 212, 237, 60, 65, 101, 16, 189, 249, 137, 227, 146, 38, 28, 79, 23, 79, 224, 126, 171, 1, 61, 77, 13, 88, 35, 116, 198, 177, 117, 213, 74, 181, 201, 67, 124, 241, 109, 76, 40, 253, 125, 122, 90, 238, 171, 83, 163, 68, 178, 141, 242, 133, 196, 176, 7, 207, 13, 137, 162, 94, 122, 182, 234 }, "88005553535", 1, "Test" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);
        }
    }
}
