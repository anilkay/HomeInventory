﻿// <auto-generated />
using HomeInventory.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HomeInventory.Migrations
{
    [DbContext(typeof(HomeInventoryDbContext))]
    [Migration("20241130213051_Owner Added")]
    partial class OwnerAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-rc.2.24474.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HomeInventory.Data.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(750)
                        .HasColumnType("character varying(750)");

                    b.Property<int?>("LocationId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<int?>("PossibleValueId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PossibleValueId");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("HomeInventory.Data.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("LocationType")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Location");

                    b.HasDiscriminator<string>("LocationType").HasValue("Location");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("HomeInventory.Data.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("HomeInventory.Data.PossibleValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("PossibleValueType")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

                    b.HasKey("Id");

                    b.ToTable("PossibleValue");

                    b.HasDiscriminator<string>("PossibleValueType").HasValue("PossibleValue");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("HomeInventory.Data.AdressLocation", b =>
                {
                    b.HasBaseType("HomeInventory.Data.Location");

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.HasDiscriminator().HasValue("Address");
                });

            modelBuilder.Entity("HomeInventory.Data.CoordinateLocation", b =>
                {
                    b.HasBaseType("HomeInventory.Data.Location");

                    b.Property<double>("X")
                        .HasColumnType("double precision");

                    b.Property<double>("Y")
                        .HasColumnType("double precision");

                    b.HasDiscriminator().HasValue("Coordinate");
                });

            modelBuilder.Entity("HomeInventory.Data.MonetaryValue", b =>
                {
                    b.HasBaseType("HomeInventory.Data.PossibleValue");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasDiscriminator().HasValue("Monetary");
                });

            modelBuilder.Entity("HomeInventory.Data.OtherValue", b =>
                {
                    b.HasBaseType("HomeInventory.Data.PossibleValue");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.HasDiscriminator().HasValue("other");
                });

            modelBuilder.Entity("HomeInventory.Data.Inventory", b =>
                {
                    b.HasOne("HomeInventory.Data.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("HomeInventory.Data.Owner", "Owner")
                        .WithMany("Inventories")
                        .HasForeignKey("OwnerId");

                    b.HasOne("HomeInventory.Data.PossibleValue", "PossibleValue")
                        .WithMany()
                        .HasForeignKey("PossibleValueId");

                    b.Navigation("Location");

                    b.Navigation("Owner");

                    b.Navigation("PossibleValue");
                });

            modelBuilder.Entity("HomeInventory.Data.Owner", b =>
                {
                    b.Navigation("Inventories");
                });
#pragma warning restore 612, 618
        }
    }
}
