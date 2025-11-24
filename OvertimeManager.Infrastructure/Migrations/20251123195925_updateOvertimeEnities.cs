using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OvertimeManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateOvertimeEnities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OvertimeCompensationRequests_Employees_RequestedByEmployeeId",
                table: "OvertimeCompensationRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_OvertimeCompensationRequests_Employees_RequestedForEmployeeId",
                table: "OvertimeCompensationRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_OvertimeRequests_Employees_RequestedByEmployeeId",
                table: "OvertimeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_OvertimeRequests_Employees_RequestedForEmployeeId",
                table: "OvertimeRequests");

            migrationBuilder.DropColumn(
                name: "RequesedForEmployeeId",
                table: "OvertimeRequests");

            migrationBuilder.DropColumn(
                name: "RequesterdByEmployeeId",
                table: "OvertimeRequests");

            migrationBuilder.DropColumn(
                name: "RequesedForEmployeeId",
                table: "OvertimeCompensationRequests");

            migrationBuilder.DropColumn(
                name: "RequesterdByEmployeeId",
                table: "OvertimeCompensationRequests");

            migrationBuilder.AlterColumn<int>(
                name: "RequestedForEmployeeId",
                table: "OvertimeRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RequestedByEmployeeId",
                table: "OvertimeRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "OvertimeRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedByEmployeeId",
                table: "OvertimeRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RequestedForEmployeeId",
                table: "OvertimeCompensationRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RequestedByEmployeeId",
                table: "OvertimeCompensationRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OvertimeRequests_ApprovedByEmployeeId",
                table: "OvertimeRequests",
                column: "ApprovedByEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OvertimeCompensationRequests_Employees_RequestedByEmployeeId",
                table: "OvertimeCompensationRequests",
                column: "RequestedByEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OvertimeCompensationRequests_Employees_RequestedForEmployeeId",
                table: "OvertimeCompensationRequests",
                column: "RequestedForEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OvertimeRequests_Employees_ApprovedByEmployeeId",
                table: "OvertimeRequests",
                column: "ApprovedByEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OvertimeRequests_Employees_RequestedByEmployeeId",
                table: "OvertimeRequests",
                column: "RequestedByEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OvertimeRequests_Employees_RequestedForEmployeeId",
                table: "OvertimeRequests",
                column: "RequestedForEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OvertimeCompensationRequests_Employees_RequestedByEmployeeId",
                table: "OvertimeCompensationRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_OvertimeCompensationRequests_Employees_RequestedForEmployeeId",
                table: "OvertimeCompensationRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_OvertimeRequests_Employees_ApprovedByEmployeeId",
                table: "OvertimeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_OvertimeRequests_Employees_RequestedByEmployeeId",
                table: "OvertimeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_OvertimeRequests_Employees_RequestedForEmployeeId",
                table: "OvertimeRequests");

            migrationBuilder.DropIndex(
                name: "IX_OvertimeRequests_ApprovedByEmployeeId",
                table: "OvertimeRequests");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "OvertimeRequests");

            migrationBuilder.DropColumn(
                name: "ApprovedByEmployeeId",
                table: "OvertimeRequests");

            migrationBuilder.AlterColumn<int>(
                name: "RequestedForEmployeeId",
                table: "OvertimeRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RequestedByEmployeeId",
                table: "OvertimeRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "RequesedForEmployeeId",
                table: "OvertimeRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequesterdByEmployeeId",
                table: "OvertimeRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "RequestedForEmployeeId",
                table: "OvertimeCompensationRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RequestedByEmployeeId",
                table: "OvertimeCompensationRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "RequesedForEmployeeId",
                table: "OvertimeCompensationRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequesterdByEmployeeId",
                table: "OvertimeCompensationRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_OvertimeCompensationRequests_Employees_RequestedByEmployeeId",
                table: "OvertimeCompensationRequests",
                column: "RequestedByEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OvertimeCompensationRequests_Employees_RequestedForEmployeeId",
                table: "OvertimeCompensationRequests",
                column: "RequestedForEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OvertimeRequests_Employees_RequestedByEmployeeId",
                table: "OvertimeRequests",
                column: "RequestedByEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OvertimeRequests_Employees_RequestedForEmployeeId",
                table: "OvertimeRequests",
                column: "RequestedForEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
