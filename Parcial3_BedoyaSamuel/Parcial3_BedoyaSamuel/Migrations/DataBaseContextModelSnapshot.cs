﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Parcial3_BedoyaSamuel.DAL;

#nullable disable

namespace Parcial3_BedoyaSamuel.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Parcial3_BedoyaSamuel.DAL.Entities.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("Parcial3_BedoyaSamuel.DAL.Entities.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NumberPlate")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("ServicesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ServicesId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("Parcial3_BedoyaSamuel.DAL.Entities.VehicleDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("VehiclesDetails");
                });

            modelBuilder.Entity("Parcial3_BedoyaSamuel.DAL.Entities.Vehicle", b =>
                {
                    b.HasOne("Parcial3_BedoyaSamuel.DAL.Entities.Service", "Services")
                        .WithMany("Vehicles")
                        .HasForeignKey("ServicesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Services");
                });

            modelBuilder.Entity("Parcial3_BedoyaSamuel.DAL.Entities.VehicleDetails", b =>
                {
                    b.HasOne("Parcial3_BedoyaSamuel.DAL.Entities.Vehicle", "Vehicle")
                        .WithMany("VehicleDetails")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("Parcial3_BedoyaSamuel.DAL.Entities.Service", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("Parcial3_BedoyaSamuel.DAL.Entities.Vehicle", b =>
                {
                    b.Navigation("VehicleDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
