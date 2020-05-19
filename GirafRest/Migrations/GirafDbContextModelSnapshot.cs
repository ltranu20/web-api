﻿// <auto-generated />
using System;
using GirafRest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GirafRest.Migrations
{
    [DbContext(typeof(GirafDbContext))]
    partial class GirafDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GirafRest.GuardianRelation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("CitizenId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("GuardianId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CitizenId");

                    b.HasIndex("GuardianId");

                    b.ToTable("GuardianRelations");
                });

            modelBuilder.Entity("GirafRest.Models.Activity", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<bool>("IsChoiceBoard")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<long>("OtherKey")
                        .HasColumnType("bigint");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<long?>("TimerKey")
                        .HasColumnType("bigint");

                    b.HasKey("Key");

                    b.HasIndex("OtherKey");

                    b.HasIndex("TimerKey");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("GirafRest.Models.Department", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Key");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("GirafRest.Models.DepartmentResource", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("OtherKey")
                        .HasColumnType("bigint");

                    b.Property<long>("PictogramKey")
                        .HasColumnType("bigint");

                    b.HasKey("Key");

                    b.HasIndex("OtherKey");

                    b.HasIndex("PictogramKey");

                    b.ToTable("DepartmentResources");
                });

            modelBuilder.Entity("GirafRest.Models.GirafRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
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
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long?>("DepartmentKey")
                        .HasColumnType("bigint");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDepartment")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long?>("SettingsKey")
                        .HasColumnType("bigint");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<byte[]>("UserIcon")
                        .HasColumnType("longblob");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
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
                        .HasColumnType("bigint");

                    b.Property<int>("AccessLevel")
                        .HasColumnType("int");

                    b.Property<string>("ImageHash")
                        .HasColumnName("ImageHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastEdit")
                        .HasColumnType("datetime(6)");

                    b.Property<byte[]>("Sound")
                        .HasColumnType("longblob");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("Id", "Title")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.ToTable("Pictograms");
                });

            modelBuilder.Entity("GirafRest.Models.Setting", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int?>("ActivitiesCount")
                        .HasColumnType("int");

                    b.Property<int>("CancelMark")
                        .HasColumnType("int");

                    b.Property<int>("CompleteMark")
                        .HasColumnType("int");

                    b.Property<int>("DefaultTimer")
                        .HasColumnType("int");

                    b.Property<bool>("GreyScale")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockTimerControl")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("NrOfDaysToDisplay")
                        .HasColumnType("int");

                    b.Property<int>("Orientation")
                        .HasColumnType("int");

                    b.Property<bool>("PictogramText")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Theme")
                        .HasColumnType("int");

                    b.Property<int?>("TimerSeconds")
                        .HasColumnType("int");

                    b.HasKey("Key");

                    b.ToTable("Setting");
                });

            modelBuilder.Entity("GirafRest.Models.Timer", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("FullLength")
                        .HasColumnType("bigint");

                    b.Property<bool>("Paused")
                        .HasColumnType("tinyint(1)");

                    b.Property<long>("Progress")
                        .HasColumnType("bigint");

                    b.Property<long>("StartTime")
                        .HasColumnType("bigint");

                    b.HasKey("Key");

                    b.ToTable("Timers");
                });

            modelBuilder.Entity("GirafRest.Models.UserResource", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("OtherKey")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("PictogramKey")
                        .HasColumnType("bigint");

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
                        .HasColumnType("bigint");

                    b.Property<string>("GirafUserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("ThumbnailKey")
                        .HasColumnType("bigint");

                    b.Property<int>("WeekNumber")
                        .HasColumnType("int");

                    b.Property<int>("WeekYear")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GirafUserId");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("ThumbnailKey");

                    b.ToTable("Weeks");
                });

            modelBuilder.Entity("GirafRest.Models.WeekTemplate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint");

                    b.Property<long>("DepartmentKey")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("ThumbnailKey")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentKey");

                    b.HasIndex("ThumbnailKey");

                    b.ToTable("WeekTemplates");
                });

            modelBuilder.Entity("GirafRest.Models.Weekday", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<long?>("WeekId")
                        .HasColumnType("bigint");

                    b.Property<long?>("WeekTemplateId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("WeekId");

                    b.HasIndex("WeekTemplateId");

                    b.ToTable("Weekdays");
                });

            modelBuilder.Entity("GirafRest.PictogramRelation", b =>
                {
                    b.Property<long>("ActivityId")
                        .HasColumnType("bigint");

                    b.Property<long>("PictogramId")
                        .HasColumnType("bigint");

                    b.HasKey("ActivityId", "PictogramId");

                    b.HasIndex("PictogramId");

                    b.ToTable("PictogramRelations");
                });

            modelBuilder.Entity("GirafRest.WeekDayColor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<string>("HexColor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("SettingId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("SettingId");

                    b.ToTable("WeekDayColors");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("GirafRest.GuardianRelation", b =>
                {
                    b.HasOne("GirafRest.Models.GirafUser", "Citizen")
                        .WithMany("Guardians")
                        .HasForeignKey("CitizenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GirafRest.Models.GirafUser", "Guardian")
                        .WithMany("Citizens")
                        .HasForeignKey("GuardianId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GirafRest.Models.Activity", b =>
                {
                    b.HasOne("GirafRest.Models.Weekday", "Other")
                        .WithMany("Activities")
                        .HasForeignKey("OtherKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GirafRest.Models.Timer", "Timer")
                        .WithMany()
                        .HasForeignKey("TimerKey")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("GirafRest.Models.DepartmentResource", b =>
                {
                    b.HasOne("GirafRest.Models.Department", "Other")
                        .WithMany("Resources")
                        .HasForeignKey("OtherKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GirafRest.Models.Pictogram", "Pictogram")
                        .WithMany("Departments")
                        .HasForeignKey("PictogramKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GirafRest.Models.GirafUser", b =>
                {
                    b.HasOne("GirafRest.Models.Department", "Department")
                        .WithMany("Members")
                        .HasForeignKey("DepartmentKey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GirafRest.Models.Setting", "Settings")
                        .WithMany()
                        .HasForeignKey("SettingsKey");
                });

            modelBuilder.Entity("GirafRest.Models.UserResource", b =>
                {
                    b.HasOne("GirafRest.Models.GirafUser", "Other")
                        .WithMany("Resources")
                        .HasForeignKey("OtherKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GirafRest.Models.Pictogram", "Pictogram")
                        .WithMany("Users")
                        .HasForeignKey("PictogramKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GirafRest.Models.Week", b =>
                {
                    b.HasOne("GirafRest.Models.GirafUser", null)
                        .WithMany("WeekSchedule")
                        .HasForeignKey("GirafUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GirafRest.Models.Pictogram", "Thumbnail")
                        .WithMany()
                        .HasForeignKey("ThumbnailKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GirafRest.Models.WeekTemplate", b =>
                {
                    b.HasOne("GirafRest.Models.Department", "Department")
                        .WithMany("WeekTemplates")
                        .HasForeignKey("DepartmentKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GirafRest.Models.Pictogram", "Thumbnail")
                        .WithMany()
                        .HasForeignKey("ThumbnailKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GirafRest.Models.Weekday", b =>
                {
                    b.HasOne("GirafRest.Models.Week", null)
                        .WithMany("Weekdays")
                        .HasForeignKey("WeekId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GirafRest.Models.WeekTemplate", null)
                        .WithMany("Weekdays")
                        .HasForeignKey("WeekTemplateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GirafRest.PictogramRelation", b =>
                {
                    b.HasOne("GirafRest.Models.Activity", "Activity")
                        .WithMany("Pictograms")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GirafRest.Models.Pictogram", "Pictogram")
                        .WithMany("Activities")
                        .HasForeignKey("PictogramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GirafRest.WeekDayColor", b =>
                {
                    b.HasOne("GirafRest.Models.Setting", "Setting")
                        .WithMany("WeekDayColors")
                        .HasForeignKey("SettingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("GirafRest.Models.GirafRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GirafRest.Models.GirafUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GirafRest.Models.GirafUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("GirafRest.Models.GirafRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GirafRest.Models.GirafUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("GirafRest.Models.GirafUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
