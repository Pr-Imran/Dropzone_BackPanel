﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DropZone_BackPanel.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    isActive = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    bpNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    otpCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    countryCode = table.Column<string>(type: "NVARCHAR(20)", nullable: true),
                    countryName = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    countryNameBn = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    shortName = table.Column<string>(type: "NVARCHAR(20)", nullable: true),
                    isActive = table.Column<string>(type: "NVARCHAR(10)", nullable: true),
                    latitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    longitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    isDelete = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rangeMetros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rangeMetroName = table.Column<string>(type: "NVARCHAR(350)", nullable: true),
                    rangeMetroNameBn = table.Column<string>(type: "NVARCHAR(350)", nullable: true),
                    isActive = table.Column<string>(type: "NVARCHAR(10)", nullable: true),
                    latitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    longitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    pimsRangeId = table.Column<int>(type: "int", nullable: true),
                    pimsRangeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isDelete = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rangeMetros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    logTime = table.Column<DateTime>(type: "datetime2", maxLength: 250, nullable: false),
                    status = table.Column<int>(type: "int", nullable: true),
                    ipAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    browserName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    pcName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    mac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ipAddress2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deviceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deviceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isDelete = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "divisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    countryId = table.Column<int>(type: "int", nullable: true),
                    divisionCode = table.Column<string>(type: "NVARCHAR(20)", nullable: true),
                    divisionName = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    divisionNameBn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    shortName = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    isActive = table.Column<string>(type: "NVARCHAR(10)", nullable: true),
                    latitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    longitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    isDelete = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_divisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_divisions_countries_countryId",
                        column: x => x.countryId,
                        principalTable: "countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "districts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    divisionId = table.Column<int>(type: "int", nullable: false),
                    districtCode = table.Column<string>(type: "NVARCHAR(20)", nullable: true),
                    districtName = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    districtNameBn = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    shortName = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    isActive = table.Column<string>(type: "NVARCHAR(10)", nullable: true),
                    latitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    longitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    isDelete = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_districts_divisions_divisionId",
                        column: x => x.divisionId,
                        principalTable: "divisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "thanas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    districtId = table.Column<int>(type: "int", nullable: true),
                    rangeMetroId = table.Column<int>(type: "int", nullable: true),
                    thanaCode = table.Column<string>(type: "NVARCHAR(20)", nullable: true),
                    thanaName = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    thanaNameBn = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    shortName = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    isActive = table.Column<string>(type: "NVARCHAR(10)", nullable: true),
                    latitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    longitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    isDelete = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thanas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_thanas_districts_districtId",
                        column: x => x.districtId,
                        principalTable: "districts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_thanas_rangeMetros_rangeMetroId",
                        column: x => x.rangeMetroId,
                        principalTable: "rangeMetros",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "postOffices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    thanaId = table.Column<int>(type: "int", nullable: false),
                    postalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    postalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    postalShortName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    postalNameBn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    isDelete = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_postOffices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_postOffices_thanas_thanaId",
                        column: x => x.thanaId,
                        principalTable: "thanas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "unionWards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    thanaId = table.Column<int>(type: "int", nullable: false),
                    districtsId = table.Column<int>(type: "int", nullable: true),
                    unionCode = table.Column<string>(type: "NVARCHAR(20)", nullable: true),
                    unionName = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    unionNameBn = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    shortName = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    isActive = table.Column<string>(type: "NVARCHAR(10)", nullable: true),
                    latitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    longitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    isDelete = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unionWards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_unionWards_districts_districtsId",
                        column: x => x.districtsId,
                        principalTable: "districts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_unionWards_thanas_thanaId",
                        column: x => x.thanaId,
                        principalTable: "thanas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "villages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    unionWardId = table.Column<int>(type: "int", nullable: false),
                    thanaId = table.Column<int>(type: "int", nullable: true),
                    districtsId = table.Column<int>(type: "int", nullable: true),
                    villageCode = table.Column<string>(type: "NVARCHAR(20)", nullable: true),
                    villageName = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    villageNameBn = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    shortName = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    isActive = table.Column<string>(type: "NVARCHAR(10)", nullable: true),
                    latitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    longitude = table.Column<string>(type: "NVARCHAR(120)", nullable: true),
                    isDelete = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_villages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_villages_districts_districtsId",
                        column: x => x.districtsId,
                        principalTable: "districts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_villages_thanas_thanaId",
                        column: x => x.thanaId,
                        principalTable: "thanas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_villages_unionWards_unionWardId",
                        column: x => x.unionWardId,
                        principalTable: "unionWards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personsDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    unionId = table.Column<int>(type: "int", nullable: true),
                    villageId = table.Column<int>(type: "int", nullable: true),
                    latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isDelete = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personsDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_personsDatas_unionWards_unionId",
                        column: x => x.unionId,
                        principalTable: "unionWards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_personsDatas_villages_villageId",
                        column: x => x.villageId,
                        principalTable: "villages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "uploadedFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    personsDataId = table.Column<int>(type: "int", nullable: true),
                    attachmentUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isDelete = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_uploadedFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_uploadedFiles_personsDatas_personsDataId",
                        column: x => x.personsDataId,
                        principalTable: "personsDatas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_districts_divisionId",
                table: "districts",
                column: "divisionId");

            migrationBuilder.CreateIndex(
                name: "IX_divisions_countryId",
                table: "divisions",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_personsDatas_unionId",
                table: "personsDatas",
                column: "unionId");

            migrationBuilder.CreateIndex(
                name: "IX_personsDatas_villageId",
                table: "personsDatas",
                column: "villageId");

            migrationBuilder.CreateIndex(
                name: "IX_postOffices_thanaId",
                table: "postOffices",
                column: "thanaId");

            migrationBuilder.CreateIndex(
                name: "IX_thanas_districtId",
                table: "thanas",
                column: "districtId");

            migrationBuilder.CreateIndex(
                name: "IX_thanas_rangeMetroId",
                table: "thanas",
                column: "rangeMetroId");

            migrationBuilder.CreateIndex(
                name: "IX_unionWards_districtsId",
                table: "unionWards",
                column: "districtsId");

            migrationBuilder.CreateIndex(
                name: "IX_unionWards_thanaId",
                table: "unionWards",
                column: "thanaId");

            migrationBuilder.CreateIndex(
                name: "IX_uploadedFiles_personsDataId",
                table: "uploadedFiles",
                column: "personsDataId");

            migrationBuilder.CreateIndex(
                name: "IX_villages_districtsId",
                table: "villages",
                column: "districtsId");

            migrationBuilder.CreateIndex(
                name: "IX_villages_thanaId",
                table: "villages",
                column: "thanaId");

            migrationBuilder.CreateIndex(
                name: "IX_villages_unionWardId",
                table: "villages",
                column: "unionWardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "postOffices");

            migrationBuilder.DropTable(
                name: "uploadedFiles");

            migrationBuilder.DropTable(
                name: "UserLogHistories");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "personsDatas");

            migrationBuilder.DropTable(
                name: "villages");

            migrationBuilder.DropTable(
                name: "unionWards");

            migrationBuilder.DropTable(
                name: "thanas");

            migrationBuilder.DropTable(
                name: "districts");

            migrationBuilder.DropTable(
                name: "rangeMetros");

            migrationBuilder.DropTable(
                name: "divisions");

            migrationBuilder.DropTable(
                name: "countries");
        }
    }
}
