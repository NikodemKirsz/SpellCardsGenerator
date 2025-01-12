using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpellCardsGenerator.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false, collation: "utf16_general_ci"),
                    NameEn = table.Column<string>(type: "varchar(31)", maxLength: 31, nullable: false, collation: "utf16_general_ci"),
                    NameNative = table.Column<string>(type: "varchar(31)", maxLength: 31, nullable: false, collation: "utf16_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                })
                .Annotation("Relational:Collation", "utf16_general_ci");

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Slug = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "utf16_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                })
                .Annotation("Relational:Collation", "utf16_general_ci");

            migrationBuilder.CreateTable(
                name: "SpellLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellLevels", x => x.Id);
                })
                .Annotation("Relational:Collation", "utf16_general_ci");

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(2)", nullable: false, collation: "utf16_general_ci"),
                    RitualLabel = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "utf16_general_ci"),
                    CastingTimeLabel = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "utf16_general_ci"),
                    RangeLabel = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "utf16_general_ci"),
                    ComponentsLabel = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "utf16_general_ci"),
                    DurationLabel = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "utf16_general_ci"),
                    VerbalComponentSymbol = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false, collation: "utf16_general_ci"),
                    SemanticComponentSymbol = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false, collation: "utf16_general_ci"),
                    MaterialComponentSymbol = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false, collation: "utf16_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Templates_Languages_Id",
                        column: x => x.Id,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("Relational:Collation", "utf16_general_ci");

            migrationBuilder.CreateTable(
                name: "SchoolContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false, collation: "utf16_general_ci"),
                    Name = table.Column<string>(type: "varchar(31)", maxLength: 31, nullable: false, collation: "utf16_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolContents", x => new { x.Id, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_SchoolContents_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolContents_Schools_Id",
                        column: x => x.Id,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("Relational:Collation", "utf16_general_ci");

            migrationBuilder.CreateTable(
                name: "SpellLevelContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false, collation: "utf16_general_ci"),
                    Name = table.Column<string>(type: "varchar(31)", maxLength: 31, nullable: false, collation: "utf16_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellLevelContents", x => new { x.Id, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_SpellLevelContents_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpellLevelContents_SpellLevels_Id",
                        column: x => x.Id,
                        principalTable: "SpellLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("Relational:Collation", "utf16_general_ci");

            migrationBuilder.CreateTable(
                name: "Spells",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Slug = table.Column<string>(type: "varchar(63)", maxLength: 63, nullable: false, collation: "utf16_general_ci"),
                    SpellLevelId = table.Column<int>(type: "int", nullable: false),
                    SchoolId = table.Column<int>(type: "int", nullable: false),
                    IsRitual = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    HasVerbal = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    HasSemantic = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    HasMaterial = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spells_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Spells_SpellLevels_SpellLevelId",
                        column: x => x.SpellLevelId,
                        principalTable: "SpellLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("Relational:Collation", "utf16_general_ci");

            migrationBuilder.CreateTable(
                name: "SpellContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false, collation: "utf16_general_ci"),
                    Name = table.Column<string>(type: "varchar(63)", maxLength: 63, nullable: false, collation: "utf16_general_ci"),
                    DescriptionHtml = table.Column<string>(type: "varchar(4095)", maxLength: 4095, nullable: false, collation: "utf16_general_ci"),
                    CastingTime = table.Column<string>(type: "varchar(63)", maxLength: 63, nullable: false, collation: "utf16_general_ci"),
                    Range = table.Column<string>(type: "varchar(63)", maxLength: 63, nullable: false, collation: "utf16_general_ci"),
                    Duration = table.Column<string>(type: "varchar(63)", maxLength: 63, nullable: false, collation: "utf16_general_ci"),
                    MaterialComponents = table.Column<string>(type: "varchar(511)", maxLength: 511, nullable: true, collation: "utf16_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellContents", x => new { x.Id, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_SpellContents_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpellContents_Spells_Id",
                        column: x => x.Id,
                        principalTable: "Spells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("Relational:Collation", "utf16_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolContents_LanguageId",
                table: "SchoolContents",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_SpellContents_LanguageId",
                table: "SpellContents",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_SpellLevelContents_LanguageId",
                table: "SpellLevelContents",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Spells_SchoolId",
                table: "Spells",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Spells_SpellLevelId",
                table: "Spells",
                column: "SpellLevelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchoolContents");

            migrationBuilder.DropTable(
                name: "SpellContents");

            migrationBuilder.DropTable(
                name: "SpellLevelContents");

            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropTable(
                name: "Spells");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Schools");

            migrationBuilder.DropTable(
                name: "SpellLevels");
        }
    }
}
