﻿// <auto-generated />
using System;
using Mediporta.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Mediporta.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240326164953_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("Mediporta.Domain.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<long>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasSynonyms")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsModeratorOnly")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
