﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Infrastructure.Context;

namespace api.Migrations
{
    [DbContext(typeof(DbContextApi))]
    [Migration("20220516174018_PrimeiraEtapa")]
    partial class PrimeiraEtapa
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("api.Models.EntityModel.InstallmentTransaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("AnticipatedValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("DateAdvancedPayment")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ExpectedDateReceivement")
                        .HasColumnType("datetime2");

                    b.Property<int>("InstallmentNumber")
                        .HasColumnType("int");

                    b.Property<long>("NsuTransaction")
                        .HasColumnType("bigint");

                    b.Property<decimal>("ValueGross")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValueLiquid")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("NsuTransaction");

                    b.ToTable("InstallmentTransaction");
                });

            modelBuilder.Entity("api.Models.EntityModel.TransactionCard", b =>
                {
                    b.Property<long>("Nsu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Anticipated")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ApprovalDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("ConfirmationAcquirer")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("FailureDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastNumberCard")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfInstallments")
                        .HasColumnType("int");

                    b.Property<decimal>("RateTransaction")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("ValueGross")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValueLiquid")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Nsu");

                    b.ToTable("TransactionCard");
                });

            modelBuilder.Entity("api.Models.EntityModel.InstallmentTransaction", b =>
                {
                    b.HasOne("api.Models.EntityModel.TransactionCard", "TransactionCard")
                        .WithMany("InstallmentTransactions")
                        .HasForeignKey("NsuTransaction")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}