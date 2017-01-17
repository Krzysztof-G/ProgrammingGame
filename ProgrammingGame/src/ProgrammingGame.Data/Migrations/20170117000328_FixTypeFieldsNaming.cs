using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProgrammingGame.Data.Migrations
{
    public partial class FixTypeFieldsNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Indicator_IndicatorType_IndicatorTypeId",
                table: "Indicator");

            migrationBuilder.DropForeignKey(
                name: "FK_OwnedItem_ItemType_ItemTypeId",
                table: "OwnedItem");

            migrationBuilder.DropIndex(
                name: "IX_Users_CharacterId",
                schema: "security",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "ItemTypeId",
                table: "OwnedItem",
                newName: "TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_OwnedItem_ItemTypeId",
                table: "OwnedItem",
                newName: "IX_OwnedItem_TypeId");

            migrationBuilder.RenameColumn(
                name: "IndicatorTypeId",
                table: "Indicator",
                newName: "TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Indicator_IndicatorTypeId",
                table: "Indicator",
                newName: "IX_Indicator_TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CharacterId",
                schema: "security",
                table: "Users",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Indicator_IndicatorType_TypeId",
                table: "Indicator",
                column: "TypeId",
                principalTable: "IndicatorType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedItem_ItemType_TypeId",
                table: "OwnedItem",
                column: "TypeId",
                principalTable: "ItemType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Indicator_IndicatorType_TypeId",
                table: "Indicator");

            migrationBuilder.DropForeignKey(
                name: "FK_OwnedItem_ItemType_TypeId",
                table: "OwnedItem");

            migrationBuilder.DropIndex(
                name: "IX_Users_CharacterId",
                schema: "security",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "OwnedItem",
                newName: "ItemTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_OwnedItem_TypeId",
                table: "OwnedItem",
                newName: "IX_OwnedItem_ItemTypeId");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Indicator",
                newName: "IndicatorTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Indicator_TypeId",
                table: "Indicator",
                newName: "IX_Indicator_IndicatorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CharacterId",
                schema: "security",
                table: "Users",
                column: "CharacterId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Indicator_IndicatorType_IndicatorTypeId",
                table: "Indicator",
                column: "IndicatorTypeId",
                principalTable: "IndicatorType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedItem_ItemType_ItemTypeId",
                table: "OwnedItem",
                column: "ItemTypeId",
                principalTable: "ItemType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
