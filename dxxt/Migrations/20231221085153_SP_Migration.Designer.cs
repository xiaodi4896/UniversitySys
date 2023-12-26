﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using dxxt.Data;

#nullable disable

namespace dxxt.Migrations
{
    [DbContext(typeof(dxxtContext))]
    [Migration("20231221085153_SP_Migration")]
    partial class SP_Migration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("dxxt.Models.Lecturer", b =>
                {
                    b.Property<int>("LecturerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LecturerID"));

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("LecturerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("LecturerID");

                    b.ToTable("Lecturer");
                });

            modelBuilder.Entity("dxxt.Models.Score", b =>
                {
                    b.Property<int>("ScoreID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScoreID"));

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<decimal?>("Marks")
                        .HasColumnType("decimal(5,2)");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.Property<int>("SubjectID")
                        .HasColumnType("int");

                    b.HasKey("ScoreID");

                    b.HasIndex("StudentID");

                    b.HasIndex("SubjectID");

                    b.ToTable("Score");
                });

            modelBuilder.Entity("dxxt.Models.ScoreStuSub", b =>
                {
                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<decimal?>("Marks")
                        .HasColumnType("decimal(5,2)");

                    b.Property<int>("ScoreID")
                        .HasColumnType("int");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.Property<string>("StudentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubjectID")
                        .HasColumnType("int");

                    b.Property<string>("SubjectName")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("ScoreStuSub");
                });

            modelBuilder.Entity("dxxt.Models.Student", b =>
                {
                    b.Property<int>("StudentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime>("Intake")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("StudentID");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("dxxt.Models.Subject", b =>
                {
                    b.Property<int>("SubjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubjectID"));

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int>("LecturerID")
                        .HasColumnType("int");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("SubjectID");

                    b.HasIndex("LecturerID");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("dxxt.Models.SubjectLec", b =>
                {
                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int>("LecturerID")
                        .HasColumnType("int");

                    b.Property<string>("LecturerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubjectID")
                        .HasColumnType("int");

                    b.Property<string>("SubjectName")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("SubjectLec");
                });

            modelBuilder.Entity("dxxt.Models.Score", b =>
                {
                    b.HasOne("dxxt.Models.Student", "Student")
                        .WithMany("Scores")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("dxxt.Models.Subject", "Subject")
                        .WithMany("Scores")
                        .HasForeignKey("SubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("dxxt.Models.Subject", b =>
                {
                    b.HasOne("dxxt.Models.Lecturer", "Lecturer")
                        .WithMany("Subjects")
                        .HasForeignKey("LecturerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lecturer");
                });

            modelBuilder.Entity("dxxt.Models.Lecturer", b =>
                {
                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("dxxt.Models.Student", b =>
                {
                    b.Navigation("Scores");
                });

            modelBuilder.Entity("dxxt.Models.Subject", b =>
                {
                    b.Navigation("Scores");
                });
#pragma warning restore 612, 618
        }
    }
}
