using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandyMan_.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldTemporalOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "TemporalOrders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "TemporalOrders");
        }
    }
}
