using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations.AppDbContextMigrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    gid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    decription = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.gid);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    lid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    content = table.Column<string>(type: "ntext", nullable: true),
                    pathimage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    postdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    latitude = table.Column<float>(type: "real", nullable: false),
                    longtitude = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.lid);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    nid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    content = table.Column<string>(type: "ntext", nullable: true),
                    pathimage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    postdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.nid);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    pid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    content = table.Column<string>(type: "ntext", nullable: true),
                    typeinoff = table.Column<int>(name: "type_inoff", type: "int", nullable: false),
                    typeprogram = table.Column<int>(name: "type_program", type: "int", nullable: false),
                    md5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pathimagelist = table.Column<string>(name: "pathimage_list", type: "ntext", nullable: true),
                    createat = table.Column<DateTime>(name: "create_at", type: "datetime2", nullable: false),
                    heldon = table.Column<DateTime>(name: "held_on", type: "datetime2", nullable: false),
                    lid = table.Column<int>(name: "l_id", type: "int", nullable: false),
                    gid = table.Column<int>(name: "g_id", type: "int", nullable: false),
                    uid = table.Column<int>(name: "u_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.pid);
                });

            migrationBuilder.CreateTable(
                name: "Supports",
                columns: table => new
                {
                    sid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    content = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supports", x => x.sid);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    tid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    price = table.Column<decimal>(type: "Money", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    inventory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.tid);
                });

            migrationBuilder.CreateTable(
                name: "Unions",
                columns: table => new
                {
                    uid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    decription = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unions", x => x.uid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropTable(
                name: "Supports");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Unions");
        }
    }
}
