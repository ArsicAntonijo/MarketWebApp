﻿// <auto-generated />
using System;
using DataLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataLayer.Migrations
{
    [DbContext(typeof(MarketApiContext))]
    [Migration("20230507083938_CustomerAndOrderTablesUpdated")]
    partial class CustomerAndOrderTablesUpdated
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DataLayer.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("DataLayer.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemId"), 1L, 1);

                    b.Property<decimal>("AmauntAvailable")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("MeasurementUnit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PricePerUnit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PriceWithTax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ReceiptId")
                        .HasColumnType("int");

                    b.Property<decimal>("Tax")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ItemId");

                    b.HasIndex("ReceiptId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("DataLayer.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<string>("Confirmed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("DataLayer.Models.OrderedItem", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amaunt")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("OrderedItem");
                });

            modelBuilder.Entity("DataLayer.Models.Receipt", b =>
                {
                    b.Property<int>("ReceiptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReceiptId"), 1L, 1);

                    b.Property<string>("DateOfReceipt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DistributerFirm")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DistributerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StringItems")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReceiptId");

                    b.ToTable("Receipt");
                });

            modelBuilder.Entity("DataLayer.Models.Item", b =>
                {
                    b.HasOne("DataLayer.Models.Receipt", null)
                        .WithMany("Items")
                        .HasForeignKey("ReceiptId");
                });

            modelBuilder.Entity("DataLayer.Models.Order", b =>
                {
                    b.HasOne("DataLayer.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("DataLayer.Models.OrderedItem", b =>
                {
                    b.HasOne("DataLayer.Models.Item", "Item")
                        .WithMany("OrderedItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Models.Order", "Order")
                        .WithMany("OrderedItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("DataLayer.Models.Item", b =>
                {
                    b.Navigation("OrderedItems");
                });

            modelBuilder.Entity("DataLayer.Models.Order", b =>
                {
                    b.Navigation("OrderedItems");
                });

            modelBuilder.Entity("DataLayer.Models.Receipt", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
