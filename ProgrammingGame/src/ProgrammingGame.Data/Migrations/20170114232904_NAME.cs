using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProgrammingGame.Data.Migrations
{
    public partial class NAME : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Indicators_Characters_CharacterId",
                table: "Indicators");

            migrationBuilder.DropForeignKey(
                name: "FK_Indicators_IndicatorTypes_IndicatorTypeId",
                table: "Indicators");

            migrationBuilder.DropForeignKey(
                name: "FK_OwnedItems_Characters_CharacterId",
                table: "OwnedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OwnedItems_Item_ItemTypeId",
                table: "OwnedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemActions_Characters_CharacterId",
                table: "SystemActions");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemActions_SystemActionTypes_TypeId",
                table: "SystemActions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Characters_CharacterId",
                schema: "security",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CharacterId",
                schema: "security",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SystemActionTypes",
                table: "SystemActionTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SystemActions",
                table: "SystemActions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OwnedItems",
                table: "OwnedItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item",
                table: "Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IndicatorTypes",
                table: "IndicatorTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Indicators",
                table: "Indicators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Characters",
                table: "Characters");

            migrationBuilder.RenameTable(
                name: "SystemActionTypes",
                newName: "SystemActionType");

            migrationBuilder.RenameTable(
                name: "SystemActions",
                newName: "SystemAction");

            migrationBuilder.RenameTable(
                name: "OwnedItems",
                newName: "OwnedItem");

            migrationBuilder.RenameTable(
                name: "Item",
                newName: "ItemType");

            migrationBuilder.RenameTable(
                name: "IndicatorTypes",
                newName: "IndicatorType");

            migrationBuilder.RenameTable(
                name: "Indicators",
                newName: "Indicator");

            migrationBuilder.RenameTable(
                name: "Characters",
                newName: "Character");

            migrationBuilder.RenameIndex(
                name: "IX_SystemActions_TypeId",
                table: "SystemAction",
                newName: "IX_SystemAction_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_SystemActions_CharacterId",
                table: "SystemAction",
                newName: "IX_SystemAction_CharacterId");

            migrationBuilder.RenameIndex(
                name: "IX_OwnedItems_ItemTypeId",
                table: "OwnedItem",
                newName: "IX_OwnedItem_ItemTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Indicators_IndicatorTypeId",
                table: "Indicator",
                newName: "IX_Indicator_IndicatorTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SystemActionType",
                table: "SystemActionType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SystemAction",
                table: "SystemAction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OwnedItem",
                table: "OwnedItem",
                columns: new[] { "CharacterId", "ItemTypeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemType",
                table: "ItemType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndicatorType",
                table: "IndicatorType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Indicator",
                table: "Indicator",
                columns: new[] { "CharacterId", "IndicatorTypeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Character",
                table: "Character",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CharacterId",
                schema: "security",
                table: "Users",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Indicator_Character_CharacterId",
                table: "Indicator",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Indicator_IndicatorType_IndicatorTypeId",
                table: "Indicator",
                column: "IndicatorTypeId",
                principalTable: "IndicatorType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedItem_Character_CharacterId",
                table: "OwnedItem",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedItem_ItemType_ItemTypeId",
                table: "OwnedItem",
                column: "ItemTypeId",
                principalTable: "ItemType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemAction_Character_CharacterId",
                table: "SystemAction",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemAction_SystemActionType_TypeId",
                table: "SystemAction",
                column: "TypeId",
                principalTable: "SystemActionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Character_CharacterId",
                schema: "security",
                table: "Users",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Indicator_Character_CharacterId",
                table: "Indicator");

            migrationBuilder.DropForeignKey(
                name: "FK_Indicator_IndicatorType_IndicatorTypeId",
                table: "Indicator");

            migrationBuilder.DropForeignKey(
                name: "FK_OwnedItem_Character_CharacterId",
                table: "OwnedItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OwnedItem_ItemType_ItemTypeId",
                table: "OwnedItem");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemAction_Character_CharacterId",
                table: "SystemAction");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemAction_SystemActionType_TypeId",
                table: "SystemAction");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Character_CharacterId",
                schema: "security",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CharacterId",
                schema: "security",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SystemActionType",
                table: "SystemActionType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SystemAction",
                table: "SystemAction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OwnedItem",
                table: "OwnedItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemType",
                table: "ItemType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IndicatorType",
                table: "IndicatorType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Indicator",
                table: "Indicator");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Character",
                table: "Character");

            migrationBuilder.RenameTable(
                name: "SystemActionType",
                newName: "SystemActionTypes");

            migrationBuilder.RenameTable(
                name: "SystemAction",
                newName: "SystemActions");

            migrationBuilder.RenameTable(
                name: "OwnedItem",
                newName: "OwnedItems");

            migrationBuilder.RenameTable(
                name: "ItemType",
                newName: "Item");

            migrationBuilder.RenameTable(
                name: "IndicatorType",
                newName: "IndicatorTypes");

            migrationBuilder.RenameTable(
                name: "Indicator",
                newName: "Indicators");

            migrationBuilder.RenameTable(
                name: "Character",
                newName: "Characters");

            migrationBuilder.RenameIndex(
                name: "IX_SystemAction_TypeId",
                table: "SystemActions",
                newName: "IX_SystemActions_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_SystemAction_CharacterId",
                table: "SystemActions",
                newName: "IX_SystemActions_CharacterId");

            migrationBuilder.RenameIndex(
                name: "IX_OwnedItem_ItemTypeId",
                table: "OwnedItems",
                newName: "IX_OwnedItems_ItemTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Indicator_IndicatorTypeId",
                table: "Indicators",
                newName: "IX_Indicators_IndicatorTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SystemActionTypes",
                table: "SystemActionTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SystemActions",
                table: "SystemActions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OwnedItems",
                table: "OwnedItems",
                columns: new[] { "CharacterId", "ItemTypeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item",
                table: "Item",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndicatorTypes",
                table: "IndicatorTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Indicators",
                table: "Indicators",
                columns: new[] { "CharacterId", "IndicatorTypeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Characters",
                table: "Characters",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CharacterId",
                schema: "security",
                table: "Users",
                column: "CharacterId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Indicators_Characters_CharacterId",
                table: "Indicators",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Indicators_IndicatorTypes_IndicatorTypeId",
                table: "Indicators",
                column: "IndicatorTypeId",
                principalTable: "IndicatorTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedItems_Characters_CharacterId",
                table: "OwnedItems",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedItems_Item_ItemTypeId",
                table: "OwnedItems",
                column: "ItemTypeId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemActions_Characters_CharacterId",
                table: "SystemActions",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemActions_SystemActionTypes_TypeId",
                table: "SystemActions",
                column: "TypeId",
                principalTable: "SystemActionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Characters_CharacterId",
                schema: "security",
                table: "Users",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
