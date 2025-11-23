using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OvertimeManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserPassowrdChangeRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MustChangePassword",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MustChangePassword",
                table: "Employees");
        }
    }
}
