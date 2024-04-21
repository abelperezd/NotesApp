﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Notes.Data;

#nullable disable

namespace Notes.Data
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240417141238_updateDatabase")]
    partial class updateDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Notes.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("NoteImportanceId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NoteImportanceId");

                    b.HasIndex("UserId");

                    b.ToTable("Note");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreationDate = new DateTime(2010, 10, 4, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            NoteImportanceId = 1,
                            Text = "First note",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            CreationDate = new DateTime(2010, 10, 5, 15, 32, 1, 0, DateTimeKind.Unspecified),
                            NoteImportanceId = 2,
                            Text = "Second note!",
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            CreationDate = new DateTime(2011, 8, 3, 21, 16, 2, 0, DateTimeKind.Unspecified),
                            NoteImportanceId = 3,
                            Text = "Third note!",
                            UserId = 3
                        });
                });

            modelBuilder.Entity("Notes.Models.NoteImportance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Importance")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("NoteImportance");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Importance = "Low"
                        },
                        new
                        {
                            Id = 2,
                            Importance = "Medium"
                        },
                        new
                        {
                            Id = 3,
                            Importance = "High"
                        });
                });

            modelBuilder.Entity("Notes.Models.NoteLike", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("NoteId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NoteId");

                    b.HasIndex("UserId");

                    b.ToTable("NoteLike");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            NoteId = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            NoteId = 1,
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            NoteId = 1,
                            UserId = 3
                        },
                        new
                        {
                            Id = 4,
                            NoteId = 2,
                            UserId = 3
                        });
                });

            modelBuilder.Entity("Notes.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateOnly>("RegisterDate")
                        .HasColumnType("date");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Abel",
                            RegisterDate = new DateOnly(2010, 10, 4),
                            UserName = "abel99"
                        },
                        new
                        {
                            Id = 2,
                            Name = "David",
                            RegisterDate = new DateOnly(2011, 11, 7),
                            UserName = "david00"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Maria",
                            RegisterDate = new DateOnly(2012, 8, 14),
                            UserName = "mar.ia15"
                        });
                });

            modelBuilder.Entity("Notes.Models.Note", b =>
                {
                    b.HasOne("Notes.Models.NoteImportance", "NoteImportance")
                        .WithMany()
                        .HasForeignKey("NoteImportanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Notes.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NoteImportance");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Notes.Models.NoteLike", b =>
                {
                    b.HasOne("Notes.Models.Note", "Note")
                        .WithMany("Likes")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Notes.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Note");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Notes.Models.Note", b =>
                {
                    b.Navigation("Likes");
                });
#pragma warning restore 612, 618
        }
    }
}
