using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkiStore.Data.IdentityMigrations
{
    /// <inheritdoc />
    public partial class seeding3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1ca1da10-9923-446c-a98b-10027f38742c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c0b71f33-57b5-4b18-8878-d24bda5e8e5a",
                columns: new[] { "ConcurrencyStamp", "Email", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "5760dd8c-d167-4978-829a-a75c53cae844", "emaderagheb@gmail.com", true, "EMADERAGHEB@GMAIL.COM", "EMADERAGHEB@GMAIL.COM", "AQAAAAIAAYagAAAAENOcPdZ/C65ee+j34re1ExH3kYMR2C1W8vZyqtvTyj7CAbvnKtMhJqPHQACdmdCwkA==", "94c4c06a-991c-4a4a-b7f1-089bb447009a", "emaderagheb@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DisplayName", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "15cb543d-19b9-494d-9e3d-3b157df0a955", 0, "77242f1f-5709-49f5-9102-711c918edac2", "bob", "bob@", false, false, null, null, null, "AQAAAAIAAYagAAAAENlkbM1wbf9VCUFg/EXMh8lcrTDDap8bhw1GJhipCIKYbveUTXcmkb7e5BbH28kKkA==", null, false, "9d8636ec-2afc-413f-b783-36c68c34ed16", false, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "15cb543d-19b9-494d-9e3d-3b157df0a955");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c0b71f33-57b5-4b18-8878-d24bda5e8e5a",
                columns: new[] { "ConcurrencyStamp", "Email", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "f898b6a1-61f1-4676-a84f-08e53074e6f7", "Emaderagheb@gmail.com", false, null, null, "AQAAAAIAAYagAAAAEEdlgN352VrTtnLRs+rnJMHHaoo5ohUSPBYW8aSHPwFqLZQqSQQFHaL9ot6AcLCFkw==", "f1705e05-e64e-47c6-990c-a84aaf839aea", "Emaderagheb@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DisplayName", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1ca1da10-9923-446c-a98b-10027f38742c", 0, "0a4d51eb-a314-40c2-9244-1101408c4a5a", "bob", "bob@", false, false, null, null, null, "AQAAAAIAAYagAAAAEOgpo58RdOY6JZn9/p51wcBqToTLDSRRtNo2H898UVZRI6K+YFKUseyl5T7Uxxpczw==", null, false, "c7f684e9-b621-4290-9a33-3917a689485b", false, null });
        }
    }
}
