using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnsToSnakeCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "catalog",
                table: "products",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "catalog",
                table: "products",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "catalog",
                table: "products",
                newName: "is_active");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                schema: "catalog",
                table: "products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "catalog",
                table: "products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "is_active",
                schema: "catalog",
                table: "products",
                newName: "IsActive");
        }
    }
}
