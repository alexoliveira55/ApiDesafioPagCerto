using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class PrimeiraEtapa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransactionCard",
                columns: table => new
                {
                    Nsu = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    ApprovalDate = table.Column<DateTime>(nullable: true),
                    FailureDate = table.Column<DateTime>(nullable: true),
                    Anticipated = table.Column<bool>(nullable: false),
                    ConfirmationAcquirer = table.Column<bool>(nullable: false),
                    ValueGross = table.Column<decimal>(nullable: false),
                    ValueLiquid = table.Column<decimal>(nullable: false),
                    RateTransaction = table.Column<decimal>(nullable: false),
                    NumberOfInstallments = table.Column<int>(nullable: false),
                    LastNumberCard = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionCard", x => x.Nsu);
                });

            migrationBuilder.CreateTable(
                name: "InstallmentTransaction",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NsuTransaction = table.Column<long>(nullable: false),
                    ValueGross = table.Column<decimal>(nullable: false),
                    ValueLiquid = table.Column<decimal>(nullable: false),
                    InstallmentNumber = table.Column<int>(nullable: false),
                    AnticipatedValue = table.Column<decimal>(nullable: true),
                    ExpectedDateReceivement = table.Column<DateTime>(nullable: true),
                    DateAdvancedPayment = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallmentTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstallmentTransaction_TransactionCard_NsuTransaction",
                        column: x => x.NsuTransaction,
                        principalTable: "TransactionCard",
                        principalColumn: "Nsu",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentTransaction_NsuTransaction",
                table: "InstallmentTransaction",
                column: "NsuTransaction");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstallmentTransaction");

            migrationBuilder.DropTable(
                name: "TransactionCard");
        }
    }
}
