﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechnologyWK.Data;

#nullable disable

namespace TechnologyWK.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TechnologyWK.Models.Categories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("NameCategory")
                        .HasColumnType("longtext")
                        .HasColumnName("NameCategory");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("TechnologyWK.Models.Products", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Description");

                    b.Property<int?>("IdCategory")
                        .HasColumnType("int");

                    b.Property<string>("Ncm")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Ncm");

                    b.Property<decimal?>("PriceCost")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("PriceCost");

                    b.Property<decimal?>("PriceSale")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("PriceSale");

                    b.HasKey("Id");

                    b.HasIndex("IdCategory");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("TechnologyWK.Models.Products", b =>
                {
                    b.HasOne("TechnologyWK.Models.Categories", "CategoriaNavigation")
                        .WithMany("Products")
                        .HasForeignKey("IdCategory");

                    b.Navigation("CategoriaNavigation");
                });

            modelBuilder.Entity("TechnologyWK.Models.Categories", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
