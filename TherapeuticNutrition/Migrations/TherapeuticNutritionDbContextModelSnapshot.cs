﻿// <auto-generated />
using System;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TherapeuticNutrition.Migrations
{
    [DbContext(typeof(TherapeuticNutritionDbContext))]
    partial class TherapeuticNutritionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Infrastructure.DataAccess.Entities.Allergen", b =>
                {
                    b.Property<Guid>("Primarykey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("DangerDegree")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Reaction")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Primarykey");

                    b.ToTable("Allergens");
                });

            modelBuilder.Entity("Infrastructure.DataAccess.Entities.Pacient", b =>
                {
                    b.Property<Guid>("Primarykey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Analysis")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Fio")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Сonclusion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Primarykey");

                    b.ToTable("Pacients");
                });

            modelBuilder.Entity("Infrastructure.DataAccess.Entities.Product", b =>
                {
                    b.Property<Guid>("Primarykey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NutritionalValue")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Primarykey");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Infrastructure.DataAccess.Entities.Recipe", b =>
                {
                    b.Property<Guid>("Primarykey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Rating")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Сalories")
                        .HasColumnType("numeric");

                    b.HasKey("Primarykey");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("Infrastructure.DataAccess.Entities.Relations.FavoriteProduct", b =>
                {
                    b.Property<Guid>("Primarykey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Pacient")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Product")
                        .HasColumnType("uuid");

                    b.HasKey("Primarykey");

                    b.ToTable("FavoriteProducts");
                });

            modelBuilder.Entity("Infrastructure.DataAccess.Entities.Relations.FavoriteRecipe", b =>
                {
                    b.Property<Guid>("Primarykey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Pacient")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Recipie")
                        .HasColumnType("uuid");

                    b.HasKey("Primarykey");

                    b.ToTable("FavoriteRecipes");
                });

            modelBuilder.Entity("Infrastructure.DataAccess.Entities.Relations.PacientAllergen", b =>
                {
                    b.Property<Guid>("Primarykey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Allergen")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Pacient")
                        .HasColumnType("uuid");

                    b.HasKey("Primarykey");

                    b.ToTable("PacientAllergens");
                });

            modelBuilder.Entity("Infrastructure.DataAccess.Entities.Relations.ProductAllergen", b =>
                {
                    b.Property<Guid>("Primarykey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Allergen")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Product")
                        .HasColumnType("uuid");

                    b.HasKey("Primarykey");

                    b.ToTable("ProductAllergens");
                });

            modelBuilder.Entity("Infrastructure.DataAccess.Entities.Relations.RecipeIngredient", b =>
                {
                    b.Property<Guid>("Primarykey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Product")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Recipe")
                        .HasColumnType("uuid");

                    b.HasKey("Primarykey");

                    b.ToTable("RecipeIngredients");
                });
#pragma warning restore 612, 618
        }
    }
}
