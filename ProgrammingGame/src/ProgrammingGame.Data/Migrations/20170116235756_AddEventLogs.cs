using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProgrammingGame.Data.Migrations
{
    public partial class AddEventLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_CharacterId",
                schema: "security",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "EventLogType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLogType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventLog",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CharacterId = table.Column<long>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Header = table.Column<string>(nullable: true),
                    OccurrenceTime = table.Column<DateTime>(nullable: false),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventLog_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventLog_EventLogType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "EventLogType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CharacterId",
                schema: "security",
                table: "Users",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_EventLog_CharacterId",
                table: "EventLog",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_EventLog_TypeId",
                table: "EventLog",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventLog");

            migrationBuilder.DropTable(
                name: "EventLogType");

            migrationBuilder.DropIndex(
                name: "IX_Users_CharacterId",
                schema: "security",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CharacterId",
                schema: "security",
                table: "Users",
                column: "CharacterId",
                unique: true);
        }
    }
}
