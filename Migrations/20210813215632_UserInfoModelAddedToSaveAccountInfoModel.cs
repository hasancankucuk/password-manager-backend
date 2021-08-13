using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace password_manager_backend.Migrations
{
    public partial class UserInfoModelAddedToSaveAccountInfoModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userMail",
                table: "UserLoginModel");

            migrationBuilder.DropColumn(
                name: "userPassword",
                table: "UserLoginModel");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "UserLoginModel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserLoginModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserInfoModelId",
                table: "SaveAccountInfoModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SaveAccountInfoModel_UserInfoModelId",
                table: "SaveAccountInfoModel",
                column: "UserInfoModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaveAccountInfoModel_UserInfoModel_UserInfoModelId",
                table: "SaveAccountInfoModel",
                column: "UserInfoModelId",
                principalTable: "UserInfoModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaveAccountInfoModel_UserInfoModel_UserInfoModelId",
                table: "SaveAccountInfoModel");

            migrationBuilder.DropIndex(
                name: "IX_SaveAccountInfoModel_UserInfoModelId",
                table: "SaveAccountInfoModel");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "UserLoginModel");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserLoginModel");

            migrationBuilder.DropColumn(
                name: "UserInfoModelId",
                table: "SaveAccountInfoModel");

            migrationBuilder.AddColumn<string>(
                name: "userMail",
                table: "UserLoginModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userPassword",
                table: "UserLoginModel",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
