using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ProgrammingGame.Data.Infrastructure.Context;

namespace ProgrammingGame.Data.Migrations
{
    [DbContext(typeof(ProgrammingGameContext))]
    [Migration("20170114232904_KeysFix")]
    partial class KeysFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole<long>", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("Roles","security");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<long>("RoleId")
                        .HasColumnName("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims","security");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<long>("UserId")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims","security");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<long>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<long>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins","security");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnName("UserId");

                    b.Property<long>("RoleId")
                        .HasColumnName("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles","security");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnName("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens","security");
                });

            modelBuilder.Entity("ProgrammingGame.Data.Entities.Character", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("Cash");

                    b.Property<long>("Experience");

                    b.Property<bool>("IsActive");

                    b.Property<Guid>("Key");

                    b.Property<DateTime>("LastStateChangeTime");

                    b.Property<int>("Level");

                    b.Property<string>("Name");

                    b.Property<int>("State");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Character");
                });

            modelBuilder.Entity("ProgrammingGame.Data.Entities.Indicator", b =>
                {
                    b.Property<long>("CharacterId");

                    b.Property<int>("IndicatorTypeId");

                    b.Property<decimal>("Value");

                    b.HasKey("CharacterId", "IndicatorTypeId");

                    b.HasIndex("IndicatorTypeId");

                    b.ToTable("Indicator");
                });

            modelBuilder.Entity("ProgrammingGame.Data.Entities.IndicatorType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("DefaultValue");

                    b.Property<decimal>("MaxValue");

                    b.Property<decimal>("MinValue");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("IndicatorType");
                });

            modelBuilder.Entity("ProgrammingGame.Data.Entities.ItemType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<long>("Price");

                    b.HasKey("Id");

                    b.ToTable("ItemType");
                });

            modelBuilder.Entity("ProgrammingGame.Data.Entities.OwnedItem", b =>
                {
                    b.Property<long>("CharacterId");

                    b.Property<int>("ItemTypeId");

                    b.Property<long>("Amount");

                    b.HasKey("CharacterId", "ItemTypeId");

                    b.HasIndex("ItemTypeId");

                    b.ToTable("OwnedItem");
                });

            modelBuilder.Entity("ProgrammingGame.Data.Entities.SystemAction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CharacterId");

                    b.Property<DateTime>("LastExecutionTime");

                    b.Property<int>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("TypeId");

                    b.ToTable("SystemAction");
                });

            modelBuilder.Entity("ProgrammingGame.Data.Entities.SystemActionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<TimeSpan>("DelayBeetwenExecuting");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("SystemActionType");
                });

            modelBuilder.Entity("ProgrammingGame.Data.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<long?>("CharacterId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("Users","security");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<long>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole<long>")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<long>", b =>
                {
                    b.HasOne("ProgrammingGame.Data.Entities.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<long>", b =>
                {
                    b.HasOne("ProgrammingGame.Data.Entities.User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<long>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole<long>")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProgrammingGame.Data.Entities.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProgrammingGame.Data.Entities.Indicator", b =>
                {
                    b.HasOne("ProgrammingGame.Data.Entities.Character", "Character")
                        .WithMany("Indicators")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProgrammingGame.Data.Entities.IndicatorType", "IndicatorType")
                        .WithMany()
                        .HasForeignKey("IndicatorTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProgrammingGame.Data.Entities.OwnedItem", b =>
                {
                    b.HasOne("ProgrammingGame.Data.Entities.Character", "Character")
                        .WithMany("OwnedItems")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProgrammingGame.Data.Entities.ItemType", "ItemType")
                        .WithMany()
                        .HasForeignKey("ItemTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProgrammingGame.Data.Entities.SystemAction", b =>
                {
                    b.HasOne("ProgrammingGame.Data.Entities.Character", "Character")
                        .WithMany("SystemActions")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProgrammingGame.Data.Entities.SystemActionType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProgrammingGame.Data.Entities.User", b =>
                {
                    b.HasOne("ProgrammingGame.Data.Entities.Character", "Character")
                        .WithOne("User")
                        .HasForeignKey("ProgrammingGame.Data.Entities.User", "CharacterId");
                });
        }
    }
}
