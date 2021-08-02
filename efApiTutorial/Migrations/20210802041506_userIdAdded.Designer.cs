﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using efApiTutorial.Data;

namespace efApiTutorial.Migrations
{
    [DbContext(typeof(QuotesDbContext))]
    [Migration("20210802041506_userIdAdded")]
    partial class userIdAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("efApiTutorial.Models.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Type")
                        .IsRequired();

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Quotes");
                });
#pragma warning restore 612, 618
        }
    }
}
