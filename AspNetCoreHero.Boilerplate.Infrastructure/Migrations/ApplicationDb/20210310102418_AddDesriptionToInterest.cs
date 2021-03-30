using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Migrations.ApplicationDb
{
    public partial class AddDesriptionToInterest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Interests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Interests");
        }
    }
}
