using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Nortal.Context;

namespace Nortal.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Nortal.Context.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Nortal.Context.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileName");

                    b.Property<int?>("ProductId");

                    b.Property<int>("Size");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("Nortal.Context.ProductSpecifications", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("CameraMpix");

                    b.Property<string>("Manufacturer");

                    b.Property<string>("OperatingSystem");

                    b.Property<int>("ProductId");

                    b.Property<int>("Storage");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("ProductSpecifications");
                });

            modelBuilder.Entity("Nortal.Context.ProductImage", b =>
                {
                    b.HasOne("Nortal.Context.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("Nortal.Context.ProductSpecifications", b =>
                {
                    b.HasOne("Nortal.Context.Product", "Product")
                        .WithOne("ProductSpecifications")
                        .HasForeignKey("Nortal.Context.ProductSpecifications", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
