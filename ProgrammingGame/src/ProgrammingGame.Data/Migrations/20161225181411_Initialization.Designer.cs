using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ProgrammingGame.Data.Infrastructure.Data;

namespace ProgrammingGame.Data.Migrations
{
    [DbContext(typeof(ProgrammingGameContext))]
    [Migration("20161225181411_Initialization")]
    partial class Initialization
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProgrammingGame.Data.Domain.Entities.Character", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("Cash");

                    b.Property<long>("Experience");

                    b.Property<int>("Level");

                    b.Property<string>("Name");

                    b.Property<int>("State");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("ProgrammingGame.Data.Domain.Entities.Indicator", b =>
                {
                    b.Property<long>("CharacterId");

                    b.Property<int>("IndicatorTypeId");

                    b.Property<decimal>("Value");

                    b.HasKey("CharacterId", "IndicatorTypeId");

                    b.HasIndex("CharacterId");

                    b.HasIndex("IndicatorTypeId");

                    b.ToTable("Indicators");
                });

            modelBuilder.Entity("ProgrammingGame.Data.Domain.Entities.IndicatorType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("DefaultValue");

                    b.Property<decimal>("MaxValue");

                    b.Property<decimal>("MinValue");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("IndicatorTypes");
                });

            modelBuilder.Entity("ProgrammingGame.Data.Domain.Entities.ItemType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<long>("Price");

                    b.HasKey("Id");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("ProgrammingGame.Data.Domain.Entities.OwnedItem", b =>
                {
                    b.Property<long>("CharacterId");

                    b.Property<int>("ItemTypeId");

                    b.Property<long>("Amount");

                    b.HasKey("CharacterId", "ItemTypeId");

                    b.HasIndex("CharacterId");

                    b.HasIndex("ItemTypeId");

                    b.ToTable("OwnedItems");
                });

            modelBuilder.Entity("ProgrammingGame.Data.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProgrammingGame.Data.Domain.Entities.Character", b =>
                {
                    b.HasOne("ProgrammingGame.Data.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProgrammingGame.Data.Domain.Entities.Indicator", b =>
                {
                    b.HasOne("ProgrammingGame.Data.Domain.Entities.Character", "Character")
                        .WithMany("Indicators")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProgrammingGame.Data.Domain.Entities.IndicatorType", "IndicatorType")
                        .WithMany()
                        .HasForeignKey("IndicatorTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProgrammingGame.Data.Domain.Entities.OwnedItem", b =>
                {
                    b.HasOne("ProgrammingGame.Data.Domain.Entities.Character", "Character")
                        .WithMany("OwnedItems")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProgrammingGame.Data.Domain.Entities.ItemType", "ItemType")
                        .WithMany()
                        .HasForeignKey("ItemTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
