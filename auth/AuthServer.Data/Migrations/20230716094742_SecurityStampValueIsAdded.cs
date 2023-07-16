using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace AuthServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecurityStampValueIsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("17c52fda-d109-44cf-a64c-9dbfc00f24b8"),
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAECEJxPgT46o42xgljOEDySLqnLk8lQ6GYewUypKOA4pFSJR2O36oHIgHUGOq0DM3TQ==", "17c52fda-d109-44cf-a64c-9dbfc00f24b8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("17c52fda-d109-44cf-a64c-9dbfc00f24b8"),
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEC1wGVoFmnjF68CstkcPB35NY6Ez6JkJgYpfIp0c7hoqjF/a2HPlF6H+k+47tRrJ8A==", null });
        }
    }
}
