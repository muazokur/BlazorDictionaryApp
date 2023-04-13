using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorDictionary.Infrastructure.Persistence.Migrations
{
    public partial class mig_entry_add_category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                schema: "dbo",
                table: "entry",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                schema: "dbo",
                table: "entry");
        }
    }
}
