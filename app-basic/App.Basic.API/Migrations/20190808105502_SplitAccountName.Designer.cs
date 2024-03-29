﻿// <auto-generated />
using App.Basic.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace App.Basic.API.Migrations
{
    [DbContext(typeof(BasicAppContext))]
    [Migration("20190808105502_SplitAccountName")]
    partial class SplitAccountName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("basicapp")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("App.Basic.Domain.AggregateModels.PermissionAggregate.AccessPoint", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("ApplyOranizationTypeIds")
                        .HasColumnName("apply_oranization_type_ids");

                    b.Property<string>("Description")
                        .HasColumnName("description");

                    b.Property<int>("IsInner")
                        .HasColumnName("is_inner");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<string>("PointKey")
                        .HasColumnName("point_key");

                    b.HasKey("Id")
                        .HasName("pk_access_point");

                    b.ToTable("access_point");
                });

            modelBuilder.Entity("App.Basic.Domain.AggregateModels.PermissionAggregate.CustomRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("AccessPointKeys")
                        .HasColumnName("access_point_keys");

                    b.Property<string>("Description")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<string>("OrganizationId")
                        .HasColumnName("organization_id");

                    b.HasKey("Id")
                        .HasName("pk_custom_role");

                    b.ToTable("custom_role");
                });

            modelBuilder.Entity("App.Basic.Domain.AggregateModels.UserAggregate.Account", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("Active")
                        .HasColumnName("active");

                    b.Property<long>("CreatedTime")
                        .HasColumnName("created_time");

                    b.Property<string>("Creator")
                        .HasColumnName("creator");

                    b.Property<string>("Description")
                        .HasColumnName("description");

                    b.Property<string>("FistName")
                        .HasColumnName("fist_name");

                    b.Property<int>("LanguageTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("language_type_id")
                        .HasDefaultValue(0);

                    b.Property<string>("LastName")
                        .HasColumnName("last_name");

                    b.Property<int>("LegalPerson")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("legal_person")
                        .HasDefaultValue(0);

                    b.Property<string>("Mail")
                        .HasColumnName("mail");

                    b.Property<long>("ModifiedTime")
                        .HasColumnName("modified_time");

                    b.Property<string>("Modifier")
                        .HasColumnName("modifier");

                    b.Property<string>("OrganizationId")
                        .HasColumnName("organization_id");

                    b.Property<string>("Password")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .HasColumnName("phone");

                    b.Property<int>("SystemRoleId")
                        .HasColumnName("system_role_id");

                    b.HasKey("Id")
                        .HasName("pk_account");

                    b.HasIndex("FistName")
                        .HasName("ix_account_fist_name");

                    b.HasIndex("LegalPerson")
                        .HasName("ix_account_legal_person");

                    b.HasIndex("Mail")
                        .HasName("ix_account_mail");

                    b.HasIndex("OrganizationId")
                        .HasName("ix_account_organization_id");

                    b.HasIndex("SystemRoleId")
                        .HasName("ix_account_system_role_id");

                    b.ToTable("account");
                });

            modelBuilder.Entity("App.Basic.Domain.AggregateModels.UserAggregate.Organization", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("Active")
                        .HasColumnName("active");

                    b.Property<long>("CreatedTime")
                        .HasColumnName("created_time");

                    b.Property<string>("Creator")
                        .HasColumnName("creator");

                    b.Property<string>("Description")
                        .HasColumnName("description");

                    b.Property<string>("Fingerprint")
                        .HasColumnName("fingerprint");

                    b.Property<int>("LValue")
                        .HasColumnName("lvalue");

                    b.Property<string>("Mail")
                        .HasColumnName("mail");

                    b.Property<long>("ModifiedTime")
                        .HasColumnName("modified_time");

                    b.Property<string>("Modifier")
                        .HasColumnName("modifier");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<int>("OrganizationTypeId")
                        .HasColumnName("organization_type_id");

                    b.Property<string>("OwnerId")
                        .HasColumnName("owner_id");

                    b.Property<string>("ParentId")
                        .HasColumnName("parent_id");

                    b.Property<string>("Phone")
                        .HasColumnName("phone");

                    b.Property<int>("RValue")
                        .HasColumnName("rvalue");

                    b.HasKey("Id")
                        .HasName("pk_organization");

                    b.ToTable("organization");
                });

            modelBuilder.Entity("App.Basic.Domain.AggregateModels.UserAggregate.UserRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("AccountId")
                        .HasColumnName("account_id");

                    b.Property<string>("CustomRoleId")
                        .HasColumnName("custom_role_id");

                    b.HasKey("Id")
                        .HasName("pk_user_role");

                    b.HasIndex("AccountId")
                        .HasName("ix_user_role_account_id");

                    b.ToTable("user_role");
                });

            modelBuilder.Entity("App.Basic.Domain.AggregateModels.UserAggregate.Account", b =>
                {
                    b.HasOne("App.Basic.Domain.AggregateModels.UserAggregate.Organization", "Organization")
                        .WithMany("OwnAccounts")
                        .HasForeignKey("OrganizationId")
                        .HasConstraintName("fk_account_organization_organization_id");
                });

            modelBuilder.Entity("App.Basic.Domain.AggregateModels.UserAggregate.UserRole", b =>
                {
                    b.HasOne("App.Basic.Domain.AggregateModels.UserAggregate.Account", "Account")
                        .WithMany("OwnRoles")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("fk_user_role_account_account_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
