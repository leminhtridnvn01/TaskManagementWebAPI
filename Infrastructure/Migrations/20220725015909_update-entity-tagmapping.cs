using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class updateentitytagmapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagMappings_Tags_TagId",
                table: "TagMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_TagMappings_TaskItems_TaskId",
                table: "TagMappings");

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "TagMappings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagMappings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TagMappings_Tags_TagId",
                table: "TagMappings",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagMappings_TaskItems_TaskId",
                table: "TagMappings",
                column: "TaskId",
                principalTable: "TaskItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagMappings_Tags_TagId",
                table: "TagMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_TagMappings_TaskItems_TaskId",
                table: "TagMappings");

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "TagMappings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagMappings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TagMappings_Tags_TagId",
                table: "TagMappings",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TagMappings_TaskItems_TaskId",
                table: "TagMappings",
                column: "TaskId",
                principalTable: "TaskItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
