using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastucture.Migrations
{
    public partial class _8mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CounterpartyIdCounterparty",
                table: "Covenants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdCounterparty",
                table: "Covenants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Counterparties",
                columns: table => new
                {
                    IdCounterparty = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counterparties", x => x.IdCounterparty);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Covenants_CounterpartyIdCounterparty",
                table: "Covenants",
                column: "CounterpartyIdCounterparty");

            migrationBuilder.AddForeignKey(
                name: "FK_Covenants_Counterparties_CounterpartyIdCounterparty",
                table: "Covenants",
                column: "CounterpartyIdCounterparty",
                principalTable: "Counterparties",
                principalColumn: "IdCounterparty");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Covenants_Counterparties_CounterpartyIdCounterparty",
                table: "Covenants");

            migrationBuilder.DropTable(
                name: "Counterparties");

            migrationBuilder.DropIndex(
                name: "IX_Covenants_CounterpartyIdCounterparty",
                table: "Covenants");

            migrationBuilder.DropColumn(
                name: "CounterpartyIdCounterparty",
                table: "Covenants");

            migrationBuilder.DropColumn(
                name: "IdCounterparty",
                table: "Covenants");
        }
    }
}
