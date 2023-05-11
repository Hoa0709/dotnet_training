using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations.AppDbContextMigrations
{
    /// <inheritdoc />
    public partial class up5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TakeTicket",
                table: "BookTickets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TakeTicket",
                table: "BookTickets");
        }
    }
}
