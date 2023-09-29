using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastucture.Migrations
{
    public partial class _7mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CovenantResults_CovenantConditions_IdCondition",
                table: "CovenantResults");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialDatas_CovenantConditions_IdCondition",
                table: "FinancialDatas");

            migrationBuilder.DropIndex(
                name: "IX_FinancialDatas_IdCondition",
                table: "FinancialDatas");

            migrationBuilder.DropIndex(
                name: "IX_CovenantResults_IdCondition",
                table: "CovenantResults");

            migrationBuilder.AddColumn<int>(
                name: "CovenantResultIdResult",
                table: "CovenantConditions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinancialDataIdFinancialData",
                table: "CovenantConditions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CovenantConditions_CovenantResultIdResult",
                table: "CovenantConditions",
                column: "CovenantResultIdResult");

            migrationBuilder.CreateIndex(
                name: "IX_CovenantConditions_FinancialDataIdFinancialData",
                table: "CovenantConditions",
                column: "FinancialDataIdFinancialData");

            migrationBuilder.AddForeignKey(
                name: "FK_CovenantConditions_CovenantResults_CovenantResultIdResult",
                table: "CovenantConditions",
                column: "CovenantResultIdResult",
                principalTable: "CovenantResults",
                principalColumn: "IdResult");

            migrationBuilder.AddForeignKey(
                name: "FK_CovenantConditions_FinancialDatas_FinancialDataIdFinancialData",
                table: "CovenantConditions",
                column: "FinancialDataIdFinancialData",
                principalTable: "FinancialDatas",
                principalColumn: "IdFinancialData");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CovenantConditions_CovenantResults_CovenantResultIdResult",
                table: "CovenantConditions");

            migrationBuilder.DropForeignKey(
                name: "FK_CovenantConditions_FinancialDatas_FinancialDataIdFinancialData",
                table: "CovenantConditions");

            migrationBuilder.DropIndex(
                name: "IX_CovenantConditions_CovenantResultIdResult",
                table: "CovenantConditions");

            migrationBuilder.DropIndex(
                name: "IX_CovenantConditions_FinancialDataIdFinancialData",
                table: "CovenantConditions");

            migrationBuilder.DropColumn(
                name: "CovenantResultIdResult",
                table: "CovenantConditions");

            migrationBuilder.DropColumn(
                name: "FinancialDataIdFinancialData",
                table: "CovenantConditions");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialDatas_IdCondition",
                table: "FinancialDatas",
                column: "IdCondition",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CovenantResults_IdCondition",
                table: "CovenantResults",
                column: "IdCondition",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CovenantResults_CovenantConditions_IdCondition",
                table: "CovenantResults",
                column: "IdCondition",
                principalTable: "CovenantConditions",
                principalColumn: "IdCondition",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialDatas_CovenantConditions_IdCondition",
                table: "FinancialDatas",
                column: "IdCondition",
                principalTable: "CovenantConditions",
                principalColumn: "IdCondition",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
