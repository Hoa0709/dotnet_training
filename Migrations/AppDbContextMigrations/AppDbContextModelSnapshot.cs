﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using app.Connects;

#nullable disable

namespace app.Migrations.AppDbContextMigrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("app.Models.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Decription")
                        .HasColumnType("ntext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Pathimage")
                        .HasColumnType("ntext");

                    b.HasKey("Id");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("app.Models.BookTicket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentityNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfTickets")
                        .HasColumnType("int");

                    b.Property<bool>("TakeTicket")
                        .HasColumnType("bit");

                    b.Property<int>("TicketId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BookTickets");
                });

            modelBuilder.Entity("app.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .HasColumnType("ntext");

                    b.Property<DateTime>("CreatAt")
                        .HasColumnType("datetime2");

                    b.Property<float>("Latitude")
                        .HasColumnType("real");

                    b.Property<float>("Longtitude")
                        .HasColumnType("real");

                    b.Property<string>("Pathimage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("app.Models.News", b =>
                {
                    b.Property<int>("nid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("nid"));

                    b.Property<string>("author")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("content")
                        .HasColumnType("ntext");

                    b.Property<string>("pathimage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("postdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("nid");

                    b.ToTable("News");
                });

            modelBuilder.Entity("app.Models.ProgramInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("ntext");

                    b.Property<DateTime>("Create_at")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Held_on")
                        .HasColumnType("datetime2");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Pathimage_list")
                        .HasColumnType("ntext");

                    b.Property<int>("Type_inoff")
                        .HasColumnType("int");

                    b.Property<int>("Type_program")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("app.Models.Support", b =>
                {
                    b.Property<int>("sid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("sid"));

                    b.Property<string>("content")
                        .HasColumnType("ntext");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("sid");

                    b.ToTable("Supports");
                });

            modelBuilder.Entity("app.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Inventory")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("Money");

                    b.Property<int>("ProgramId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
