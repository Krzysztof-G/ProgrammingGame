using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProgrammingGame.Data.Migrations
{
    public partial class AddSystemActions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemActionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemActionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemActions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CharacterId = table.Column<long>(nullable: false),
                    LastExecutionTime = table.Column<DateTime>(nullable: false),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemActions_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemActions_SystemActionTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "SystemActionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemActions_CharacterId",
                table: "SystemActions",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemActions_TypeId",
                table: "SystemActions",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemActions");

            migrationBuilder.DropTable(
                name: "SystemActionTypes");
        }
    }
}
