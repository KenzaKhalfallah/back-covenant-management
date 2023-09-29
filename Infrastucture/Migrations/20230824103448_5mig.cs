using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastucture.Migrations
{
    public partial class _5mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CovenantConditions_CovenantResults_CovenantResultIdResult",
                table: "CovenantConditions");

            migrationBuilder.DropForeignKey(
                name: "FK_CovenantConditions_FinancialData_FinancialDataIdFinancialData",
                table: "CovenantConditions");

            migrationBuilder.DropIndex(
                name: "IX_CovenantConditions_CovenantResultIdResult",
                table: "CovenantConditions");

            migrationBuilder.DropIndex(
                name: "IX_CovenantConditions_FinancialDataIdFinancialData",
                table: "CovenantConditions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialData",
                table: "FinancialData");

            migrationBuilder.DropColumn(
                name: "CovenantResultIdResult",
                table: "CovenantConditions");

            migrationBuilder.DropColumn(
                name: "FinancialDataIdFinancialData",
                table: "CovenantConditions");

            migrationBuilder.RenameTable(
                name: "FinancialData",
                newName: "FinancialDatas");

            migrationBuilder.AddColumn<int>(
                name: "IdCondition",
                table: "CovenantResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdCondition",
                table: "FinancialDatas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialDatas",
                table: "FinancialDatas",
                column: "IdFinancialData");

            migrationBuilder.CreateIndex(
                name: "IX_CovenantResults_IdCondition",
                table: "CovenantResults",
                column: "IdCondition",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialDatas_IdCondition",
                table: "FinancialDatas",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CovenantResults_CovenantConditions_IdCondition",
                table: "CovenantResults");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialDatas_CovenantConditions_IdCondition",
                table: "FinancialDatas");

            migrationBuilder.DropIndex(
                name: "IX_CovenantResults_IdCondition",
                table: "CovenantResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialDatas",
                table: "FinancialDatas");

            migrationBuilder.DropIndex(
                name: "IX_FinancialDatas_IdCondition",
                table: "FinancialDatas");

            migrationBuilder.DropColumn(
                name: "IdCondition",
                table: "CovenantResults");

            migrationBuilder.DropColumn(
                name: "IdCondition",
                table: "FinancialDatas");

            migrationBuilder.RenameTable(
                name: "FinancialDatas",
                newName: "FinancialData");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialData",
                table: "FinancialData",
                column: "IdFinancialData");

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
                name: "FK_CovenantConditions_FinancialData_FinancialDataIdFinancialData",
                table: "CovenantConditions",
                column: "FinancialDataIdFinancialData",
                principalTable: "FinancialData",
                principalColumn: "IdFinancialData");
        }
    }
}
