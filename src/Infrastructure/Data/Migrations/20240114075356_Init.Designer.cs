// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Data.Migrations;

[DbContext(typeof(UniversityEventsDbContext))]
[Migration("20240114075356_Init")]
partial class Init
{
    /// <inheritdoc />
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.1")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

        modelBuilder.Entity("Domain.Entities.Event", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("bigint");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

            b.Property<string>("Announcement")
                .HasColumnType("varchar(256)");

            b.Property<long>("AuthorId")
                .HasColumnType("bigint");

            b.Property<long>("CategoryId")
                .HasColumnType("bigint");

            b.Property<string>("Content")
                .HasColumnType("text");

            b.Property<DateTimeOffset>("CreateDate")
                .HasColumnType("timestamptz");

            b.Property<DateTimeOffset>("Date")
                .HasColumnType("timestamptz");

            b.Property<long>("Duration")
                .HasColumnType("bigint");

            b.Property<string>("Place")
                .IsRequired()
                .HasColumnType("varchar(256)");

            b.Property<long?>("PreviewImageId")
                .HasColumnType("bigint");

            b.Property<string>("Subject")
                .IsRequired()
                .HasColumnType("varchar(256)");

            b.Property<short>("Type")
                .HasColumnType("smallint");

            b.Property<DateTimeOffset>("UpdateDate")
                .HasColumnType("timestamptz");

            b.HasKey("Id");

            b.HasIndex("AuthorId");

            b.HasIndex("CategoryId");

            b.HasIndex("PreviewImageId")
                .IsUnique();

            b.ToTable("Event", (string) null);
        });

        modelBuilder.Entity("Domain.Entities.EventCategory", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("bigint");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

            b.Property<DateTimeOffset>("CreateDate")
                .HasColumnType("timestamptz");

            b.Property<string>("Name")
                .IsRequired()
                .HasColumnType("varchar(128)");

            b.Property<short>("Order")
                .ValueGeneratedOnAdd()
                .HasColumnType("smallint")
                .HasDefaultValue((short) 0);

            b.Property<long>("SectionId")
                .HasColumnType("bigint");

            b.Property<DateTimeOffset>("UpdateDate")
                .HasColumnType("timestamptz");

            b.HasKey("Id");

            b.HasIndex("SectionId");

            b.ToTable("EventCategory", (string) null);
        });

        modelBuilder.Entity("Domain.Entities.EventParticipant", b =>
        {
            b.Property<long>("EventId")
                .HasColumnType("bigint");

            b.Property<long>("UserId")
                .HasColumnType("bigint");

            b.Property<DateTimeOffset>("CreateDate")
                .HasColumnType("timestamptz");

            b.Property<long>("Id")
                .HasColumnType("bigint");

            b.Property<DateTimeOffset>("UpdateDate")
                .HasColumnType("timestamptz");

            b.HasKey("EventId", "UserId");

            b.HasIndex("UserId");

            b.ToTable("EventParticipant", (string) null);
        });

        modelBuilder.Entity("Domain.Entities.EventSection", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("bigint");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

            b.Property<DateTimeOffset>("CreateDate")
                .HasColumnType("timestamptz");

            b.Property<string>("Description")
                .HasColumnType("varchar(256)");

            b.Property<string>("Name")
                .IsRequired()
                .HasColumnType("varchar(128)");

            b.Property<short>("Order")
                .ValueGeneratedOnAdd()
                .HasColumnType("smallint")
                .HasDefaultValue((short) 0);

            b.Property<DateTimeOffset>("UpdateDate")
                .HasColumnType("timestamptz");

            b.HasKey("Id");

            b.ToTable("EventSection", (string) null);
        });

        modelBuilder.Entity("Domain.Entities.EventSpeaker", b =>
        {
            b.Property<long>("EventId")
                .HasColumnType("bigint");

            b.Property<long>("UserId")
                .HasColumnType("bigint");

            b.Property<DateTimeOffset>("CreateDate")
                .HasColumnType("timestamptz");

            b.Property<long>("Id")
                .HasColumnType("bigint");

            b.Property<DateTimeOffset>("UpdateDate")
                .HasColumnType("timestamptz");

            b.HasKey("EventId", "UserId");

            b.HasIndex("UserId");

            b.ToTable("EventSpeaker", (string) null);
        });

        modelBuilder.Entity("Domain.Entities.Image", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("bigint");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

            b.Property<DateTimeOffset>("CreateDate")
                .HasColumnType("timestamptz");

            b.Property<string>("FileName")
                .IsRequired()
                .HasColumnType("varchar(128)");

            b.Property<string>("Link")
                .IsRequired()
                .HasColumnType("varchar(256)");

            b.Property<short>("StorageType")
                .HasColumnType("smallint");

            b.Property<DateTimeOffset>("UpdateDate")
                .HasColumnType("timestamptz");

            b.HasKey("Id");

            b.ToTable("Image", (string) null);
        });

        modelBuilder.Entity("Domain.Entities.RefreshToken", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("bigint");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

            b.Property<DateTimeOffset>("CreateDate")
                .HasColumnType("timestamptz");

            b.Property<DateTimeOffset>("ExpiryDate")
                .HasColumnType("timestamptz");

            b.Property<bool>("RememberMe")
                .ValueGeneratedOnAdd()
                .HasColumnType("boolean")
                .HasDefaultValue(false);

            b.Property<string>("Token")
                .IsRequired()
                .HasColumnType("varchar(256)");

            b.Property<DateTimeOffset>("UpdateDate")
                .HasColumnType("timestamptz");

            b.Property<long>("UserId")
                .HasColumnType("bigint");

            b.HasKey("Id");

            b.HasIndex("Token")
                .IsUnique();

            b.HasIndex("UserId");

            b.ToTable("RefreshToken", (string) null);
        });

        modelBuilder.Entity("Domain.Entities.User", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("bigint");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

            b.Property<long?>("AvatarId")
                .HasColumnType("bigint");

            b.Property<DateTimeOffset>("CreateDate")
                .HasColumnType("timestamptz");

            b.Property<string>("Email")
                .IsRequired()
                .HasColumnType("varchar(256)");

            b.Property<string>("FirstName")
                .IsRequired()
                .HasColumnType("varchar(256)");

            b.Property<string>("LastName")
                .IsRequired()
                .HasColumnType("varchar(256)");

            b.Property<string>("MiddleName")
                .HasColumnType("varchar(256)");

            b.Property<string>("Password")
                .IsRequired()
                .HasColumnType("varchar(256)");

            b.Property<string>("PasswordSalt")
                .IsRequired()
                .HasColumnType("varchar(256)");

            b.Property<short>("Role")
                .HasColumnType("smallint");

            b.Property<DateTimeOffset>("UpdateDate")
                .HasColumnType("timestamptz");

            b.HasKey("Id");

            b.HasIndex("AvatarId")
                .IsUnique();

            b.ToTable("User", (string) null);
        });

        modelBuilder.Entity("Domain.Entities.Event", b =>
        {
            b.HasOne("Domain.Entities.User", "Author")
                .WithMany("EventsAsAuthor")
                .HasForeignKey("AuthorId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("Domain.Entities.EventCategory", "Category")
                .WithMany("Events")
                .HasForeignKey("CategoryId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("Domain.Entities.Image", "PreviewImage")
                .WithOne("Event")
                .HasForeignKey("Domain.Entities.Event", "PreviewImageId");

            b.Navigation("Author");

            b.Navigation("Category");

            b.Navigation("PreviewImage");
        });

        modelBuilder.Entity("Domain.Entities.EventCategory", b =>
        {
            b.HasOne("Domain.Entities.EventSection", "Section")
                .WithMany("Categories")
                .HasForeignKey("SectionId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Section");
        });

        modelBuilder.Entity("Domain.Entities.EventParticipant", b =>
        {
            b.HasOne("Domain.Entities.Event", "Event")
                .WithMany("EventParticipants")
                .HasForeignKey("EventId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("Domain.Entities.User", "User")
                .WithMany("EventParticipants")
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Event");

            b.Navigation("User");
        });

        modelBuilder.Entity("Domain.Entities.EventSpeaker", b =>
        {
            b.HasOne("Domain.Entities.Event", "Event")
                .WithMany("EventSpeakers")
                .HasForeignKey("EventId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("Domain.Entities.User", "User")
                .WithMany("EventSpeakers")
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Event");

            b.Navigation("User");
        });

        modelBuilder.Entity("Domain.Entities.RefreshToken", b =>
        {
            b.HasOne("Domain.Entities.User", "User")
                .WithMany("RefreshTokens")
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("User");
        });

        modelBuilder.Entity("Domain.Entities.User", b =>
        {
            b.HasOne("Domain.Entities.Image", "AvatarImage")
                .WithOne("User")
                .HasForeignKey("Domain.Entities.User", "AvatarId");

            b.Navigation("AvatarImage");
        });

        modelBuilder.Entity("Domain.Entities.Event", b =>
        {
            b.Navigation("EventParticipants");

            b.Navigation("EventSpeakers");
        });

        modelBuilder.Entity("Domain.Entities.EventCategory", b =>
        {
            b.Navigation("Events");
        });

        modelBuilder.Entity("Domain.Entities.EventSection", b =>
        {
            b.Navigation("Categories");
        });

        modelBuilder.Entity("Domain.Entities.Image", b =>
        {
            b.Navigation("Event");

            b.Navigation("User");
        });

        modelBuilder.Entity("Domain.Entities.User", b =>
        {
            b.Navigation("EventParticipants");

            b.Navigation("EventSpeakers");

            b.Navigation("EventsAsAuthor");

            b.Navigation("RefreshTokens");
        });
#pragma warning restore 612, 618
    }
}