using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.API.Migrations
{
    /// <inheritdoc />
    public partial class CheckPendingModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9b016f5d-3c2f-4e12-bf9e-bf3cf88ef492"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEL1PmuH77B6HFaZ/zje5tI3K+Irso3y6o78VHZ88ZkqRj8geWEgjp8Cl2tFxRFH45Q==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9b016f5d-3c2f-4e12-bf9e-bf3cf88ef492"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEJJPaFNPxv2jJYEQf/E12ycbURHrMUGz4M7KOBVE00xKilCqvYli0dHlDTERXeB0Bg==");
        }
    }
}
