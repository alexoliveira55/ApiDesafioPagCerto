using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class SegundaEtapaAjustes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusAnticipationRequest",
                table: "AnticipationRequest");

            migrationBuilder.AddColumn<int>(
                name: "ResultAnalizeTransaction",
                table: "TransactionAnticipationRequest",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusAnticipation",
                table: "TransactionAnticipationRequest",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpectedDateReceivement",
                table: "InstallmentTransaction",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ResultAnticipationRequest",
                table: "AnticipationRequest",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "AnticipatedValue",
                table: "AnticipationRequest",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultAnalizeTransaction",
                table: "TransactionAnticipationRequest");

            migrationBuilder.DropColumn(
                name: "StatusAnticipation",
                table: "TransactionAnticipationRequest");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpectedDateReceivement",
                table: "InstallmentTransaction",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<int>(
                name: "ResultAnticipationRequest",
                table: "AnticipationRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AnticipatedValue",
                table: "AnticipationRequest",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusAnticipationRequest",
                table: "AnticipationRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
