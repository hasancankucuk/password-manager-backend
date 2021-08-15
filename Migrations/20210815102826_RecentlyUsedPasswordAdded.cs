using Microsoft.EntityFrameworkCore.Migrations;

namespace password_manager_backend.Migrations
{
    public partial class RecentlyUsedPasswordAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecentlyUsedPasswords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserInfoModelId = table.Column<int>(type: "int", nullable: false),
                    SaveAccountInfoModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecentlyUsedPasswords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecentlyUsedPasswords_SaveAccountInfoModel_SaveAccountInfoModelId",
                        column: x => x.SaveAccountInfoModelId,
                        principalTable: "SaveAccountInfoModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_RecentlyUsedPasswords_UserInfoModel_UserInfoModelId",
                        column: x => x.UserInfoModelId,
                        principalTable: "UserInfoModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecentlyUsedPasswords_SaveAccountInfoModelId",
                table: "RecentlyUsedPasswords",
                column: "SaveAccountInfoModelId");

            migrationBuilder.CreateIndex(
                name: "IX_RecentlyUsedPasswords_UserInfoModelId",
                table: "RecentlyUsedPasswords",
                column: "UserInfoModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecentlyUsedPasswords");
        }
    }
}
