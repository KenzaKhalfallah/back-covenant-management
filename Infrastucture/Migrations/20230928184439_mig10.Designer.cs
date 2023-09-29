﻿// <auto-generated />
using System;
using Infrastucture;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastucture.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20230928184439_mig10")]
    partial class mig10
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.Account", b =>
                {
                    b.Property<int>("IdAccount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAccount"), 1L, 1);

                    b.Property<bool>("ConditionsAccepted")
                        .HasColumnType("bit");

                    b.Property<string>("ConfirmPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdAccount");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Domain.Entities.Counterparty", b =>
                {
                    b.Property<int>("IdCounterparty")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCounterparty"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCounterparty");

                    b.ToTable("Counterparties");
                });

            modelBuilder.Entity("Domain.Entities.Covenant", b =>
                {
                    b.Property<int>("IdCovenant")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCovenant"), 1L, 1);

                    b.Property<int>("CategoryCovenant")
                        .HasColumnType("int");

                    b.Property<int?>("CounterpartyIdCounterparty")
                        .HasColumnType("int");

                    b.Property<string>("DescriptionCovenant")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdCounterparty")
                        .HasColumnType("int");

                    b.Property<int?>("LinkedLineItem")
                        .HasColumnType("int");

                    b.Property<string>("NameCovenant")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PeriodTypeCovenant")
                        .HasColumnType("int");

                    b.Property<int>("StatementSourceCovenant")
                        .HasColumnType("int");

                    b.Property<int>("TypeCovenant")
                        .HasColumnType("int");

                    b.HasKey("IdCovenant");

                    b.HasIndex("CounterpartyIdCounterparty");

                    b.ToTable("Covenants");
                });

            modelBuilder.Entity("Domain.Entities.CovenantCondition", b =>
                {
                    b.Property<int>("IdCondition")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCondition"), 1L, 1);

                    b.Property<int>("BreachWeight")
                        .HasColumnType("int");

                    b.Property<bool>("ContractualFlagCondition")
                        .HasColumnType("bit");

                    b.Property<int?>("CovenantIdCovenant")
                        .HasColumnType("int");

                    b.Property<int?>("CovenantResultIdResult")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDateCondition")
                        .HasColumnType("datetime2");

                    b.Property<bool>("ExceptionFlagCondition")
                        .HasColumnType("bit");

                    b.Property<int?>("FinancialDataIdFinancialData")
                        .HasColumnType("int");

                    b.Property<int>("IdCovenant")
                        .HasColumnType("int");

                    b.Property<decimal>("LowerLimitCondition")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDateCondition")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("UpperLimitCondition")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdCondition");

                    b.HasIndex("CovenantIdCovenant");

                    b.HasIndex("CovenantResultIdResult");

                    b.HasIndex("FinancialDataIdFinancialData");

                    b.ToTable("CovenantConditions");
                });

            modelBuilder.Entity("Domain.Entities.CovenantResult", b =>
                {
                    b.Property<int>("IdResult")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdResult"), 1L, 1);

                    b.Property<int>("IdCondition")
                        .HasColumnType("int");

                    b.Property<DateTime>("ResultDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ResultStatus")
                        .HasColumnType("int");

                    b.HasKey("IdResult");

                    b.ToTable("CovenantResults");
                });

            modelBuilder.Entity("Domain.Entities.FinancialData", b =>
                {
                    b.Property<int>("IdFinancialData")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdFinancialData"), 1L, 1);

                    b.Property<decimal?>("Amortization")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("AverageInventory")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("CostOfGoodsSold")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("CurrentAssets")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("CurrentLiabilities")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Depreciation")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("GrossProfit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IdCondition")
                        .HasColumnType("int");

                    b.Property<decimal?>("Interest")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("InterestExpense")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("NetIncome")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("OperatingExpenses")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Taxes")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("TotalAssets")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("TotalDebt")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("TotalDebtService")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("TotalEquity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("TotalRevenues")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdFinancialData");

                    b.ToTable("FinancialDatas");
                });

            modelBuilder.Entity("Domain.Entities.ResultNote", b =>
                {
                    b.Property<int>("IdNote")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdNote"), 1L, 1);

                    b.Property<int?>("CovenantResultIdResult")
                        .HasColumnType("int");

                    b.Property<int>("IdCovenantResult")
                        .HasColumnType("int");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<string>("TextNote")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdNote");

                    b.HasIndex("CovenantResultIdResult");

                    b.ToTable("ResultNotes");
                });

            modelBuilder.Entity("Domain.Entities.Covenant", b =>
                {
                    b.HasOne("Domain.Entities.Counterparty", null)
                        .WithMany("Covenants")
                        .HasForeignKey("CounterpartyIdCounterparty");
                });

            modelBuilder.Entity("Domain.Entities.CovenantCondition", b =>
                {
                    b.HasOne("Domain.Entities.Covenant", null)
                        .WithMany("CovenantConditions")
                        .HasForeignKey("CovenantIdCovenant");

                    b.HasOne("Domain.Entities.CovenantResult", "CovenantResult")
                        .WithMany()
                        .HasForeignKey("CovenantResultIdResult");

                    b.HasOne("Domain.Entities.FinancialData", "FinancialData")
                        .WithMany()
                        .HasForeignKey("FinancialDataIdFinancialData");

                    b.Navigation("CovenantResult");

                    b.Navigation("FinancialData");
                });

            modelBuilder.Entity("Domain.Entities.ResultNote", b =>
                {
                    b.HasOne("Domain.Entities.CovenantResult", null)
                        .WithMany("ResultNotes")
                        .HasForeignKey("CovenantResultIdResult");
                });

            modelBuilder.Entity("Domain.Entities.Counterparty", b =>
                {
                    b.Navigation("Covenants");
                });

            modelBuilder.Entity("Domain.Entities.Covenant", b =>
                {
                    b.Navigation("CovenantConditions");
                });

            modelBuilder.Entity("Domain.Entities.CovenantResult", b =>
                {
                    b.Navigation("ResultNotes");
                });
#pragma warning restore 612, 618
        }
    }
}
