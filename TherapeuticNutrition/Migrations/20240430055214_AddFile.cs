using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TherapeuticNutrition.Migrations
{
    /// <inheritdoc />
    public partial class AddFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Recipie",
                table: "FavoriteRecipes",
                newName: "Recipe");

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Primarykey = table.Column<Guid>(type: "uuid", nullable: false),
                    Relation = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Primarykey);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.RenameColumn(
                name: "Recipe",
                table: "FavoriteRecipes",
                newName: "Recipie");
        }
    }
}
