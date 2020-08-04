using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PollWebApi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "poll",
                columns: table => new
                {
                    poll_id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    poll_description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_poll", x => x.poll_id);
                });

            migrationBuilder.CreateTable(
                name: "option",
                columns: table => new
                {
                    option_id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    poll_id = table.Column<long>(nullable: false),
                    option_description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_option", x => x.option_id);
                    table.ForeignKey(
                        name: "FK_option_poll_poll_id",
                        column: x => x.poll_id,
                        principalTable: "poll",
                        principalColumn: "poll_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pollview",
                columns: table => new
                {
                    pollview_id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    poll_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pollview", x => x.pollview_id);
                    table.ForeignKey(
                        name: "FK_pollview_poll_poll_id",
                        column: x => x.poll_id,
                        principalTable: "poll",
                        principalColumn: "poll_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vote",
                columns: table => new
                {
                    vote_id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    option_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vote", x => x.vote_id);
                    table.ForeignKey(
                        name: "FK_vote_option_option_id",
                        column: x => x.option_id,
                        principalTable: "option",
                        principalColumn: "option_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_option_poll_id",
                table: "option",
                column: "poll_id");

            migrationBuilder.CreateIndex(
                name: "IX_pollview_poll_id",
                table: "pollview",
                column: "poll_id");

            migrationBuilder.CreateIndex(
                name: "IX_vote_option_id",
                table: "vote",
                column: "option_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pollview");

            migrationBuilder.DropTable(
                name: "vote");

            migrationBuilder.DropTable(
                name: "option");

            migrationBuilder.DropTable(
                name: "poll");
        }
    }
}
