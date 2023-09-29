using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastucture.Migrations
{
    public partial class _2mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CovenantConditions_CovenantResults_ResultIdResult",
                table: "CovenantConditions");

            migrationBuilder.DropColumn(
                name: "IdCondition",
                table: "CovenantResults");

            migrationBuilder.RenameColumn(
                name: "ResultIdResult",
                table: "CovenantConditions",
                newName: "CovenantResultIdResult");

            migrationBuilder.RenameIndex(
                name: "IX_CovenantConditions_ResultIdResult",
                table: "CovenantConditions",
                newName: "IX_CovenantConditions_CovenantResultIdResult");

            migrationBuilder.AddForeignKey(
                name: "FK_CovenantConditions_CovenantResults_CovenantResultIdResult",
                table: "CovenantConditions",
                column: "CovenantResultIdResult",
                principalTable: "CovenantResults",
                principalColumn: "IdResult");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CovenantConditions_CovenantResults_CovenantResultIdResult",
                table: "CovenantConditions");

            migrationBuilder.RenameColumn(
                name: "CovenantResultIdResult",
                table: "CovenantConditions",
                newName: "ResultIdResult");

            migrationBuilder.RenameIndex(
                name: "IX_CovenantConditions_CovenantResultIdResult",
                table: "CovenantConditions",
                newName: "IX_CovenantConditions_ResultIdResult");

            migrationBuilder.AddColumn<int>(
                name: "IdCondition",
                table: "CovenantResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_CovenantConditions_CovenantResults_ResultIdResult",
                table: "CovenantConditions",
                column: "ResultIdResult",
                principalTable: "CovenantResults",
                principalColumn: "IdResult");
        }
    }
}
