using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProdService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DishCarts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishCarts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Processings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReduceSquirrels = table.Column<double>(type: "float", nullable: false),
                    ReduceFats = table.Column<double>(type: "float", nullable: false),
                    ReduceСarbohydrates = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DishCartMenuItemJunctions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DishCartId = table.Column<long>(type: "bigint", nullable: false),
                    MenuItemId = table.Column<long>(type: "bigint", nullable: false),
                    MealId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishCartMenuItemJunctions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DishCartMenuItemJunctions_DishCarts_DishCartId",
                        column: x => x.DishCartId,
                        principalTable: "DishCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishCartMenuItemJunctions_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishCartMenuItemJunctions_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeelRate = table.Column<double>(type: "float", nullable: false),
                    HeatTreatmentRate = table.Column<double>(type: "float", nullable: false),
                    Squirrels = table.Column<double>(type: "float", nullable: false),
                    Fats = table.Column<double>(type: "float", nullable: false),
                    Сarbohydrates = table.Column<double>(type: "float", nullable: false),
                    NormPerDay = table.Column<double>(type: "float", nullable: false),
                    KCal = table.Column<double>(type: "float", nullable: false),
                    ProductGroupId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductGroups_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "ProductGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductDishCartJunctions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    DishCartId = table.Column<long>(type: "bigint", nullable: false),
                    ProcessingId = table.Column<long>(type: "bigint", nullable: true),
                    WeightBrutto = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDishCartJunctions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDishCartJunctions_DishCarts_DishCartId",
                        column: x => x.DishCartId,
                        principalTable: "DishCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDishCartJunctions_Processings_ProcessingId",
                        column: x => x.ProcessingId,
                        principalTable: "Processings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductDishCartJunctions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DishCarts",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Сосиска отварная, каша пшеная вязкая" },
                    { 2L, "Лук, хлеб ржаной" }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Завтрак" },
                    { 2L, "Обед" },
                    { 3L, "Ужин" }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Date" },
                values: new object[,]
                {
                    { 1L, new DateTime(2022, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2L, new DateTime(2022, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Processings",
                columns: new[] { "Id", "Name", "ReduceFats", "ReduceSquirrels", "ReduceСarbohydrates" },
                values: new object[,]
                {
                    { 4L, "Очистка + Тепловая обработка", 0.12, 0.059999999999999998, 0.089999999999999997 },
                    { 3L, "Тепловая обработка", 0.12, 0.059999999999999998, 0.089999999999999997 },
                    { 1L, "Без обработки", 0.0, 0.0, 0.0 },
                    { 2L, "Очистка", 0.0, 0.0, 0.0 }
                });

            migrationBuilder.InsertData(
                table: "ProductGroups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 8L, "Консервные изделия" },
                    { 1L, "Мясная продукция" },
                    { 2L, "Рыбная продукция" },
                    { 3L, "Колбасные изделия" },
                    { 4L, "Жиры" },
                    { 5L, "Крупа" },
                    { 6L, "Овощи" },
                    { 7L, "Хлебобулочные изделия" },
                    { 9L, "Прочие" }
                });

            migrationBuilder.InsertData(
                table: "DishCartMenuItemJunctions",
                columns: new[] { "Id", "DishCartId", "MealId", "MenuItemId" },
                values: new object[,]
                {
                    { 1L, 1L, 1L, 1L },
                    { 2L, 1L, 1L, 1L },
                    { 3L, 2L, 2L, 1L },
                    { 4L, 2L, 2L, 1L },
                    { 5L, 2L, 2L, 1L },
                    { 6L, 2L, 3L, 1L },
                    { 7L, 1L, 1L, 1L },
                    { 8L, 1L, 1L, 2L }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Fats", "HeatTreatmentRate", "KCal", "Name", "NormPerDay", "PeelRate", "ProductGroupId", "Squirrels", "Сarbohydrates" },
                values: new object[,]
                {
                    { 1L, 49.299999999999997, 0.32000000000000001, 491.0, "Свинина", 150.0, 0.245, 1L, 11.699999999999999, 0.0 },
                    { 5L, 31.600000000000001, 0.029999999999999999, 270.0, "Сосиски", 100.0, 0.029999999999999999, 3L, 10.1, 1.8999999999999999 },
                    { 6L, 78.0, 0.0, 709.0, "Масло сливочное", 30.0, 0.0, 4L, 0.69999999999999996, 1.0 },
                    { 2L, 3.2999999999999998, 4.0, 348.0, "Пшено", 100.0, 0.0, 5L, 11.5, 66.5 },
                    { 4L, 0.20000000000000001, 0.5, 41.0, "Лук", 30.0, 0.16, 6L, 1.3999999999999999, 8.1999999999999993 },
                    { 3L, 1.3999999999999999, 0.0, 210.0, "Xлеб ржаной", 100.0, 0.0, 7L, 7.7000000000000002, 37.600000000000001 }
                });

            migrationBuilder.InsertData(
                table: "ProductDishCartJunctions",
                columns: new[] { "Id", "DishCartId", "ProcessingId", "ProductId", "WeightBrutto" },
                values: new object[,]
                {
                    { 2L, 1L, 2L, 5L, 80.0 },
                    { 3L, 1L, 1L, 6L, 5.0 },
                    { 1L, 1L, 2L, 2L, 80.0 },
                    { 4L, 2L, 2L, 4L, 20.0 },
                    { 5L, 2L, 1L, 3L, 100.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DishCartMenuItemJunctions_DishCartId",
                table: "DishCartMenuItemJunctions",
                column: "DishCartId");

            migrationBuilder.CreateIndex(
                name: "IX_DishCartMenuItemJunctions_MealId",
                table: "DishCartMenuItemJunctions",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_DishCartMenuItemJunctions_MenuItemId",
                table: "DishCartMenuItemJunctions",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDishCartJunctions_DishCartId",
                table: "ProductDishCartJunctions",
                column: "DishCartId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDishCartJunctions_ProcessingId",
                table: "ProductDishCartJunctions",
                column: "ProcessingId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDishCartJunctions_ProductId",
                table: "ProductDishCartJunctions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductGroupId",
                table: "Products",
                column: "ProductGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DishCartMenuItemJunctions");

            migrationBuilder.DropTable(
                name: "ProductDishCartJunctions");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "DishCarts");

            migrationBuilder.DropTable(
                name: "Processings");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductGroups");
        }
    }
}
