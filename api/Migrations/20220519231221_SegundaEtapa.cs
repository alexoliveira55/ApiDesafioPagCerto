using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class SegundaEtapa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastNumberCard",
                table: "TransactionCard");

            migrationBuilder.AddColumn<int>(
                name: "LastNumberCar",
                table: "TransactionCard",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AnticipationRequest",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestDate = table.Column<DateTime>(nullable: false),
                    DateStartAnalyze = table.Column<DateTime>(nullable: true),
                    DateEndAnalyze = table.Column<DateTime>(nullable: true),
                    StatusAnticipationRequest = table.Column<int>(nullable: false),
                    ResultAnticipationRequest = table.Column<int>(nullable: false),
                    ValueAnticipationRequest = table.Column<decimal>(nullable: false),
                    AnticipatedValue = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnticipationRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionAnticipationRequest",
                columns: table => new
                {
                    IdAnticipationRequest = table.Column<long>(nullable: false),
                    NsuTransction = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionAnticipationRequest", x => new { x.NsuTransction, x.IdAnticipationRequest });
                    table.ForeignKey(
                        name: "FK_TransactionAnticipationRequest_AnticipationRequest_IdAnticipationRequest",
                        column: x => x.IdAnticipationRequest,
                        principalTable: "AnticipationRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionAnticipationRequest_TransactionCard_NsuTransction",
                        column: x => x.NsuTransction,
                        principalTable: "TransactionCard",
                        principalColumn: "Nsu",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionAnticipationRequest_IdAnticipationRequest",
                table: "TransactionAnticipationRequest",
                column: "IdAnticipationRequest");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionAnticipationRequest");

            migrationBuilder.DropTable(
                name: "AnticipationRequest");

            migrationBuilder.DropColumn(
                name: "LastNumberCar",
                table: "TransactionCard");

            migrationBuilder.AddColumn<int>(
                name: "LastNumberCard",
                table: "TransactionCard",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
