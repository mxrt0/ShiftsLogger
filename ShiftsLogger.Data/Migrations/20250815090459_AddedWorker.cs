using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftsLogger.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedWorker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "Shifts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_WorkerId",
                table: "Shifts",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Workers_WorkerId",
                table: "Shifts",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Workers_WorkerId",
                table: "Shifts");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_WorkerId",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Shifts");
        }
    }
}
