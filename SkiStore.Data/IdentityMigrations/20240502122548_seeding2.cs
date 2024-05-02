using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkiStore.Data.IdentityMigrations
{
    /// <inheritdoc />
    public partial class seeding2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c0b71f33-57b5-4b18-8878-d24bda5e8e5a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f898b6a1-61f1-4676-a84f-08e53074e6f7", "AQAAAAIAAYagAAAAEEdlgN352VrTtnLRs+rnJMHHaoo5ohUSPBYW8aSHPwFqLZQqSQQFHaL9ot6AcLCFkw==", "f1705e05-e64e-47c6-990c-a84aaf839aea" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DisplayName", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1ca1da10-9923-446c-a98b-10027f38742c", 0, "0a4d51eb-a314-40c2-9244-1101408c4a5a", "bob", "bob@", false, false, null, null, null, "AQAAAAIAAYagAAAAEOgpo58RdOY6JZn9/p51wcBqToTLDSRRtNo2H898UVZRI6K+YFKUseyl5T7Uxxpczw==", null, false, "c7f684e9-b621-4290-9a33-3917a689485b", false, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1ca1da10-9923-446c-a98b-10027f38742c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c0b71f33-57b5-4b18-8878-d24bda5e8e5a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fcac67c8-cdb8-4188-b9c5-ac37f7e5a5af", "AQAAAAIAAYagAAAAELnBy8EC17o3W2ipPM51SDQO5AdDXdu7+/wPppo3iE+gRooceyZSaJ0BEtqjG7LCtA==", "c6faae91-bd98-4cf1-885c-2671c5767a24" });
        }
    }
}
