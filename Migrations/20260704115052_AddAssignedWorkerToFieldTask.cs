using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QR_Field_Monitoring_System.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignedWorkerToFieldTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedWorkerId",
                table: "FieldTasks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FieldTasks_AssignedWorkerId",
                table: "FieldTasks",
                column: "AssignedWorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldTasks_AspNetUsers_AssignedWorkerId",
                table: "FieldTasks",
                column: "AssignedWorkerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldTasks_AspNetUsers_AssignedWorkerId",
                table: "FieldTasks");

            migrationBuilder.DropIndex(
                name: "IX_FieldTasks_AssignedWorkerId",
                table: "FieldTasks");

            migrationBuilder.DropColumn(
                name: "AssignedWorkerId",
                table: "FieldTasks");
        }
    }
}
