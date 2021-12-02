using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameService.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerOne = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerTwo = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Finished = table.Column<bool>(type: "bit", nullable: false),
                    Rounds = table.Column<int>(type: "int", nullable: false),
                    TeamOnePoints = table.Column<int>(type: "int", nullable: false),
                    TeamOneTeamId = table.Column<int>(type: "int", nullable: true),
                    TeamTwoPoints = table.Column<int>(type: "int", nullable: false),
                    TeamTwoTeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Games_Teams_TeamOneTeamId",
                        column: x => x.TeamOneTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Teams_TeamTwoTeamId",
                        column: x => x.TeamTwoTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_TeamOneTeamId",
                table: "Games",
                column: "TeamOneTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_TeamTwoTeamId",
                table: "Games",
                column: "TeamTwoTeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
