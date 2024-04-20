using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TherapeuticNutrition.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allergens",
                columns: table => new
                {
                    Primarykey = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Reaction = table.Column<string>(type: "text", nullable: false),
                    DangerDegree = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergens", x => x.Primarykey);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteProducts",
                columns: table => new
                {
                    Primarykey = table.Column<Guid>(type: "uuid", nullable: false),
                    Pacient = table.Column<Guid>(type: "uuid", nullable: false),
                    Product = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteProducts", x => x.Primarykey);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteRecipes",
                columns: table => new
                {
                    Primarykey = table.Column<Guid>(type: "uuid", nullable: false),
                    Pacient = table.Column<Guid>(type: "uuid", nullable: false),
                    Recipie = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteRecipes", x => x.Primarykey);
                });

            migrationBuilder.CreateTable(
                name: "PacientAllergens",
                columns: table => new
                {
                    Primarykey = table.Column<Guid>(type: "uuid", nullable: false),
                    Pacient = table.Column<Guid>(type: "uuid", nullable: false),
                    Allergen = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacientAllergens", x => x.Primarykey);
                });

            migrationBuilder.CreateTable(
                name: "Pacients",
                columns: table => new
                {
                    Primarykey = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Fio = table.Column<string>(type: "text", nullable: false),
                    Analysis = table.Column<string>(type: "text", nullable: false),
                    Сonclusion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacients", x => x.Primarykey);
                });

            migrationBuilder.CreateTable(
                name: "ProductAllergens",
                columns: table => new
                {
                    Primarykey = table.Column<Guid>(type: "uuid", nullable: false),
                    Product = table.Column<Guid>(type: "uuid", nullable: false),
                    Allergen = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAllergens", x => x.Primarykey);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Primarykey = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NutritionalValue = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Primarykey);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    Primarykey = table.Column<Guid>(type: "uuid", nullable: false),
                    Recipe = table.Column<Guid>(type: "uuid", nullable: false),
                    Product = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => x.Primarykey);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Primarykey = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Сalories = table.Column<decimal>(type: "numeric", nullable: false),
                    Rating = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Primarykey);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allergens");

            migrationBuilder.DropTable(
                name: "FavoriteProducts");

            migrationBuilder.DropTable(
                name: "FavoriteRecipes");

            migrationBuilder.DropTable(
                name: "PacientAllergens");

            migrationBuilder.DropTable(
                name: "Pacients");

            migrationBuilder.DropTable(
                name: "ProductAllergens");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
