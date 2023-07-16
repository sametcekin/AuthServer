using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecurityStampIsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("17c52fda-d109-44cf-a64c-9dbfc00f24b8"),
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEC1wGVoFmnjF68CstkcPB35NY6Ez6JkJgYpfIp0c7hoqjF/a2HPlF6H+k+47tRrJ8A==", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("17c52fda-d109-44cf-a64c-9dbfc00f24b8"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEIJZ/m85ANLTujrFHK3OAQLQ92FtKGLwo8SZuLetzWzXr4tYLYz25n6UZTyz3TkD0w==");
        }
    }
}
