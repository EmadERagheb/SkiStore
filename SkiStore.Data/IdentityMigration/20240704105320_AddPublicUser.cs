using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkiStore.Data.IdentityMigration
{
    /// <inheritdoc />
    public partial class AddPublicUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c0b71f33-57b5-4b18-8878-d24bda5e8e5a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "98aadbc0-b45c-4dd6-b9a0-140c3b765681", "AQAAAAIAAYagAAAAEEuwn20TVu4aauXRD1UwwZP79HW8h/26MlHQW7NCkMsJHnU8ni6b/mDrzqtDW+Okow==", "0fd9cf23-8233-45c8-8119-431858c216f4" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DisplayName", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "21c033a9-a5d5-4fee-97e3-5d8c51563060", 0, "ed811078-b73e-4158-be0f-e2d07d039959", "Public", "public@skiNet.com", true, false, null, "PUBLIC@SKINET.COM", "PUBLIC@SKINET.COM", "AQAAAAIAAYagAAAAEGxrveZyscu8o6tKW1WNdHttgY0KcaHpAJKPUqsz9rOv0xUf7OsZAW+D+jVBBCuXNw==", null, false, "bc4ac595-6634-4bd1-92dd-c15aa8ba96db", false, "public@skiNet.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21c033a9-a5d5-4fee-97e3-5d8c51563060");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c0b71f33-57b5-4b18-8878-d24bda5e8e5a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c153887d-08b3-4326-bf5f-3fe65b7a110a", "AQAAAAIAAYagAAAAENkNzoU0mKWkvmMbKoYcJiEmMbsDiBg4AORX0em+a6RvWk/mfp/AEmKGcCG7Zb7ucA==", "fbd54898-94df-4bd1-8571-9f9b45b10903" });
        }
    }
}
