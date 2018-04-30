﻿// <auto-generated />
using GirafRest.Data;
using GirafRest.Models;
using GirafRest.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace GirafRest.Migrations
{
    [DbContext(typeof(GirafDbContext))]
    [Migration("20180430083904_RenameLauncherSettings")]
    partial class RenameLauncherSettings
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("GirafRest.GuardianRelation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CitizenId")
                        .IsRequired();

                    b.Property<string>("GuardianId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CitizenId");

                    b.HasIndex("GuardianId");

                    b.ToTable("GuardianRelations");
                });

            modelBuilder.Entity("GirafRest.Models.Department", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Key");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.ToTable("Departments");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Department");
                });

            modelBuilder.Entity("GirafRest.Models.DepartmentResource", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("OtherKey");

                    b.Property<long>("PictogramKey");

                    b.Property<long?>("ResourceKey");

                    b.HasKey("Key");

                    b.HasIndex("OtherKey");

                    b.HasIndex("PictogramKey");

                    b.ToTable("DeparmentResources");
                });

            modelBuilder.Entity("GirafRest.Models.GirafRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("GirafRest.Models.GirafUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<long?>("DepartmentKey");

                    b.Property<string>("DisplayName");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsDepartment");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<long?>("SettingsKey");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<byte[]>("UserIcon");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("DepartmentKey");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.HasIndex("SettingsKey");

                    b.HasIndex("Id", "UserName")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("GirafRest.Models.Pictogram", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessLevel");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<byte[]>("Image")
                        .HasColumnName("Image");

                    b.Property<DateTime>("LastEdit");

                    b.Property<byte[]>("Sound");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Id", "Title")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.ToTable("Pictograms");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Pictogram");
                });

            modelBuilder.Entity("GirafRest.Models.Setting", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ActivitiesCount");

                    b.Property<int>("CancelMark");

                    b.Property<int>("ColorThemeWeekSchedules");

                    b.Property<int>("CompleteMark");

                    b.Property<int>("DefaultTimer");

                    b.Property<bool>("GreyScale");

                    b.Property<int?>("NrOfDaysToDisplay");

                    b.Property<int>("Orientation");

                    b.Property<int>("Theme");

                    b.Property<int?>("TimerSeconds");

                    b.HasKey("Key");

                    b.ToTable("Setting");
                });

            modelBuilder.Entity("GirafRest.Models.UserResource", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("OtherKey")
                        .IsRequired();

                    b.Property<long>("PictogramKey");

                    b.Property<long?>("ResourceKey");

                    b.HasKey("Key");

                    b.HasIndex("OtherKey");

                    b.HasIndex("PictogramKey");

                    b.ToTable("UserResources");
                });

            modelBuilder.Entity("GirafRest.Models.Week", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("GirafUserId");

                    b.Property<string>("Name");

                    b.Property<long>("ThumbnailKey");

                    b.HasKey("Id");

                    b.HasIndex("GirafUserId");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("ThumbnailKey");

                    b.ToTable("Weeks");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Week");
                });

            modelBuilder.Entity("GirafRest.Models.Weekday", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Day");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("LastEdit");

                    b.Property<long?>("WeekId");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("WeekId");

                    b.ToTable("Weekdays");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Weekday");
                });

            modelBuilder.Entity("GirafRest.Models.WeekdayResource", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Order");

                    b.Property<long>("OtherKey");

                    b.Property<long>("PictogramKey");

                    b.Property<long?>("ResourceKey");

                    b.HasKey("Key");

                    b.HasIndex("OtherKey");

                    b.HasIndex("PictogramKey");

                    b.ToTable("WeekdayResources");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("GirafRest.Models.WeekTemplate", b =>
                {
                    b.HasBaseType("GirafRest.Models.Week");

                    b.Property<long>("DepartmentKey");

                    b.HasIndex("DepartmentKey");

                    b.ToTable("Weeks");

                    b.HasDiscriminator().HasValue("WeekTemplate");
                });

            modelBuilder.Entity("GirafRest.GuardianRelation", b =>
                {
                    b.HasOne("GirafRest.Models.GirafUser", "Citizen")
                        .WithMany("Guardians")
                        .HasForeignKey("CitizenId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GirafRest.Models.GirafUser", "Guardian")
                        .WithMany("Citizens")
                        .HasForeignKey("GuardianId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GirafRest.Models.DepartmentResource", b =>
                {
                    b.HasOne("GirafRest.Models.Department", "Other")
                        .WithMany("Resources")
                        .HasForeignKey("OtherKey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GirafRest.Models.Pictogram", "Pictogram")
                        .WithMany("Departments")
                        .HasForeignKey("PictogramKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GirafRest.Models.GirafUser", b =>
                {
                    b.HasOne("GirafRest.Models.Department", "Department")
                        .WithMany("Members")
                        .HasForeignKey("DepartmentKey");

                    b.HasOne("GirafRest.Models.Setting", "Settings")
                        .WithMany()
                        .HasForeignKey("SettingsKey");
                });

            modelBuilder.Entity("GirafRest.Models.UserResource", b =>
                {
                    b.HasOne("GirafRest.Models.GirafUser", "Other")
                        .WithMany("Resources")
                        .HasForeignKey("OtherKey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GirafRest.Models.Pictogram", "Pictogram")
                        .WithMany("Users")
                        .HasForeignKey("PictogramKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GirafRest.Models.Week", b =>
                {
                    b.HasOne("GirafRest.Models.GirafUser")
                        .WithMany("WeekSchedule")
                        .HasForeignKey("GirafUserId");

                    b.HasOne("GirafRest.Models.Pictogram", "Thumbnail")
                        .WithMany()
                        .HasForeignKey("ThumbnailKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GirafRest.Models.Weekday", b =>
                {
                    b.HasOne("GirafRest.Models.Week")
                        .WithMany("Weekdays")
                        .HasForeignKey("WeekId");
                });

            modelBuilder.Entity("GirafRest.Models.WeekdayResource", b =>
                {
                    b.HasOne("GirafRest.Models.Weekday", "Other")
                        .WithMany("Activities")
                        .HasForeignKey("OtherKey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GirafRest.Models.Pictogram", "Pictogram")
                        .WithMany()
                        .HasForeignKey("PictogramKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("GirafRest.Models.GirafRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GirafRest.Models.GirafUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GirafRest.Models.GirafUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("GirafRest.Models.GirafRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GirafRest.Models.GirafUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("GirafRest.Models.GirafUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GirafRest.Models.WeekTemplate", b =>
                {
                    b.HasOne("GirafRest.Models.Department", "Department")
                        .WithMany("WeekTemplates")
                        .HasForeignKey("DepartmentKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
