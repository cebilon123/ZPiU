using Microsoft.EntityFrameworkCore.Migrations;

namespace Manage.Data.Migrations
{
    public partial class Add_ExternalId_To_Contractor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ExternalId",
                table: "Contractors",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Contractors");
        }
    }
}
