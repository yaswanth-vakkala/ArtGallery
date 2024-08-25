using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtGalleryAPI.Migrations
{
    /// <inheritdoc />
    public partial class editusertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "CountryCode",
                table: "AspNetUsers",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4f3cd3ce-b56e-46b4-85c1-9d6598915c81",
                columns: new[] { "ConcurrencyStamp", "CountryCode", "CreatedAt", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c27a2702-70f4-463b-bc55-1efe8dde3463", null, new DateTime(2024, 8, 25, 17, 5, 4, 705, DateTimeKind.Utc).AddTicks(4691), null, "AQAAAAIAAYagAAAAEFciXYqbrgrOkpLWmhrcbecDW9ZBxEAH1sD6dIfDw7zj7s1HpTYC0CtcGctSGpKECQ==", "134e110f-7335-42bd-b549-dec7e2ac839c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CountryCode",
                table: "AspNetUsers",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4f3cd3ce-b56e-46b4-85c1-9d6598915c81",
                columns: new[] { "ConcurrencyStamp", "CountryCode", "CreatedAt", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "00e6d72c-3948-4f5e-82aa-7b22230fc988", "+91", new DateTime(2024, 8, 25, 15, 58, 37, 257, DateTimeKind.Utc).AddTicks(7137), "admin", "AQAAAAIAAYagAAAAEEy2DFrYtebFLz29n8iDlktN5TlE7pdnS/f5PqJDUaoM/3ZpwB9JyokS8nuFyjL7HQ==", "ca609980-3a6f-47af-930b-3ea82e9175e9" });
        }
    }
}
