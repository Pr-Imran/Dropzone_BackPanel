﻿// <auto-generated />
using System;
using DropZone_BackPanel.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DropZone_BackPanel.Migrations
{
    [DbContext(typeof(DropZoneDbContext))]
    partial class DropZoneDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DropZone_BackPanel.Data.Entity.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("DropZone_BackPanel.Data.Entity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("bpNo")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("createdBy")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<int?>("isActive")
                        .HasColumnType("int");

                    b.Property<string>("otpCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("DropZone_BackPanel.Data.Entity.Droper.PersonsData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("createdBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int?>("isDelete")
                        .HasColumnType("int");

                    b.Property<string>("latitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("longitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("unionId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("updatedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int?>("villageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("unionId");

                    b.HasIndex("villageId");

                    b.ToTable("personsDatas");
                });

            modelBuilder.Entity("DropZone_BackPanel.Data.Entity.Droper.UploadedFiles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("attachmentUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("createdBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int?>("isDelete")
                        .HasColumnType("int");

                    b.Property<int?>("personsDataId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("updatedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("personsDataId");

                    b.ToTable("uploadedFiles");
                });

            modelBuilder.Entity("DropZone_BackPanel.Data.Entity.MasterData.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("countryCode")
                        .HasColumnType("NVARCHAR(20)");

                    b.Property<string>("countryName")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<string>("countryNameBn")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("createdBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("isActive")
                        .HasColumnType("NVARCHAR(10)");

                    b.Property<int?>("isDelete")
                        .HasColumnType("int");

                    b.Property<string>("latitude")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<string>("longitude")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<string>("nationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("shortName")
                        .HasColumnType("NVARCHAR(20)");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("updatedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("DropZone_BackPanel.Data.Entity.MasterData.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("createdBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("districtCode")
                        .HasColumnType("NVARCHAR(20)");

                    b.Property<string>("districtName")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<string>("districtNameBn")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<int>("divisionId")
                        .HasColumnType("int");

                    b.Property<string>("isActive")
                        .HasColumnType("NVARCHAR(10)");

                    b.Property<int?>("isDelete")
                        .HasColumnType("int");

                    b.Property<string>("latitude")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<string>("longitude")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<string>("shortName")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("updatedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("divisionId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("DropZone_BackPanel.Data.Entity.MasterData.Division", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("countryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("createdBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("divisionCode")
                        .HasColumnType("NVARCHAR(20)");

                    b.Property<string>("divisionName")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<string>("divisionNameBn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("isActive")
                        .HasColumnType("NVARCHAR(10)");

                    b.Property<int?>("isDelete")
                        .HasColumnType("int");

                    b.Property<string>("latitude")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<string>("longitude")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<string>("shortName")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("updatedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("countryId");

                    b.ToTable("Divisions");
                });

            modelBuilder.Entity("DropZone_BackPanel.Data.Entity.MasterData.PostOffice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("createdBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int?>("isDelete")
                        .HasColumnType("int");

                    b.Property<string>("postalCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("postalName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("postalNameBn")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("postalShortName")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("thanaId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("updatedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("thanaId");

                    b.ToTable("postOffices");
                });

            modelBuilder.Entity("DropZone_BackPanel.Data.Entity.MasterData.RangeMetro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("createdBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("isActive")
                        .HasColumnType("NVARCHAR(10)");

                    b.Property<int?>("isDelete")
                        .HasColumnType("int");

                    b.Property<string>("latitude")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<string>("longitude")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<int?>("pimsRangeId")
                        .HasColumnType("int");

                    b.Property<string>("pimsRangeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("rangeMetroName")
                        .HasColumnType("NVARCHAR(350)");

                    b.Property<string>("rangeMetroNameBn")
                        .HasColumnType("NVARCHAR(350)");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("updatedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("RangeMetros");
                });

            modelBuilder.Entity("DropZone_BackPanel.Data.Entity.MasterData.Thana", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("createdBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int?>("districtId")
                        .HasColumnType("int");

                    b.Property<string>("isActive")
                        .HasColumnType("NVARCHAR(10)");

                    b.Property<int?>("isDelete")
                        .HasColumnType("int");

                    b.Property<string>("latitude")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<string>("longitude")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<int?>("rangeMetroId")
                        .HasColumnType("int");

                    b.Property<string>("shortName")
                        .HasColumnType("NVARCHAR(50)");

                    b.Property<string>("thanaCode")
                        .HasColumnType("NVARCHAR(20)");

                    b.Property<string>("thanaName")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<string>("thanaNameBn")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("updatedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("districtId");

                    b.HasIndex("rangeMetroId");

                    b.ToTable("Thanas");
                });

            modelBuilder.Entity("DropZone_BackPanel.Data.Entity.MasterData.UnionWard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("createdBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int?>("districtsId")
                        .HasColumnType("int");

                    b.Property<string>("isActive")
                        .HasColumnType("NVARCHAR(10)");

                    b.Property<int?>("isDelete")
                        .HasColumnType("int");

                    b.Property<string>("latitude")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<string>("longitude")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<string>("shortName")
                        .HasColumnType("NVARCHAR(50)");

                    b.Property<int>("thanaId")
                        .HasColumnType("int");

                    b.Property<string>("unionCode")
                        .HasColumnType("NVARCHAR(20)");

                    b.Property<string>("unionName")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<string>("unionNameBn")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("updatedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("districtsId");

                    b.HasIndex("thanaId");

                    b.ToTable("UnionWards");
                });

            modelBuilder.Entity("DropZone_BackPanel.Data.Entity.MasterData.UserLogHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Latitude")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Longitude")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("browserName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("createdBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("deviceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("deviceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ipAddress")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("ipAddress2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("isDelete")
                        .HasColumnType("int");

                    b.Property<DateTime>("logTime")
                        .HasMaxLength(250)
                        .HasColumnType("datetime2");

                    b.Property<string>("mac")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pcName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int?>("status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("updatedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("userId")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("UserLogHistories");
                });

            modelBuilder.Entity("DropZone_BackPanel.Data.Entity.MasterData.Village", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("createdBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int?>("districtsId")
                        .HasColumnType("int");

                    b.Property<string>("isActive")
                        .HasColumnType("NVARCHAR(10)");

                    b.Property<int?>("isDelete")
                        .HasColumnType("int");

                    b.Property<string>("latitude")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<string>("longitude")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<string>("shortName")
                        .HasColumnType("NVARCHAR(50)");

                    b.Property<int?>("thanaId")
                        .HasColumnType("int");

                    b.Property<int>("unionWardId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("updatedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("villageCode")
                        .HasColumnType("NVARCHAR(20)");

                    b.Property<string>("villageName")
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<string>("villageNameBn")
                        .HasColumnType("NVARCHAR(120)");

                    b.HasKey("Id");

                    b.HasIndex("districtsId");

                    b.HasIndex("thanaId");

                    b.HasIndex("unionWardId");

                    b.ToTable("Villages");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DropZone_BackPanel.Data.Entity.Droper.PersonsData", b =>
                {
                    b.HasOne("DropZone_BackPanel.Data.Entity.MasterData.UnionWard", "union")
                        .WithMany()
                        .HasForeignKey("unionId");

                    b.HasOne("DropZone_BackPanel.Data.Entity.MasterData.Village", "village")
                        .WithMany()
                        .HasForeignKey("villageId");

                    b.Navigation("union");

                    b.Navigation("village");
                });

            modelBuilder.Entity("DropZone_BackPanel.Data.Entity.Droper.UploadedFiles", b =>
                {
                    b.HasOne("DropZone_BackPanel.Data.Entity.Droper.PersonsData", "personsData")
                        .WithMany()
                        .HasForeignKey("personsDataId");

                    b.Navigation("personsData");
                });

            modelBuilder.Entity("DropZone_BackPanel.Data.Entity.MasterData.District", b =>
                {
                    b.HasOne("DropZone_BackPanel.Data.Entity.MasterData.Division", "division")
                        .WithMany()
                        .HasForeignKey("divisionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("division");
                });

            modelBuilder.Entity("DropZone_BackPanel.Data.Entity.MasterData.Division", b =>
                {
                    b.HasOne("DropZone_BackPanel.Data.Entity.MasterData.Country", "country")
                        .WithMany()
                        .HasForeignKey("countryId");

                    b.Navigation("country");
                });

            modelBuilder.Entity("DropZone_BackPanel.Data.Entity.MasterData.PostOffice", b =>
                {
                    b.HasOne("DropZone_BackPanel.Data.Entity.MasterData.Thana", "thana")
                        .WithMany()
                        .HasForeignKey("thanaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("thana");
                });

            modelBuilder.Entity("DropZone_BackPanel.Data.Entity.MasterData.Thana", b =>
                {
                    b.HasOne("DropZone_BackPanel.Data.Entity.MasterData.District", "district")
                        .WithMany()
                        .HasForeignKey("districtId");

                    b.HasOne("DropZone_BackPanel.Data.Entity.MasterData.RangeMetro", "rangeMetro")
                        .WithMany()
                        .HasForeignKey("rangeMetroId");

                    b.Navigation("district");

                    b.Navigation("rangeMetro");
                });

            modelBuilder.Entity("DropZone_BackPanel.Data.Entity.MasterData.UnionWard", b =>
                {
                    b.HasOne("DropZone_BackPanel.Data.Entity.MasterData.District", "districts")
                        .WithMany()
                        .HasForeignKey("districtsId");

                    b.HasOne("DropZone_BackPanel.Data.Entity.MasterData.Thana", "thana")
                        .WithMany()
                        .HasForeignKey("thanaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("districts");

                    b.Navigation("thana");
                });

            modelBuilder.Entity("DropZone_BackPanel.Data.Entity.MasterData.Village", b =>
                {
                    b.HasOne("DropZone_BackPanel.Data.Entity.MasterData.District", "districts")
                        .WithMany()
                        .HasForeignKey("districtsId");

                    b.HasOne("DropZone_BackPanel.Data.Entity.MasterData.Thana", "thana")
                        .WithMany()
                        .HasForeignKey("thanaId");

                    b.HasOne("DropZone_BackPanel.Data.Entity.MasterData.UnionWard", "unionWard")
                        .WithMany()
                        .HasForeignKey("unionWardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("districts");

                    b.Navigation("thana");

                    b.Navigation("unionWard");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("DropZone_BackPanel.Data.Entity.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DropZone_BackPanel.Data.Entity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DropZone_BackPanel.Data.Entity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("DropZone_BackPanel.Data.Entity.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DropZone_BackPanel.Data.Entity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DropZone_BackPanel.Data.Entity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
