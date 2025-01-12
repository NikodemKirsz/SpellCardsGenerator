﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpellCardsGenerator.Data.Data;

#nullable disable

namespace SpellCardsGenerator.Data.Migrations
{
    [DbContext(typeof(SpellCardsDataContext))]
    [Migration("20240407194324_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf16_general_ci")
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("SpellCardsGenerator.Data.Entities.Language", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("varchar(31)");

                    b.Property<string>("NameNative")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("varchar(31)");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("SpellCardsGenerator.Data.Entities.SchoolContent", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("LanguageId")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("varchar(31)");

                    b.HasKey("Id", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.ToTable("SchoolContents");
                });

            modelBuilder.Entity("SpellCardsGenerator.Data.Entities.SchoolData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.HasKey("Id");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("SpellCardsGenerator.Data.Entities.SpellContent", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("LanguageId")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<string>("CastingTime")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("varchar(63)");

                    b.Property<string>("DescriptionHtml")
                        .IsRequired()
                        .HasMaxLength(4095)
                        .HasColumnType("varchar(4095)");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("varchar(63)");

                    b.Property<string>("MaterialComponents")
                        .HasMaxLength(511)
                        .HasColumnType("varchar(511)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("varchar(63)");

                    b.Property<string>("Range")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("varchar(63)");

                    b.HasKey("Id", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.ToTable("SpellContents");
                });

            modelBuilder.Entity("SpellCardsGenerator.Data.Entities.SpellData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("HasMaterial")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("HasSemantic")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("HasVerbal")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsRitual")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("SchoolId")
                        .HasColumnType("int");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("varchar(31)");

                    b.Property<int>("SpellLevelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId");

                    b.HasIndex("SpellLevelId");

                    b.ToTable("Spells");
                });

            modelBuilder.Entity("SpellCardsGenerator.Data.Entities.SpellLevelContent", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("LanguageId")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("varchar(31)");

                    b.HasKey("Id", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.ToTable("SpellLevelContents");
                });

            modelBuilder.Entity("SpellCardsGenerator.Data.Entities.SpellLevelData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SpellLevels");
                });

            modelBuilder.Entity("SpellCardsGenerator.Data.Entities.TemplateContent", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(2)");

                    b.Property<string>("CastingTimeLabel")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("ComponentsLabel")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("DurationLabel")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("MaterialComponentSymbol")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("varchar(1)");

                    b.Property<string>("RangeLabel")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("RitualLabel")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("SemanticComponentSymbol")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("varchar(1)");

                    b.Property<string>("VerbalComponentSymbol")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("varchar(1)");

                    b.HasKey("Id");

                    b.ToTable("Templates");
                });

            modelBuilder.Entity("SpellCardsGenerator.Data.Entities.SchoolContent", b =>
                {
                    b.HasOne("SpellCardsGenerator.Data.Entities.SchoolData", "Data")
                        .WithMany("Contents")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpellCardsGenerator.Data.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Data");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("SpellCardsGenerator.Data.Entities.SpellContent", b =>
                {
                    b.HasOne("SpellCardsGenerator.Data.Entities.SpellData", "Data")
                        .WithMany("Contents")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpellCardsGenerator.Data.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Data");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("SpellCardsGenerator.Data.Entities.SpellData", b =>
                {
                    b.HasOne("SpellCardsGenerator.Data.Entities.SchoolData", "School")
                        .WithMany()
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpellCardsGenerator.Data.Entities.SpellLevelData", "SpellLevel")
                        .WithMany()
                        .HasForeignKey("SpellLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("School");

                    b.Navigation("SpellLevel");
                });

            modelBuilder.Entity("SpellCardsGenerator.Data.Entities.SpellLevelContent", b =>
                {
                    b.HasOne("SpellCardsGenerator.Data.Entities.SpellLevelData", "Data")
                        .WithMany("Contents")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpellCardsGenerator.Data.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Data");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("SpellCardsGenerator.Data.Entities.TemplateContent", b =>
                {
                    b.HasOne("SpellCardsGenerator.Data.Entities.Language", "Language")
                        .WithOne("Template")
                        .HasForeignKey("SpellCardsGenerator.Data.Entities.TemplateContent", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("SpellCardsGenerator.Data.Entities.Language", b =>
                {
                    b.Navigation("Template");
                });

            modelBuilder.Entity("SpellCardsGenerator.Data.Entities.SchoolData", b =>
                {
                    b.Navigation("Contents");
                });

            modelBuilder.Entity("SpellCardsGenerator.Data.Entities.SpellData", b =>
                {
                    b.Navigation("Contents");
                });

            modelBuilder.Entity("SpellCardsGenerator.Data.Entities.SpellLevelData", b =>
                {
                    b.Navigation("Contents");
                });
#pragma warning restore 612, 618
        }
    }
}