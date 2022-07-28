using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class updateentitytaskItem2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_Users_AssigneeInProgressId",
                table: "TaskItems");

            migrationBuilder.AlterColumn<int>(
                name: "AssigneeInProgressId",
                table: "TaskItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_Users_AssigneeInProgressId",
                table: "TaskItems",
                column: "AssigneeInProgressId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_Users_AssigneeInProgressId",
                table: "TaskItems");

            migrationBuilder.AlterColumn<int>(
                name: "AssigneeInProgressId",
                table: "TaskItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_Users_AssigneeInProgressId",
                table: "TaskItems",
                column: "AssigneeInProgressId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}