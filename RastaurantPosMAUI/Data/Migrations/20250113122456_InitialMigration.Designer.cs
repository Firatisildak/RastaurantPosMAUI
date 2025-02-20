﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RastaurantPosMAUI.Data;

#nullable disable

namespace RastaurantPosMAUI.Data.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20250113122456_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RastaurantPosMAUI.Data.Entities.MenuCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("MenuCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Icon = "drink.png",
                            Name = "Beverages"
                        },
                        new
                        {
                            Id = 2,
                            Icon = "meal.png",
                            Name = "Main Course"
                        },
                        new
                        {
                            Id = 3,
                            Icon = "snacks.png",
                            Name = "Snacks"
                        },
                        new
                        {
                            Id = 4,
                            Icon = "cake.png",
                            Name = "Desserts"
                        },
                        new
                        {
                            Id = 5,
                            Icon = "fast_food.png",
                            Name = "Fast Food"
                        });
                });

            modelBuilder.Entity("RastaurantPosMAUI.Data.Entities.MenuItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("MenuItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Chilled beer",
                            Icon = "beer.png",
                            Name = "Beer",
                            Price = 4.99m
                        },
                        new
                        {
                            Id = 2,
                            Description = "Spicy chicken biryani",
                            Icon = "biryani.png",
                            Name = "Biryani",
                            Price = 7.99m
                        },
                        new
                        {
                            Id = 3,
                            Description = "Freshly baked buns",
                            Icon = "buns.png",
                            Name = "Buns",
                            Price = 2.99m
                        },
                        new
                        {
                            Id = 4,
                            Description = "Burger with fries",
                            Icon = "burger_fries_combo.png",
                            Name = "Burger and Fries Combo",
                            Price = 5.99m
                        },
                        new
                        {
                            Id = 5,
                            Description = "Delicious chocolate cake",
                            Icon = "cake.png",
                            Name = "Cake",
                            Price = 3.99m
                        },
                        new
                        {
                            Id = 6,
                            Description = "Rich chocolate bar",
                            Icon = "chocolate.png",
                            Name = "Chocolate",
                            Price = 1.99m
                        },
                        new
                        {
                            Id = 7,
                            Description = "Refreshing cocktail",
                            Icon = "cocktail.png",
                            Name = "Cocktail",
                            Price = 6.99m
                        },
                        new
                        {
                            Id = 8,
                            Description = "Hot coffee",
                            Icon = "coffee.png",
                            Name = "Coffee",
                            Price = 1.99m
                        },
                        new
                        {
                            Id = 9,
                            Description = "Sweet cupcake",
                            Icon = "cupcake.png",
                            Name = "Cupcake",
                            Price = 2.49m
                        },
                        new
                        {
                            Id = 10,
                            Description = "Glazed donut",
                            Icon = "donut.png",
                            Name = "Donut",
                            Price = 1.49m
                        },
                        new
                        {
                            Id = 11,
                            Description = "Energy drink",
                            Icon = "energy_drink.png",
                            Name = "Energy Drink",
                            Price = 2.99m
                        },
                        new
                        {
                            Id = 12,
                            Description = "Quick and tasty fast food",
                            Icon = "fast_food.png",
                            Name = "Fast Food",
                            Price = 5.99m
                        },
                        new
                        {
                            Id = 13,
                            Description = "Crispy fish and chips",
                            Icon = "fish_and_chips.png",
                            Name = "Fish and Chips",
                            Price = 6.99m
                        },
                        new
                        {
                            Id = 14,
                            Description = "Grilled fish",
                            Icon = "fish.png",
                            Name = "Fish",
                            Price = 7.99m
                        },
                        new
                        {
                            Id = 15,
                            Description = "Crispy french fries",
                            Icon = "french_fries.png",
                            Name = "French Fries",
                            Price = 2.99m
                        },
                        new
                        {
                            Id = 16,
                            Description = "Crispy fried chicken",
                            Icon = "fried_chicken.png",
                            Name = "Fried Chicken",
                            Price = 5.99m
                        },
                        new
                        {
                            Id = 17,
                            Description = "Sunny_side_up fried egg",
                            Icon = "fried_egg.png",
                            Name = "Fried Egg",
                            Price = 1.49m
                        },
                        new
                        {
                            Id = 18,
                            Description = "Savory fried rice",
                            Icon = "fried_rice.png",
                            Name = "Fried Rice",
                            Price = 4.99m
                        },
                        new
                        {
                            Id = 19,
                            Description = "Juicy hamburger",
                            Icon = "hamburger.png",
                            Name = "Hamburger",
                            Price = 4.99m
                        },
                        new
                        {
                            Id = 20,
                            Description = "Grilled hotdog",
                            Icon = "hotdog.png",
                            Name = "Hotdog",
                            Price = 3.49m
                        },
                        new
                        {
                            Id = 21,
                            Description = "Cold ice cream",
                            Icon = "ice_cream.png",
                            Name = "Ice Cream",
                            Price = 2.99m
                        },
                        new
                        {
                            Id = 22,
                            Description = "South Indian idli platter",
                            Icon = "idli_platter.png",
                            Name = "Idli Platter",
                            Price = 3.99m
                        },
                        new
                        {
                            Id = 23,
                            Description = "Grilled kebab",
                            Icon = "kebab.png",
                            Name = "Kebab",
                            Price = 5.99m
                        },
                        new
                        {
                            Id = 24,
                            Description = "Spicy kimchi stew",
                            Icon = "kimchi_jjiage.png",
                            Name = "Kimchi Jjigae",
                            Price = 6.99m
                        },
                        new
                        {
                            Id = 25,
                            Description = "Sweet laddu",
                            Icon = "laddu.png",
                            Name = "Laddu",
                            Price = 1.99m
                        },
                        new
                        {
                            Id = 26,
                            Description = "Fresh lobster",
                            Icon = "lobster.png",
                            Name = "Lobster",
                            Price = 15.99m
                        },
                        new
                        {
                            Id = 27,
                            Description = "Ripe mango",
                            Icon = "mango.png",
                            Name = "Mango",
                            Price = 1.49m
                        },
                        new
                        {
                            Id = 28,
                            Description = "Crispy masala dosa",
                            Icon = "masala_dosa.png",
                            Name = "Masala Dosa",
                            Price = 4.99m
                        },
                        new
                        {
                            Id = 29,
                            Description = "Complete meal",
                            Icon = "meal.png",
                            Name = "Meal",
                            Price = 9.99m
                        },
                        new
                        {
                            Id = 30,
                            Description = "Cheesy nachos",
                            Icon = "nachos.png",
                            Name = "Nachos",
                            Price = 3.99m
                        },
                        new
                        {
                            Id = 31,
                            Description = "Stir_fried noodles",
                            Icon = "noodles.png",
                            Name = "Noodles",
                            Price = 4.99m
                        },
                        new
                        {
                            Id = 32,
                            Description = "Fresh orange juice",
                            Icon = "orange_juice.png",
                            Name = "Orange Juice",
                            Price = 2.49m
                        },
                        new
                        {
                            Id = 33,
                            Description = "Fluffy pancakes",
                            Icon = "pancakes.png",
                            Name = "Pancakes",
                            Price = 3.99m
                        },
                        new
                        {
                            Id = 34,
                            Description = "Paneer curry",
                            Icon = "paneer.png",
                            Name = "Paneer",
                            Price = 4.99m
                        },
                        new
                        {
                            Id = 35,
                            Description = "Italian pasta",
                            Icon = "pasta.png",
                            Name = "Pasta",
                            Price = 5.99m
                        },
                        new
                        {
                            Id = 36,
                            Description = "Fruit pie",
                            Icon = "pie.png",
                            Name = "Pie",
                            Price = 3.99m
                        },
                        new
                        {
                            Id = 37,
                            Description = "Slice of pizza",
                            Icon = "pizza_slice.png",
                            Name = "Pizza Slice",
                            Price = 2.99m
                        },
                        new
                        {
                            Id = 38,
                            Description = "Whole pizza",
                            Icon = "pizza.png",
                            Name = "Pizza",
                            Price = 8.99m
                        },
                        new
                        {
                            Id = 39,
                            Description = "Japanese ramen",
                            Icon = "ramen.png",
                            Name = "Ramen",
                            Price = 6.99m
                        },
                        new
                        {
                            Id = 40,
                            Description = "Steamed rice",
                            Icon = "rice.png",
                            Name = "Rice",
                            Price = 1.99m
                        },
                        new
                        {
                            Id = 41,
                            Description = "Roasted chicken",
                            Icon = "roasted_chicken.png",
                            Name = "Roasted Chicken",
                            Price = 7.99m
                        },
                        new
                        {
                            Id = 42,
                            Description = "Fresh salad bowl",
                            Icon = "salad_bowl.png",
                            Name = "Salad Bowl",
                            Price = 4.99m
                        },
                        new
                        {
                            Id = 43,
                            Description = "Fresh salad plate",
                            Icon = "salad_plate.png",
                            Name = "Salad Plate",
                            Price = 4.99m
                        },
                        new
                        {
                            Id = 44,
                            Description = "Crispy samosa",
                            Icon = "samosa.png",
                            Name = "Samosa",
                            Price = 1.99m
                        },
                        new
                        {
                            Id = 45,
                            Description = "Tasty sandwich",
                            Icon = "sandwich.png",
                            Name = "Sandwich",
                            Price = 3.99m
                        },
                        new
                        {
                            Id = 46,
                            Description = "Various snacks",
                            Icon = "snacks.png",
                            Name = "Snacks",
                            Price = 2.99m
                        },
                        new
                        {
                            Id = 47,
                            Description = "Refreshing soda",
                            Icon = "soda.png",
                            Name = "Soda",
                            Price = 1.49m
                        },
                        new
                        {
                            Id = 48,
                            Description = "Cold soft drink",
                            Icon = "soft_drink.png",
                            Name = "Soft Drink",
                            Price = 1.99m
                        },
                        new
                        {
                            Id = 49,
                            Description = "Korean soju",
                            Icon = "soju.png",
                            Name = "Soju",
                            Price = 3.99m
                        },
                        new
                        {
                            Id = 50,
                            Description = "Italian spaghetti",
                            Icon = "spaghetti.png",
                            Name = "Spaghetti",
                            Price = 5.99m
                        },
                        new
                        {
                            Id = 51,
                            Description = "Assorted sushi",
                            Icon = "sushi.png",
                            Name = "Sushi",
                            Price = 8.99m
                        },
                        new
                        {
                            Id = 52,
                            Description = "Mexican taco",
                            Icon = "taco.png",
                            Name = "Taco",
                            Price = 3.99m
                        },
                        new
                        {
                            Id = 53,
                            Description = "Spicy Thai food",
                            Icon = "thai_food.png",
                            Name = "Thai Food",
                            Price = 6.99m
                        },
                        new
                        {
                            Id = 54,
                            Description = "Indian thali",
                            Icon = "thali.png",
                            Name = "Thali",
                            Price = 7.99m
                        },
                        new
                        {
                            Id = 55,
                            Description = "Healthy wrap",
                            Icon = "wrap.png",
                            Name = "Wrap",
                            Price = 4.99m
                        });
                });

            modelBuilder.Entity("RastaurantPosMAUI.Data.Entities.MenuItemCategoryMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MenuCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("MenuItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MenuCategoryId");

                    b.HasIndex("MenuItemId");

                    b.ToTable("MenuItemCategoriesMapping");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MenuCategoryId = 1,
                            MenuItemId = 1
                        },
                        new
                        {
                            Id = 2,
                            MenuCategoryId = 1,
                            MenuItemId = 6
                        },
                        new
                        {
                            Id = 3,
                            MenuCategoryId = 1,
                            MenuItemId = 7
                        },
                        new
                        {
                            Id = 4,
                            MenuCategoryId = 1,
                            MenuItemId = 8
                        },
                        new
                        {
                            Id = 5,
                            MenuCategoryId = 1,
                            MenuItemId = 10
                        },
                        new
                        {
                            Id = 6,
                            MenuCategoryId = 1,
                            MenuItemId = 11
                        },
                        new
                        {
                            Id = 7,
                            MenuCategoryId = 1,
                            MenuItemId = 32
                        },
                        new
                        {
                            Id = 8,
                            MenuCategoryId = 1,
                            MenuItemId = 47
                        },
                        new
                        {
                            Id = 9,
                            MenuCategoryId = 1,
                            MenuItemId = 48
                        },
                        new
                        {
                            Id = 10,
                            MenuCategoryId = 1,
                            MenuItemId = 49
                        },
                        new
                        {
                            Id = 11,
                            MenuCategoryId = 2,
                            MenuItemId = 2
                        },
                        new
                        {
                            Id = 12,
                            MenuCategoryId = 2,
                            MenuItemId = 13
                        },
                        new
                        {
                            Id = 13,
                            MenuCategoryId = 2,
                            MenuItemId = 14
                        },
                        new
                        {
                            Id = 14,
                            MenuCategoryId = 2,
                            MenuItemId = 18
                        },
                        new
                        {
                            Id = 15,
                            MenuCategoryId = 2,
                            MenuItemId = 20
                        },
                        new
                        {
                            Id = 16,
                            MenuCategoryId = 2,
                            MenuItemId = 22
                        },
                        new
                        {
                            Id = 17,
                            MenuCategoryId = 2,
                            MenuItemId = 23
                        },
                        new
                        {
                            Id = 18,
                            MenuCategoryId = 2,
                            MenuItemId = 24
                        },
                        new
                        {
                            Id = 19,
                            MenuCategoryId = 2,
                            MenuItemId = 26
                        },
                        new
                        {
                            Id = 20,
                            MenuCategoryId = 2,
                            MenuItemId = 28
                        },
                        new
                        {
                            Id = 21,
                            MenuCategoryId = 2,
                            MenuItemId = 29
                        },
                        new
                        {
                            Id = 22,
                            MenuCategoryId = 2,
                            MenuItemId = 31
                        },
                        new
                        {
                            Id = 23,
                            MenuCategoryId = 2,
                            MenuItemId = 34
                        },
                        new
                        {
                            Id = 24,
                            MenuCategoryId = 2,
                            MenuItemId = 35
                        },
                        new
                        {
                            Id = 25,
                            MenuCategoryId = 2,
                            MenuItemId = 38
                        },
                        new
                        {
                            Id = 26,
                            MenuCategoryId = 2,
                            MenuItemId = 39
                        },
                        new
                        {
                            Id = 27,
                            MenuCategoryId = 2,
                            MenuItemId = 40
                        },
                        new
                        {
                            Id = 28,
                            MenuCategoryId = 2,
                            MenuItemId = 41
                        },
                        new
                        {
                            Id = 29,
                            MenuCategoryId = 2,
                            MenuItemId = 45
                        },
                        new
                        {
                            Id = 30,
                            MenuCategoryId = 2,
                            MenuItemId = 50
                        },
                        new
                        {
                            Id = 31,
                            MenuCategoryId = 2,
                            MenuItemId = 51
                        },
                        new
                        {
                            Id = 32,
                            MenuCategoryId = 2,
                            MenuItemId = 52
                        },
                        new
                        {
                            Id = 33,
                            MenuCategoryId = 2,
                            MenuItemId = 53
                        },
                        new
                        {
                            Id = 34,
                            MenuCategoryId = 2,
                            MenuItemId = 54
                        },
                        new
                        {
                            Id = 35,
                            MenuCategoryId = 2,
                            MenuItemId = 55
                        },
                        new
                        {
                            Id = 36,
                            MenuCategoryId = 3,
                            MenuItemId = 3
                        },
                        new
                        {
                            Id = 37,
                            MenuCategoryId = 3,
                            MenuItemId = 15
                        },
                        new
                        {
                            Id = 38,
                            MenuCategoryId = 3,
                            MenuItemId = 16
                        },
                        new
                        {
                            Id = 39,
                            MenuCategoryId = 3,
                            MenuItemId = 17
                        },
                        new
                        {
                            Id = 40,
                            MenuCategoryId = 3,
                            MenuItemId = 19
                        },
                        new
                        {
                            Id = 41,
                            MenuCategoryId = 3,
                            MenuItemId = 30
                        },
                        new
                        {
                            Id = 42,
                            MenuCategoryId = 4,
                            MenuItemId = 5
                        },
                        new
                        {
                            Id = 43,
                            MenuCategoryId = 4,
                            MenuItemId = 8
                        },
                        new
                        {
                            Id = 44,
                            MenuCategoryId = 4,
                            MenuItemId = 9
                        },
                        new
                        {
                            Id = 45,
                            MenuCategoryId = 4,
                            MenuItemId = 21
                        },
                        new
                        {
                            Id = 46,
                            MenuCategoryId = 4,
                            MenuItemId = 25
                        },
                        new
                        {
                            Id = 47,
                            MenuCategoryId = 4,
                            MenuItemId = 27
                        },
                        new
                        {
                            Id = 48,
                            MenuCategoryId = 4,
                            MenuItemId = 33
                        },
                        new
                        {
                            Id = 49,
                            MenuCategoryId = 4,
                            MenuItemId = 36
                        },
                        new
                        {
                            Id = 50,
                            MenuCategoryId = 5,
                            MenuItemId = 4
                        },
                        new
                        {
                            Id = 51,
                            MenuCategoryId = 5,
                            MenuItemId = 12
                        },
                        new
                        {
                            Id = 52,
                            MenuCategoryId = 5,
                            MenuItemId = 37
                        },
                        new
                        {
                            Id = 53,
                            MenuCategoryId = 5,
                            MenuItemId = 38
                        },
                        new
                        {
                            Id = 54,
                            MenuCategoryId = 5,
                            MenuItemId = 45
                        },
                        new
                        {
                            Id = 55,
                            MenuCategoryId = 5,
                            MenuItemId = 46
                        });
                });

            modelBuilder.Entity("RastaurantPosMAUI.Data.Entities.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("TotalAmountPaid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TotalItemsCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("RastaurantPosMAUI.Data.Entities.OrderItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("RastaurantPosMAUI.Data.Entities.MenuItemCategoryMapping", b =>
                {
                    b.HasOne("RastaurantPosMAUI.Data.Entities.MenuCategory", null)
                        .WithMany("MenuItemCategoriesMapping")
                        .HasForeignKey("MenuCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RastaurantPosMAUI.Data.Entities.MenuItem", null)
                        .WithMany("MenuItemCategoriesMapping")
                        .HasForeignKey("MenuItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RastaurantPosMAUI.Data.Entities.MenuCategory", b =>
                {
                    b.Navigation("MenuItemCategoriesMapping");
                });

            modelBuilder.Entity("RastaurantPosMAUI.Data.Entities.MenuItem", b =>
                {
                    b.Navigation("MenuItemCategoriesMapping");
                });
#pragma warning restore 612, 618
        }
    }
}
