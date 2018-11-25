using Microsoft.EntityFrameworkCore.Migrations;

namespace THONK.Migrations
{
    public partial class Initial_create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guilds",
                columns: table => new
                {
                    GuildID = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Prefix = table.Column<string>(nullable: true),
                    ChannelGeneral = table.Column<ulong>(nullable: false),
                    ChannelAnnouncements = table.Column<ulong>(nullable: false),
                    ChannelBotLog = table.Column<ulong>(nullable: false),
                    ChannelLog = table.Column<ulong>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guilds", x => x.GuildID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guilds");
        }
    }
}
