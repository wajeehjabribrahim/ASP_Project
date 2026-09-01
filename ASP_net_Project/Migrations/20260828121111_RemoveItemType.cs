using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_net_Project.Migrations
{
    /// <inheritdoc />
    public partial class RemoveItemType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Items");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
