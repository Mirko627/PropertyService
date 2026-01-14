using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyService.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialPropertyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgencyId",
                table: "properties");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgencyId",
                table: "properties",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
