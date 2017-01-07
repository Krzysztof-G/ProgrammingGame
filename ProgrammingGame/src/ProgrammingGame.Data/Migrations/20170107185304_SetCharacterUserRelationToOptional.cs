using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProgrammingGame.Data.Migrations
{
    public partial class SetCharacterUserRelationToOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Characters_CharacterId",
                schema: "security",
                table: "Users");

            migrationBuilder.AlterColumn<long>(
                name: "CharacterId",
                schema: "security",
                table: "Users",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Characters_CharacterId",
                schema: "security",
                table: "Users",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Characters_CharacterId",
                schema: "security",
                table: "Users");

            migrationBuilder.AlterColumn<long>(
                name: "CharacterId",
                schema: "security",
                table: "Users",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Characters_CharacterId",
                schema: "security",
                table: "Users",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
