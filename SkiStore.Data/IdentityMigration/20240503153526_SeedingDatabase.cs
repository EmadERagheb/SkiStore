using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkiStore.Data.IdentityMigration
{
    /// <inheritdoc />
    public partial class SeedingDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DisplayName", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c0b71f33-57b5-4b18-8878-d24bda5e8e5a", 0, "2fe88a6e-8d02-4a4b-aae6-924dfba2246f", "Emad", "emaderagheb@gmail.com", true, false, null, "EMADERAGHEB@GMAIL.COM", "EMADERAGHEB@GMAIL.COM", "AQAAAAIAAYagAAAAEHW0DBmFsQNAbURSh/cJpenjDsqHHkkSxqvvNCQALFNN8fO65OixlMqTeRnD2isSUQ==", null, false, "86068912-5229-405c-ad42-e5132bd75fec", false, "emaderagheb@gmail.com" });

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
