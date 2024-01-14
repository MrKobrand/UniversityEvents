using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class Init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "EventSection",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "varchar(128)", nullable: false),
                Description = table.Column<string>(type: "varchar(256)", nullable: true),
                Order = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short) 0),
                CreateDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                UpdateDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EventSection", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Image",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                FileName = table.Column<string>(type: "varchar(128)", nullable: false),
                Link = table.Column<string>(type: "varchar(256)", nullable: false),
                StorageType = table.Column<short>(type: "smallint", nullable: false),
                CreateDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                UpdateDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Image", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "EventCategory",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "varchar(128)", nullable: false),
                Order = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short) 0),
                SectionId = table.Column<long>(type: "bigint", nullable: false),
                CreateDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                UpdateDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EventCategory", x => x.Id);
                table.ForeignKey(
                    name: "FK_EventCategory_EventSection_SectionId",
                    column: x => x.SectionId,
                    principalTable: "EventSection",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "User",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                FirstName = table.Column<string>(type: "varchar(256)", nullable: false),
                LastName = table.Column<string>(type: "varchar(256)", nullable: false),
                MiddleName = table.Column<string>(type: "varchar(256)", nullable: true),
                Role = table.Column<short>(type: "smallint", nullable: false),
                Email = table.Column<string>(type: "varchar(256)", nullable: false),
                Password = table.Column<string>(type: "varchar(256)", nullable: false),
                PasswordSalt = table.Column<string>(type: "varchar(256)", nullable: false),
                AvatarId = table.Column<long>(type: "bigint", nullable: true),
                CreateDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                UpdateDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_User", x => x.Id);
                table.ForeignKey(
                    name: "FK_User_Image_AvatarId",
                    column: x => x.AvatarId,
                    principalTable: "Image",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Event",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Type = table.Column<short>(type: "smallint", nullable: false),
                Date = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                Duration = table.Column<long>(type: "bigint", nullable: false),
                Place = table.Column<string>(type: "varchar(256)", nullable: false),
                Subject = table.Column<string>(type: "varchar(256)", nullable: false),
                Announcement = table.Column<string>(type: "varchar(256)", nullable: true),
                Content = table.Column<string>(type: "text", nullable: true),
                PreviewImageId = table.Column<long>(type: "bigint", nullable: true),
                CategoryId = table.Column<long>(type: "bigint", nullable: false),
                AuthorId = table.Column<long>(type: "bigint", nullable: false),
                CreateDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                UpdateDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Event", x => x.Id);
                table.ForeignKey(
                    name: "FK_Event_EventCategory_CategoryId",
                    column: x => x.CategoryId,
                    principalTable: "EventCategory",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Event_Image_PreviewImageId",
                    column: x => x.PreviewImageId,
                    principalTable: "Image",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Event_User_AuthorId",
                    column: x => x.AuthorId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "RefreshToken",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                UserId = table.Column<long>(type: "bigint", nullable: false),
                Token = table.Column<string>(type: "varchar(256)", nullable: false),
                RememberMe = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                ExpiryDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                CreateDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                UpdateDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RefreshToken", x => x.Id);
                table.ForeignKey(
                    name: "FK_RefreshToken_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "EventParticipant",
            columns: table => new
            {
                EventId = table.Column<long>(type: "bigint", nullable: false),
                UserId = table.Column<long>(type: "bigint", nullable: false),
                Id = table.Column<long>(type: "bigint", nullable: false),
                CreateDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                UpdateDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EventParticipant", x => new { x.EventId, x.UserId });
                table.ForeignKey(
                    name: "FK_EventParticipant_Event_EventId",
                    column: x => x.EventId,
                    principalTable: "Event",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_EventParticipant_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "EventSpeaker",
            columns: table => new
            {
                EventId = table.Column<long>(type: "bigint", nullable: false),
                UserId = table.Column<long>(type: "bigint", nullable: false),
                Id = table.Column<long>(type: "bigint", nullable: false),
                CreateDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                UpdateDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EventSpeaker", x => new { x.EventId, x.UserId });
                table.ForeignKey(
                    name: "FK_EventSpeaker_Event_EventId",
                    column: x => x.EventId,
                    principalTable: "Event",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_EventSpeaker_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Event_AuthorId",
            table: "Event",
            column: "AuthorId");

        migrationBuilder.CreateIndex(
            name: "IX_Event_CategoryId",
            table: "Event",
            column: "CategoryId");

        migrationBuilder.CreateIndex(
            name: "IX_Event_PreviewImageId",
            table: "Event",
            column: "PreviewImageId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_EventCategory_SectionId",
            table: "EventCategory",
            column: "SectionId");

        migrationBuilder.CreateIndex(
            name: "IX_EventParticipant_UserId",
            table: "EventParticipant",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_EventSpeaker_UserId",
            table: "EventSpeaker",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_RefreshToken_Token",
            table: "RefreshToken",
            column: "Token",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_RefreshToken_UserId",
            table: "RefreshToken",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_User_AvatarId",
            table: "User",
            column: "AvatarId",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "EventParticipant");

        migrationBuilder.DropTable(
            name: "EventSpeaker");

        migrationBuilder.DropTable(
            name: "RefreshToken");

        migrationBuilder.DropTable(
            name: "Event");

        migrationBuilder.DropTable(
            name: "EventCategory");

        migrationBuilder.DropTable(
            name: "User");

        migrationBuilder.DropTable(
            name: "EventSection");

        migrationBuilder.DropTable(
            name: "Image");
    }
}
