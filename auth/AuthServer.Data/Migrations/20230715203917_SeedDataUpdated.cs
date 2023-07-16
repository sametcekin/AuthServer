using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace AuthServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("17c52fda-d109-44cf-a64c-9dbfc00f24b8"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "17c52fda-d109-44cf-a64c-9dbfc00f24b8", "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAIAAYagAAAAELFQL0WaWNBJjSPIGRBHKDc+UoPVFGd6ZSUoO5j0sNS3xYlrvp5pPFC37U1kfuKMqQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("17c52fda-d109-44cf-a64c-9dbfc00f24b8"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "843365fb-d1f2-4a74-905f-9737ebc8d797", "ADMIN", null, "AQAAAAIAAYagAAAAEBKJIvk07i8fAcbhf+wU+VJqFENQvskjp5NK6CKPuVZJQc1CGuu26DmQ9ap0BTSk7A==" });
        }
    }
}
