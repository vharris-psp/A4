using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A4.Migrations
{
    /// <inheritdoc />
    public partial class AddPOFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "POs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "POs",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "POs");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "POs");
        }
    }
}
