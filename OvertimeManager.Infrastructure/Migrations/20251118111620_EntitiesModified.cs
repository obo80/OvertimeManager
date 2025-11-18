using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OvertimeManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_OvertimeSummaries_OvertimeSummaryId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_OvertimeSummaryId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "Reason",
                table: "OvertimeRequests",
                newName: "BusinessJustificationReason");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "OvertimeSummaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OvertimeSummaryId1",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_OvertimeSummaryId1",
                table: "Employees",
                column: "OvertimeSummaryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_OvertimeSummaries_OvertimeSummaryId1",
                table: "Employees",
                column: "OvertimeSummaryId1",
                principalTable: "OvertimeSummaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_OvertimeSummaries_OvertimeSummaryId1",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_OvertimeSummaryId1",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "OvertimeSummaries");

            migrationBuilder.DropColumn(
                name: "OvertimeSummaryId1",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "BusinessJustificationReason",
                table: "OvertimeRequests",
                newName: "Reason");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_OvertimeSummaryId",
                table: "Employees",
                column: "OvertimeSummaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_OvertimeSummaries_OvertimeSummaryId",
                table: "Employees",
                column: "OvertimeSummaryId",
                principalTable: "OvertimeSummaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
