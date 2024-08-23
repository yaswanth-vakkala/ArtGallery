using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtGalleryAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initializeauthdbtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4f3cd3ce-b56e-46b4-85c1-9d6598915c81",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7624de30-5481-414c-95a9-dd234a2ae697", "AQAAAAIAAYagAAAAEP13hF7+SBgJDG7HcJ8QmdWz1NBkJbLL8kzzzm2wgWzBHtPc7vaIG7BAbqF4GXFNGA==", "9221eeca-9858-4010-ac24-d0b486159e12" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4f3cd3ce-b56e-46b4-85c1-9d6598915c81",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "acc32ede-e3a0-4ca7-bb03-10b84c258c40", "AQAAAAIAAYagAAAAEGCkozYc0J5Yp10tR2EVNG3FND1k52X9I6WywksoRVh2rTnFMzpKObtYj3w0SdHAsA==", "16ec1502-b6a3-4cb8-b945-d0ec5989a275" });
        }
    }
}
