using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkiStore.Data.IdentityMigrations
{
    /// <inheritdoc />
    public partial class SeedingUsersAndAddressTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DisplayName", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c0b71f33-57b5-4b18-8878-d24bda5e8e5a", 0, "fcac67c8-cdb8-4188-b9c5-ac37f7e5a5af", "Emad", "Emaderagheb@gmail.com", false, false, null, null, null, "AQAAAAIAAYagAAAAELnBy8EC17o3W2ipPM51SDQO5AdDXdu7+/wPppo3iE+gRooceyZSaJ0BEtqjG7LCtA==", null, false, "c6faae91-bd98-4cf1-885c-2671c5767a24", false, "Emaderagheb@gmail.com" });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "AppUserId", "City", "FirstName", "LastName", "State", "Street", "ZipCode" },
                values: new object[] { 1, "c0b71f33-57b5-4b18-8878-d24bda5e8e5a", "Cairo", "Emad", "Ragheb", "6th-October", "Harm City", "123456" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c0b71f33-57b5-4b18-8878-d24bda5e8e5a");
        }
    }
}
