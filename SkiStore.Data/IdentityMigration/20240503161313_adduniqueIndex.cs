using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkiStore.Data.IdentityMigration
{
    /// <inheritdoc />
    public partial class adduniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Address_AppUserId",
                table: "Address");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c0b71f33-57b5-4b18-8878-d24bda5e8e5a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c153887d-08b3-4326-bf5f-3fe65b7a110a", "AQAAAAIAAYagAAAAENkNzoU0mKWkvmMbKoYcJiEmMbsDiBg4AORX0em+a6RvWk/mfp/AEmKGcCG7Zb7ucA==", "fbd54898-94df-4bd1-8571-9f9b45b10903" });

            migrationBuilder.CreateIndex(
                name: "IX_Address_AppUserId",
                table: "Address",
                column: "AppUserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Address_AppUserId",
                table: "Address");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c0b71f33-57b5-4b18-8878-d24bda5e8e5a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2fe88a6e-8d02-4a4b-aae6-924dfba2246f", "AQAAAAIAAYagAAAAEHW0DBmFsQNAbURSh/cJpenjDsqHHkkSxqvvNCQALFNN8fO65OixlMqTeRnD2isSUQ==", "86068912-5229-405c-ad42-e5132bd75fec" });

            migrationBuilder.CreateIndex(
                name: "IX_Address_AppUserId",
                table: "Address",
                column: "AppUserId");
        }
    }
}
