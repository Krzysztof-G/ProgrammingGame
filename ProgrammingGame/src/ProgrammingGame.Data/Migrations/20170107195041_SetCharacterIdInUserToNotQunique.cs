using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProgrammingGame.Data.Migrations
{
    public partial class SetCharacterIdInUserToNotQunique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_CharacterId",
                schema: "security",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CharacterId",
                schema: "security",
                table: "Users",
                column: "CharacterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
