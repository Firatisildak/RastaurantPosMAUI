using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RastaurantPosMAUI.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalItemsCount = table.Column<int>(type: "int", nullable: false),
                    TotalAmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuItemCategoriesMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuCategoryId = table.Column<int>(type: "int", nullable: false),
                    MenuItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemCategoriesMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItemCategoriesMapping_MenuCategories_MenuCategoryId",
                        column: x => x.MenuCategoryId,
                        principalTable: "MenuCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuItemCategoriesMapping_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MenuCategories",
                columns: new[] { "Id", "Icon", "Name" },
                values: new object[,]
                {
                    { 1, "drink.png", "Beverages" },
                    { 2, "meal.png", "Main Course" },
                    { 3, "snacks.png", "Snacks" },
                    { 4, "cake.png", "Desserts" },
                    { 5, "fast_food.png", "Fast Food" }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Description", "Icon", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Chilled beer", "beer.png", "Beer", 4.99m },
                    { 2, "Spicy chicken biryani", "biryani.png", "Biryani", 7.99m },
                    { 3, "Freshly baked buns", "buns.png", "Buns", 2.99m },
                    { 4, "Burger with fries", "burger_fries_combo.png", "Burger and Fries Combo", 5.99m },
                    { 5, "Delicious chocolate cake", "cake.png", "Cake", 3.99m },
                    { 6, "Rich chocolate bar", "chocolate.png", "Chocolate", 1.99m },
                    { 7, "Refreshing cocktail", "cocktail.png", "Cocktail", 6.99m },
                    { 8, "Hot coffee", "coffee.png", "Coffee", 1.99m },
                    { 9, "Sweet cupcake", "cupcake.png", "Cupcake", 2.49m },
                    { 10, "Glazed donut", "donut.png", "Donut", 1.49m },
                    { 11, "Energy drink", "energy_drink.png", "Energy Drink", 2.99m },
                    { 12, "Quick and tasty fast food", "fast_food.png", "Fast Food", 5.99m },
                    { 13, "Crispy fish and chips", "fish_and_chips.png", "Fish and Chips", 6.99m },
                    { 14, "Grilled fish", "fish.png", "Fish", 7.99m },
                    { 15, "Crispy french fries", "french_fries.png", "French Fries", 2.99m },
                    { 16, "Crispy fried chicken", "fried_chicken.png", "Fried Chicken", 5.99m },
                    { 17, "Sunny_side_up fried egg", "fried_egg.png", "Fried Egg", 1.49m },
                    { 18, "Savory fried rice", "fried_rice.png", "Fried Rice", 4.99m },
                    { 19, "Juicy hamburger", "hamburger.png", "Hamburger", 4.99m },
                    { 20, "Grilled hotdog", "hotdog.png", "Hotdog", 3.49m },
                    { 21, "Cold ice cream", "ice_cream.png", "Ice Cream", 2.99m },
                    { 22, "South Indian idli platter", "idli_platter.png", "Idli Platter", 3.99m },
                    { 23, "Grilled kebab", "kebab.png", "Kebab", 5.99m },
                    { 24, "Spicy kimchi stew", "kimchi_jjiage.png", "Kimchi Jjigae", 6.99m },
                    { 25, "Sweet laddu", "laddu.png", "Laddu", 1.99m },
                    { 26, "Fresh lobster", "lobster.png", "Lobster", 15.99m },
                    { 27, "Ripe mango", "mango.png", "Mango", 1.49m },
                    { 28, "Crispy masala dosa", "masala_dosa.png", "Masala Dosa", 4.99m },
                    { 29, "Complete meal", "meal.png", "Meal", 9.99m },
                    { 30, "Cheesy nachos", "nachos.png", "Nachos", 3.99m },
                    { 31, "Stir_fried noodles", "noodles.png", "Noodles", 4.99m },
                    { 32, "Fresh orange juice", "orange_juice.png", "Orange Juice", 2.49m },
                    { 33, "Fluffy pancakes", "pancakes.png", "Pancakes", 3.99m },
                    { 34, "Paneer curry", "paneer.png", "Paneer", 4.99m },
                    { 35, "Italian pasta", "pasta.png", "Pasta", 5.99m },
                    { 36, "Fruit pie", "pie.png", "Pie", 3.99m },
                    { 37, "Slice of pizza", "pizza_slice.png", "Pizza Slice", 2.99m },
                    { 38, "Whole pizza", "pizza.png", "Pizza", 8.99m },
                    { 39, "Japanese ramen", "ramen.png", "Ramen", 6.99m },
                    { 40, "Steamed rice", "rice.png", "Rice", 1.99m },
                    { 41, "Roasted chicken", "roasted_chicken.png", "Roasted Chicken", 7.99m },
                    { 42, "Fresh salad bowl", "salad_bowl.png", "Salad Bowl", 4.99m },
                    { 43, "Fresh salad plate", "salad_plate.png", "Salad Plate", 4.99m },
                    { 44, "Crispy samosa", "samosa.png", "Samosa", 1.99m },
                    { 45, "Tasty sandwich", "sandwich.png", "Sandwich", 3.99m },
                    { 46, "Various snacks", "snacks.png", "Snacks", 2.99m },
                    { 47, "Refreshing soda", "soda.png", "Soda", 1.49m },
                    { 48, "Cold soft drink", "soft_drink.png", "Soft Drink", 1.99m },
                    { 49, "Korean soju", "soju.png", "Soju", 3.99m },
                    { 50, "Italian spaghetti", "spaghetti.png", "Spaghetti", 5.99m },
                    { 51, "Assorted sushi", "sushi.png", "Sushi", 8.99m },
                    { 52, "Mexican taco", "taco.png", "Taco", 3.99m },
                    { 53, "Spicy Thai food", "thai_food.png", "Thai Food", 6.99m },
                    { 54, "Indian thali", "thali.png", "Thali", 7.99m },
                    { 55, "Healthy wrap", "wrap.png", "Wrap", 4.99m }
                });

            migrationBuilder.InsertData(
                table: "MenuItemCategoriesMapping",
                columns: new[] { "Id", "MenuCategoryId", "MenuItemId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 6 },
                    { 3, 1, 7 },
                    { 4, 1, 8 },
                    { 5, 1, 10 },
                    { 6, 1, 11 },
                    { 7, 1, 32 },
                    { 8, 1, 47 },
                    { 9, 1, 48 },
                    { 10, 1, 49 },
                    { 11, 2, 2 },
                    { 12, 2, 13 },
                    { 13, 2, 14 },
                    { 14, 2, 18 },
                    { 15, 2, 20 },
                    { 16, 2, 22 },
                    { 17, 2, 23 },
                    { 18, 2, 24 },
                    { 19, 2, 26 },
                    { 20, 2, 28 },
                    { 21, 2, 29 },
                    { 22, 2, 31 },
                    { 23, 2, 34 },
                    { 24, 2, 35 },
                    { 25, 2, 38 },
                    { 26, 2, 39 },
                    { 27, 2, 40 },
                    { 28, 2, 41 },
                    { 29, 2, 45 },
                    { 30, 2, 50 },
                    { 31, 2, 51 },
                    { 32, 2, 52 },
                    { 33, 2, 53 },
                    { 34, 2, 54 },
                    { 35, 2, 55 },
                    { 36, 3, 3 },
                    { 37, 3, 15 },
                    { 38, 3, 16 },
                    { 39, 3, 17 },
                    { 40, 3, 19 },
                    { 41, 3, 30 },
                    { 42, 4, 5 },
                    { 43, 4, 8 },
                    { 44, 4, 9 },
                    { 45, 4, 21 },
                    { 46, 4, 25 },
                    { 47, 4, 27 },
                    { 48, 4, 33 },
                    { 49, 4, 36 },
                    { 50, 5, 4 },
                    { 51, 5, 12 },
                    { 52, 5, 37 },
                    { 53, 5, 38 },
                    { 54, 5, 45 },
                    { 55, 5, 46 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemCategoriesMapping_MenuCategoryId",
                table: "MenuItemCategoriesMapping",
                column: "MenuCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemCategoriesMapping_MenuItemId",
                table: "MenuItemCategoriesMapping",
                column: "MenuItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItemCategoriesMapping");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "MenuCategories");

            migrationBuilder.DropTable(
                name: "MenuItems");
        }
    }
}
