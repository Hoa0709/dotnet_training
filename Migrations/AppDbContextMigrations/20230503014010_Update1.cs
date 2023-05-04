using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations.AppDbContextMigrations
{
    /// <inheritdoc />
    public partial class Update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Tickets",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "Programs",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    aid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    decription = table.Column<string>(type: "ntext", nullable: true),
                    pathimage = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.aid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropColumn(
                name: "name",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "price",
                table: "Programs");
        }
    }
}
