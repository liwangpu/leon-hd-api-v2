﻿// <auto-generated />
using System;
using App.OMS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace App.OMS.API.Migrations
{
    [DbContext(typeof(OMSAppContext))]
    [Migration("20190627100726_orderItemAddFields")]
    partial class orderItemAddFields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("omsapp")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("App.OMS.Domain.AggregateModels.CustomerAggregate.Customer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasColumnName("address");

                    b.Property<string>("Company")
                        .HasColumnName("company");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnName("created_time");

                    b.Property<string>("Creator")
                        .HasColumnName("creator");

                    b.Property<string>("Description")
                        .HasColumnName("description");

                    b.Property<string>("Mail")
                        .HasColumnName("mail");

                    b.Property<DateTime>("ModifiedTime")
                        .HasColumnName("modified_time");

                    b.Property<string>("Modifier")
                        .HasColumnName("modifier");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<string>("OrganizationId")
                        .HasColumnName("organization_id");

                    b.Property<string>("Phone")
                        .HasColumnName("phone");

                    b.HasKey("Id")
                        .HasName("pk_customer");

                    b.ToTable("customer");
                });

            modelBuilder.Entity("App.OMS.Domain.AggregateModels.OrderAggregate.Order", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("ContactMail")
                        .HasColumnName("contact_mail");

                    b.Property<string>("ContactName")
                        .HasColumnName("contact_name");

                    b.Property<string>("ContactPhone")
                        .HasColumnName("contact_phone");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnName("created_time");

                    b.Property<string>("Creator")
                        .HasColumnName("creator");

                    b.Property<string>("CustomerId")
                        .HasColumnName("customer_id");

                    b.Property<string>("Description")
                        .HasColumnName("description");

                    b.Property<DateTime>("ModifiedTime")
                        .HasColumnName("modified_time");

                    b.Property<string>("Modifier")
                        .HasColumnName("modifier");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<string>("OrderNo")
                        .HasColumnName("order_no");

                    b.Property<string>("OrganizationId")
                        .HasColumnName("organization_id");

                    b.Property<string>("ShippingAddress")
                        .HasColumnName("shipping_address");

                    b.Property<int>("TotalNum")
                        .HasColumnName("total_num");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnName("total_price");

                    b.HasKey("Id")
                        .HasName("pk_order");

                    b.HasIndex("CustomerId")
                        .HasName("ix_order_customer_id");

                    b.ToTable("order");
                });

            modelBuilder.Entity("App.OMS.Domain.AggregateModels.OrderAggregate.OrderItem", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("Num")
                        .HasColumnName("num");

                    b.Property<string>("OrderId")
                        .HasColumnName("order_id");

                    b.Property<string>("ProductBrand")
                        .HasColumnName("product_brand");

                    b.Property<string>("ProductDescription")
                        .HasColumnName("product_description");

                    b.Property<string>("ProductIcon")
                        .HasColumnName("product_icon");

                    b.Property<string>("ProductId")
                        .HasColumnName("product_id");

                    b.Property<string>("ProductName")
                        .HasColumnName("product_name");

                    b.Property<string>("ProductSpecDescription")
                        .HasColumnName("product_spec_description");

                    b.Property<string>("ProductSpecIcon")
                        .HasColumnName("product_spec_icon");

                    b.Property<string>("ProductSpecId")
                        .HasColumnName("product_spec_id");

                    b.Property<string>("ProductSpecName")
                        .HasColumnName("product_spec_name");

                    b.Property<string>("ProductUnit")
                        .HasColumnName("product_unit");

                    b.Property<string>("Remark")
                        .HasColumnName("remark");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnName("unit_price");

                    b.HasKey("Id")
                        .HasName("pk_order_item");

                    b.HasIndex("OrderId")
                        .HasName("ix_order_item_order_id");

                    b.ToTable("order_item");
                });

            modelBuilder.Entity("App.OMS.Domain.AggregateModels.OrderAggregate.Order", b =>
                {
                    b.HasOne("App.OMS.Domain.AggregateModels.CustomerAggregate.Customer", "Customer")
                        .WithMany("OwnOrders")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("fk_order_customer_customer_id");
                });

            modelBuilder.Entity("App.OMS.Domain.AggregateModels.OrderAggregate.OrderItem", b =>
                {
                    b.HasOne("App.OMS.Domain.AggregateModels.OrderAggregate.Order", "Order")
                        .WithMany("OwnOrderItems")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("fk_order_item_order_order_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
