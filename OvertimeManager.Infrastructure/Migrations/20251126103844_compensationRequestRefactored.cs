using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OvertimeManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class compensationRequestRefactored : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OvertimeRequestsStatusses");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "OvertimeCompensationRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedByEmployeeId",
                table: "OvertimeCompensationRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "OvertimeCompensationRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OvertimeCompensationRequests_ApprovedByEmployeeId",
                table: "OvertimeCompensationRequests",
                column: "ApprovedByEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OvertimeCompensationRequests_Employees_ApprovedByEmployeeId",
                table: "OvertimeCompensationRequests",
                column: "ApprovedByEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OvertimeCompensationRequests_Employees_ApprovedByEmployeeId",
                table: "OvertimeCompensationRequests");

            migrationBuilder.DropIndex(
                name: "IX_OvertimeCompensationRequests_ApprovedByEmployeeId",
                table: "OvertimeCompensationRequests");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "OvertimeCompensationRequests");

            migrationBuilder.DropColumn(
                name: "ApprovedByEmployeeId",
                table: "OvertimeCompensationRequests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "OvertimeCompensationRequests");

            migrationBuilder.CreateTable(
                name: "OvertimeRequestsStatusses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OvertimeRequestsStatusses", x => x.Id);
                });
        }
    }
}
