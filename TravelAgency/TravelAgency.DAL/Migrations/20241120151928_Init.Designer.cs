﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TravelAgency.DAL;

namespace TravelAgency.DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241120151928_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("TravelAgency.Domain.Models.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PathImg")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("TravelAgency.Domain.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("BuyerId")
                        .HasColumnType("integer");

                    b.Property<Guid?>("BuyerId1")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId1");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("TravelAgency.Domain.Models.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<Guid?>("BookId1")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Cost")
                        .HasColumnType("numeric");

                    b.Property<double>("Count")
                        .HasColumnType("double precision");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<Guid?>("OrderId1")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BookId1");

                    b.HasIndex("OrderId1");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("TravelAgency.Domain.Models.OrderTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<decimal>("Cost")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<Guid?>("OrderId1")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OrderId1");

                    b.ToTable("OrderTransactions");
                });

            modelBuilder.Entity("TravelAgency.Domain.Models.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("TravelAgency.Domain.Models.StorageTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<Guid?>("BookId1")
                        .HasColumnType("uuid");

                    b.Property<double>("Count")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("ShopId")
                        .HasColumnType("integer");

                    b.Property<int>("TransactionType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BookId1");

                    b.HasIndex("ShopId");

                    b.ToTable("StorageTransactions");
                });

            modelBuilder.Entity("TravelAgency.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("PathImg")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Buyers");
                });

            modelBuilder.Entity("TravelAgency.Domain.Models.Order", b =>
                {
                    b.HasOne("TravelAgency.Domain.Models.User", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerId1");

                    b.Navigation("Buyer");
                });

            modelBuilder.Entity("TravelAgency.Domain.Models.OrderItem", b =>
                {
                    b.HasOne("TravelAgency.Domain.Models.Book", "Book")
                        .WithMany("Items")
                        .HasForeignKey("BookId1");

                    b.HasOne("TravelAgency.Domain.Models.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId1");

                    b.Navigation("Book");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("TravelAgency.Domain.Models.OrderTransaction", b =>
                {
                    b.HasOne("TravelAgency.Domain.Models.Order", "Order")
                        .WithMany("OrderTransaction")
                        .HasForeignKey("OrderId1");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("TravelAgency.Domain.Models.StorageTransaction", b =>
                {
                    b.HasOne("TravelAgency.Domain.Models.Book", "Book")
                        .WithMany("Transactions")
                        .HasForeignKey("BookId1");

                    b.HasOne("TravelAgency.Domain.Models.Shop", null)
                        .WithMany("Transactions")
                        .HasForeignKey("ShopId");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("TravelAgency.Domain.Models.Book", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("TravelAgency.Domain.Models.Order", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("OrderTransaction");
                });

            modelBuilder.Entity("TravelAgency.Domain.Models.Shop", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
