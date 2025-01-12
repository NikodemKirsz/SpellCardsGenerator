using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpellCardsGenerator.Data.Migrations
{
    /// <inheritdoc />
    public partial class HigherLevelsClause : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HigherLevelsLabel",
                table: "Templates",
                type: "varchar(31)",
                maxLength: 31,
                nullable: false,
                defaultValue: "",
                collation: "utf16_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "HigherLevelsDescription",
                table: "SpellContents",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                collation: "utf16_general_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HigherLevelsLabel",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "HigherLevelsDescription",
                table: "SpellContents");
        }
    }
}
