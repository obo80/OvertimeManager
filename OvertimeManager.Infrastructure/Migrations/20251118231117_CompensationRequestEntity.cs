using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OvertimeManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CompensationRequestEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OvertimeCompensationRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Multiplier = table.Column<double>(type: "float", nullable: false),
                    RequestedTime = table.Column<double>(type: "float", nullable: false),
                    CompensatedTime = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedForDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequesterdByEmployeeId = table.Column<int>(type: "int", nullable: false),
                    RequestedByEmployeeId = table.Column<int>(type: "int", nullable: true),
                    RequesedForEmployeeId = table.Column<int>(type: "int", nullable: false),
                    RequestedForEmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OvertimeCompensationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OvertimeCompensationRequests_Employees_RequestedByEmployeeId",
                        column: x => x.RequestedByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OvertimeCompensationRequests_Employees_RequestedForEmployeeId",
                        column: x => x.RequestedForEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OvertimeCompensationRequests_RequestedByEmployeeId",
                table: "OvertimeCompensationRequests",
                column: "RequestedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_OvertimeCompensationRequests_RequestedForEmployeeId",
                table: "OvertimeCompensationRequests",
                column: "RequestedForEmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OvertimeCompensationRequests");
        }
    }
}
