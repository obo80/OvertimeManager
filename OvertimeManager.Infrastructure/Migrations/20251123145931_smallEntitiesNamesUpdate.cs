using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OvertimeManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class smallEntitiesNamesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnsetledOvertime",
                table: "OvertimeSummaries",
                newName: "UnsettledOvertime");

            migrationBuilder.RenameColumn(
                name: "SettledOvertimet",
                table: "OvertimeSummaries",
                newName: "SettledOvertime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnsettledOvertime",
                table: "OvertimeSummaries",
                newName: "UnsetledOvertime");

            migrationBuilder.RenameColumn(
                name: "SettledOvertime",
                table: "OvertimeSummaries",
                newName: "SettledOvertimet");
        }
    }
}
