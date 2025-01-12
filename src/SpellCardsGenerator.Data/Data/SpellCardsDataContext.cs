using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySqlConnector;
using SpellCardsGenerator.Data.Configuration;
using SpellCardsGenerator.Data.Entities;

namespace SpellCardsGenerator.Data.Data;

public sealed class SpellCardsDataContext : DbContext
{
  private static readonly MySqlServerVersion MySqlServerVersion = new(new Version(8, 3));

  private readonly IOptions<Config.Main> _mainConfig;

  public DbSet<Language> Languages { get; set; } = null!;
  public DbSet<SchoolData> Schools { get; set; } = null!;
  public DbSet<SchoolContent> SchoolContents { get; set; } = null!;
  public DbSet<SpellData> Spells { get; set; } = null!;
  public DbSet<SpellContent> SpellContents { get; set; } = null!;
  public DbSet<SpellLevelData> SpellLevels { get; set; } = null!;
  public DbSet<SpellLevelContent> SpellLevelContents { get; set; } = null!;
  public DbSet<TemplateContent> Templates { get; set; } = null!;

  public SpellCardsDataContext(DbContextOptions<SpellCardsDataContext> options, IOptions<Config.Main> mainConfig)
    : base(options)
  {
    _mainConfig = mainConfig;
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    Config.Main config = _mainConfig.Value;
    MySqlConnectionStringBuilder connectionStringBuilder = new()
    {
      Server = config.Eredan.Host,
      UserID = config.Eredan.UserId,
      Password = config.Eredan.Password,
      Database = nameof(config.Eredan),
      ApplicationName = nameof(SpellCardsGenerator),
    };
    string connectionString = connectionStringBuilder.ToString();

    optionsBuilder.UseMySql(connectionString, MySqlServerVersion, static builder =>
    {
      builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
    });

    base.OnConfiguring(optionsBuilder);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.UseCollation("utf16_general_ci", DelegationModes.ApplyToAll);

    modelBuilder.Entity<Language>(static entity =>
    {
      entity.HasKey(static language => language.Id);
      entity
        .HasOne<TemplateContent>(static language => language.Template)
        .WithOne(static templateContent => templateContent.Language)
        .HasForeignKey<TemplateContent>(static language => language.Id);
    });

    modelBuilder.Entity<TemplateContent>(static entity =>
    {
      entity.HasKey(static templateContent => templateContent.Id);
    });

    modelBuilder.Entity<SchoolData>(static entity =>
    {
      entity.HasKey(static schoolData => schoolData.Id);
      entity
        .HasIndex(static schoolData => schoolData.Slug)
        .IsUnique();
      entity
        .HasMany<SchoolContent>(static schoolData => schoolData.Contents)
        .WithOne(static schoolContent => schoolContent.Data)
        .HasForeignKey(static schoolContent => schoolContent.Id);
    });
    modelBuilder.Entity<SchoolContent>(static entity =>
    {
      entity.HasKey(static schoolContent => new { schoolContent.Id, schoolContent.LanguageId });
      entity.HasOne<Language>(static schoolContent => schoolContent.Language);
    });

    modelBuilder.Entity<SpellData>(static entity =>
    {
      entity.HasKey(static spellData => spellData.Id);
      entity
        .HasIndex(static spellData => spellData.Slug)
        .IsUnique();
      entity.HasOne<SchoolData>(static spellData => spellData.School);
      entity.HasOne<SpellLevelData>(static spellData => spellData.SpellLevel);
      entity
        .HasMany<SpellContent>(static spellData => spellData.Contents)
        .WithOne(static spellContent => spellContent.Data)
        .HasForeignKey(static spellContent => spellContent.Id);
    });
    modelBuilder.Entity<SpellContent>(static entity =>
    {
      entity.HasKey(static spellContent => new { spellContent.Id, spellContent.LanguageId });
      entity.HasOne<Language>(static spellContent => spellContent.Language);
    });

    modelBuilder.Entity<SpellLevelData>(static entity =>
    {
      entity.HasKey(static spellLevel => spellLevel.Id);
      entity
        .HasMany<SpellLevelContent>(static spellLevel => spellLevel.Contents)
        .WithOne(static spellLevelContent => spellLevelContent.Data)
        .HasForeignKey(static spellLevelContent => spellLevelContent.Id);
    });
    modelBuilder.Entity<SpellLevelContent>(static entity =>
    {
      entity.HasKey(static spellLevelContent => new { spellLevelContent.Id, spellLevelContent.LanguageId });
      entity.HasOne<Language>(static spellLevelContent => spellLevelContent.Language);
    });

    base.OnModelCreating(modelBuilder);
  }
}