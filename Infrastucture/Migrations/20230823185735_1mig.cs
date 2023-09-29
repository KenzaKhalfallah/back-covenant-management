using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastucture.Migrations
{
    public partial class _1mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CovenantResults",
                columns: table => new
                {
                    IdResult = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResultStatus = table.Column<int>(type: "int", nullable: false),
                    ResultDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCondition = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CovenantResults", x => x.IdResult);
                });

            migrationBuilder.CreateTable(
                name: "Covenants",
                columns: table => new
                {
                    IdCovenant = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameCovenant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryCovenant = table.Column<int>(type: "int", nullable: false),
                    DescriptionCovenant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeCovenant = table.Column<int>(type: "int", nullable: false),
                    PeriodTypeCovenant = table.Column<int>(type: "int", nullable: false),
                    StatementSourceCovenant = table.Column<int>(type: "int", nullable: false),
                    LinkedLineItem = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Covenants", x => x.IdCovenant);
                });

            migrationBuilder.CreateTable(
                name: "FinancialData",
                columns: table => new
                {
                    IdFinancialData = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NetIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Interest = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Taxes = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalEquity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OperatingExpenses = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalRevenues = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Depreciation = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Amortization = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrentAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrentLiabilities = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GrossProfit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalDebt = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalDebtService = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InterestExpense = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CostOfGoodsSold = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AverageInventory = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialData", x => x.IdFinancialData);
                });

            migrationBuilder.CreateTable(
                name: "ResultNotes",
                columns: table => new
                {
                    IdNote = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TextNote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    IdCovenantResult = table.Column<int>(type: "int", nullable: false),
                    CovenantResultIdResult = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultNotes", x => x.IdNote);
                    table.ForeignKey(
                        name: "FK_ResultNotes_CovenantResults_CovenantResultIdResult",
                        column: x => x.CovenantResultIdResult,
                        principalTable: "CovenantResults",
                        principalColumn: "IdResult");
                });

            migrationBuilder.CreateTable(
                name: "CovenantConditions",
                columns: table => new
                {
                    IdCondition = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDateCondition = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateCondition = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LowerLimitCondition = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpperLimitCondition = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContractualFlagCondition = table.Column<bool>(type: "bit", nullable: false),
                    ExceptionFlagCondition = table.Column<bool>(type: "bit", nullable: false),
                    BreachWeight = table.Column<int>(type: "int", nullable: false),
                    IdCovenant = table.Column<int>(type: "int", nullable: false),
                    ResultIdResult = table.Column<int>(type: "int", nullable: true),
                    FinancialDataIdFinancialData = table.Column<int>(type: "int", nullable: true),
                    CovenantIdCovenant = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CovenantConditions", x => x.IdCondition);
                    table.ForeignKey(
                        name: "FK_CovenantConditions_CovenantResults_ResultIdResult",
                        column: x => x.ResultIdResult,
                        principalTable: "CovenantResults",
                        principalColumn: "IdResult");
                    table.ForeignKey(
                        name: "FK_CovenantConditions_Covenants_CovenantIdCovenant",
                        column: x => x.CovenantIdCovenant,
                        principalTable: "Covenants",
                        principalColumn: "IdCovenant");
                    table.ForeignKey(
                        name: "FK_CovenantConditions_FinancialData_FinancialDataIdFinancialData",
                        column: x => x.FinancialDataIdFinancialData,
                        principalTable: "FinancialData",
                        principalColumn: "IdFinancialData");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CovenantConditions_CovenantIdCovenant",
                table: "CovenantConditions",
                column: "CovenantIdCovenant");

            migrationBuilder.CreateIndex(
                name: "IX_CovenantConditions_FinancialDataIdFinancialData",
                table: "CovenantConditions",
                column: "FinancialDataIdFinancialData");

            migrationBuilder.CreateIndex(
                name: "IX_CovenantConditions_ResultIdResult",
                table: "CovenantConditions",
                column: "ResultIdResult");

            migrationBuilder.CreateIndex(
                name: "IX_ResultNotes_CovenantResultIdResult",
                table: "ResultNotes",
                column: "CovenantResultIdResult");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CovenantConditions");

            migrationBuilder.DropTable(
                name: "ResultNotes");

            migrationBuilder.DropTable(
                name: "Covenants");

            migrationBuilder.DropTable(
                name: "FinancialData");

            migrationBuilder.DropTable(
                name: "CovenantResults");
        }
    }
}
