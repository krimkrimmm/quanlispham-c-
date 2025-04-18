﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TMDT.Data;

#nullable disable

namespace TMDT.Migrations
{
    [DbContext(typeof(TMDTDbContext))]
    [Migration("20250409102102_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CartPaymentInformationViewModel", b =>
                {
                    b.Property<int>("CartItemsId")
                        .HasColumnType("int");

                    b.Property<int>("PaymentInformationViewModelId")
                        .HasColumnType("int");

                    b.HasKey("CartItemsId", "PaymentInformationViewModelId");

                    b.HasIndex("PaymentInformationViewModelId");

                    b.ToTable("CartPaymentInformationViewModel");
                });

            modelBuilder.Entity("DonhangProductOrders", b =>
                {
                    b.Property<int>("DonhangId")
                        .HasColumnType("int");

                    b.Property<int>("ProductOrdersId")
                        .HasColumnType("int");

                    b.HasKey("DonhangId", "ProductOrdersId");

                    b.HasIndex("ProductOrdersId");

                    b.ToTable("DonhangProductOrders");
                });

            modelBuilder.Entity("PaymentResultProductOrders", b =>
                {
                    b.Property<int>("PaymentResultId")
                        .HasColumnType("int");

                    b.Property<int>("ProductOrdersId")
                        .HasColumnType("int");

                    b.HasKey("PaymentResultId", "ProductOrdersId");

                    b.HasIndex("ProductOrdersId");

                    b.ToTable("PaymentResultProductOrders");
                });

            modelBuilder.Entity("TMDT.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int?>("PaymentInformationId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TimeOrder")
                        .HasColumnType("datetime2");

                    b.Property<float>("TotalPrice")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("PaymentInformationId");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("TMDT.Models.Categories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TMDT.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("TMDT.Models.CustomerBehaviors", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FavoriteCategories")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastPurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("TotalPurchases")
                        .HasColumnType("real");

                    b.Property<float>("TotalSpent")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("CustomerBehaviors");
                });

            modelBuilder.Entity("TMDT.Models.Districts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("TMDT.Models.Donhang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Createtime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Diachi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSuccess")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductsId")
                        .HasColumnType("int");

                    b.Property<string>("QRcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sdt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tennguoinhan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Trangthaidonhang")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransactionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ProductsId");

                    b.ToTable("Donhang");
                });

            modelBuilder.Entity("TMDT.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("TMDT.Models.PaymentInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BillingAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CVV")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreditCardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DiscountApplied")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpiryDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VoucherCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("decrease")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("PaymentInformation");
                });

            modelBuilder.Entity("TMDT.Models.PaymentInformationViewModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("OrderNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QRCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("TotalPrice")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("PaymentInformationViewModel");
                });

            modelBuilder.Entity("TMDT.Models.PaymentResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Createtime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Diachi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSuccess")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductsId")
                        .HasColumnType("int");

                    b.Property<string>("QRcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sdt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tennguoinhan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Trangthaidonhang")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransactionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ProductsId");

                    b.ToTable("PaymentResult");
                });

            modelBuilder.Entity("TMDT.Models.ProductOrders", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("decrease")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("ProductOrders");
                });

            modelBuilder.Entity("TMDT.Models.ProductReviews", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductOrdersId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductsId")
                        .HasColumnType("int");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductOrdersId");

                    b.HasIndex("ProductsId");

                    b.ToTable("ProductReviews");
                });

            modelBuilder.Entity("TMDT.Models.Products", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("decrease")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("CategoriesId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("TMDT.Models.Voucher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<int?>("PaymentInformationViewModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PaymentInformationViewModelId");

                    b.ToTable("Voucher");
                });

            modelBuilder.Entity("TMDT.Models.Whitelist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TimeOrder")
                        .HasColumnType("datetime2");

                    b.Property<float>("TotalPrice")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Whilelist");
                });

            modelBuilder.Entity("CartPaymentInformationViewModel", b =>
                {
                    b.HasOne("TMDT.Models.Cart", null)
                        .WithMany()
                        .HasForeignKey("CartItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TMDT.Models.PaymentInformationViewModel", null)
                        .WithMany()
                        .HasForeignKey("PaymentInformationViewModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DonhangProductOrders", b =>
                {
                    b.HasOne("TMDT.Models.Donhang", null)
                        .WithMany()
                        .HasForeignKey("DonhangId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TMDT.Models.ProductOrders", null)
                        .WithMany()
                        .HasForeignKey("ProductOrdersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PaymentResultProductOrders", b =>
                {
                    b.HasOne("TMDT.Models.PaymentResult", null)
                        .WithMany()
                        .HasForeignKey("PaymentResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TMDT.Models.ProductOrders", null)
                        .WithMany()
                        .HasForeignKey("ProductOrdersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TMDT.Models.Cart", b =>
                {
                    b.HasOne("TMDT.Models.PaymentInformation", null)
                        .WithMany("carts")
                        .HasForeignKey("PaymentInformationId");
                });

            modelBuilder.Entity("TMDT.Models.Districts", b =>
                {
                    b.HasOne("TMDT.Models.City", null)
                        .WithMany("Districts")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TMDT.Models.Donhang", b =>
                {
                    b.HasOne("TMDT.Models.Products", null)
                        .WithMany("Donhang")
                        .HasForeignKey("ProductsId");
                });

            modelBuilder.Entity("TMDT.Models.PaymentResult", b =>
                {
                    b.HasOne("TMDT.Models.Products", null)
                        .WithMany("PaymentResult")
                        .HasForeignKey("ProductsId");
                });

            modelBuilder.Entity("TMDT.Models.ProductReviews", b =>
                {
                    b.HasOne("TMDT.Models.ProductOrders", null)
                        .WithMany("ProductReviews")
                        .HasForeignKey("ProductOrdersId");

                    b.HasOne("TMDT.Models.Products", null)
                        .WithMany("ProductReviews")
                        .HasForeignKey("ProductsId");
                });

            modelBuilder.Entity("TMDT.Models.Products", b =>
                {
                    b.HasOne("TMDT.Models.Categories", null)
                        .WithMany("Products")
                        .HasForeignKey("CategoriesId");
                });

            modelBuilder.Entity("TMDT.Models.Voucher", b =>
                {
                    b.HasOne("TMDT.Models.PaymentInformationViewModel", null)
                        .WithMany("Vouchers")
                        .HasForeignKey("PaymentInformationViewModelId");
                });

            modelBuilder.Entity("TMDT.Models.Categories", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("TMDT.Models.City", b =>
                {
                    b.Navigation("Districts");
                });

            modelBuilder.Entity("TMDT.Models.PaymentInformation", b =>
                {
                    b.Navigation("carts");
                });

            modelBuilder.Entity("TMDT.Models.PaymentInformationViewModel", b =>
                {
                    b.Navigation("Vouchers");
                });

            modelBuilder.Entity("TMDT.Models.ProductOrders", b =>
                {
                    b.Navigation("ProductReviews");
                });

            modelBuilder.Entity("TMDT.Models.Products", b =>
                {
                    b.Navigation("Donhang");

                    b.Navigation("PaymentResult");

                    b.Navigation("ProductReviews");
                });
#pragma warning restore 612, 618
        }
    }
}
