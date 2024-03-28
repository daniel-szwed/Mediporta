using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mediporta.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "PercentageShare",
                table: "Tags",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PercentageShare",
                table: "Tags");
        }
    }
}
