using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learnify.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_add_prop_lesson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Lessons");
        }
    }
}
