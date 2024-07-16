﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OrderService.DataAccess;

#nullable disable

namespace OrderService.DataAccess.Migrations
{
    [DbContext(typeof(OrderServiceDbContext))]
    [Migration("20240711184910_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OrderService.DataAccess.Entities.CanceledOrderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Basket")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<DateTime>("CanceledDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Cheque")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime?>("CookingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeSpan>("CookingTime")
                        .HasColumnType("interval");

                    b.Property<Guid>("CourierId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeliveryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeSpan>("DeliveryTime")
                        .HasColumnType("interval");

                    b.Property<int>("LastStatus")
                        .HasColumnType("integer");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<string>("ReasonOfCanceled")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uuid");

                    b.Property<int>("WhoCanceled")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("CourierId");

                    b.HasIndex("StoreId");

                    b.ToTable("CanceledOrders");
                });

            modelBuilder.Entity("OrderService.DataAccess.Entities.CurrentOrderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Basket")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<string>("Cheque")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("ClientAddress")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<string>("ClientNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime?>("CookingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeSpan>("CookingTime")
                        .HasColumnType("interval");

                    b.Property<Guid>("CourierId")
                        .HasColumnType("uuid");

                    b.Property<string>("CourierNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<DateTime?>("DeliveryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeSpan>("DeliveryTime")
                        .HasColumnType("interval");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("StoreAddress")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("CourierId");

                    b.HasIndex("StoreId");

                    b.ToTable("CurrentOrders");
                });

            modelBuilder.Entity("OrderService.DataAccess.Entities.LastOrderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Basket")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<string>("Cheque")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime?>("CookingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeSpan>("CookingTime")
                        .HasColumnType("interval");

                    b.Property<Guid>("CourierId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeliveryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeSpan>("DeliveryTime")
                        .HasColumnType("interval");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("CourierId");

                    b.HasIndex("StoreId");

                    b.ToTable("LastOrders");
                });
#pragma warning restore 612, 618
        }
    }
}