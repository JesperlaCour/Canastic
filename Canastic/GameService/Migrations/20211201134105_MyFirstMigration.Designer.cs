﻿// <auto-generated />
using System;
using GameService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GameService.Migrations
{
    [DbContext(typeof(GameContext))]
    [Migration("20211201134105_MyFirstMigration")]
    partial class MyFirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GameService.Models.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Finished")
                        .HasColumnType("bit");

                    b.Property<int>("Rounds")
                        .HasColumnType("int");

                    b.Property<int>("TeamOnePoints")
                        .HasColumnType("int");

                    b.Property<int?>("TeamOneTeamId")
                        .HasColumnType("int");

                    b.Property<int>("TeamTwoPoints")
                        .HasColumnType("int");

                    b.Property<int?>("TeamTwoTeamId")
                        .HasColumnType("int");

                    b.HasKey("GameId");

                    b.HasIndex("TeamOneTeamId");

                    b.HasIndex("TeamTwoTeamId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("GameService.Models.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("PlayerOne")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlayerTwo")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TeamName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeamId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("GameService.Models.Game", b =>
                {
                    b.HasOne("GameService.Models.Team", "TeamOne")
                        .WithMany()
                        .HasForeignKey("TeamOneTeamId");

                    b.HasOne("GameService.Models.Team", "TeamTwo")
                        .WithMany()
                        .HasForeignKey("TeamTwoTeamId");

                    b.Navigation("TeamOne");

                    b.Navigation("TeamTwo");
                });
#pragma warning restore 612, 618
        }
    }
}
