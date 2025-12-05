using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WPF_Budgetplanerare_GOhman.Migrations
{
    /// <inheritdoc />
    public partial class finaldatastructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BudgetTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsRecurring = table.Column<bool>(type: "bit", nullable: false),
                    IsRecurrence = table.Column<bool>(type: "bit", nullable: false),
                    RecurringRuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetTransactions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecurringRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BudgetTransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurringRules_BudgetTransactions_BudgetTransactionId",
                        column: x => x.BudgetTransactionId,
                        principalTable: "BudgetTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecurrenceExceptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecurringRuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurrenceExceptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurrenceExceptions_RecurringRules_RecurringRuleId",
                        column: x => x.RecurringRuleId,
                        principalTable: "RecurringRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "TransactionType" },
                values: new object[,]
                {
                    { new Guid("d1111111-1111-1111-1111-111111111111"), "Mat", 1 },
                    { new Guid("d2222222-2222-2222-2222-222222222222"), "Hus & drift", 1 },
                    { new Guid("d3333333-3333-3333-3333-333333333333"), "Transport", 1 },
                    { new Guid("d4444444-4444-4444-4444-444444444444"), "Fritid", 1 },
                    { new Guid("d5555555-5555-5555-5555-555555555555"), "Streaming-tjänster", 1 },
                    { new Guid("d6666666-6666-6666-6666-666666666666"), "Lön/Övertid", 0 },
                    { new Guid("d7777777-7777-7777-7777-777777777777"), "Bidrag", 0 },
                    { new Guid("d8888888-8888-8888-8888-888888888888"), "Hobbyverksamhet", 0 }
                });

            migrationBuilder.InsertData(
                table: "BudgetTransactions",
                columns: new[] { "Id", "Amount", "CategoryId", "EffectiveDate", "IsRecurrence", "IsRecurring", "Note", "RecurringRuleId", "TransactionType" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 5000m, new Guid("d6666666-6666-6666-6666-666666666666"), new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, "Lön", null, 0 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 1200m, new Guid("d2222222-2222-2222-2222-222222222222"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, "Hyra", new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 1 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 800m, new Guid("d1111111-1111-1111-1111-111111111111"), new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, "Mat", null, 1 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), 400m, new Guid("d3333333-3333-3333-3333-333333333333"), new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, "Transport", null, 1 },
                    { new Guid("55555555-5555-5555-5555-555555555555"), 300m, new Guid("d2222222-2222-2222-2222-222222222222"), new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, "Elräkning", null, 1 },
                    { new Guid("66666666-6666-6666-6666-666666666666"), 200m, new Guid("d2222222-2222-2222-2222-222222222222"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, "Försäkring", null, 1 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), 1500m, new Guid("d8888888-8888-8888-8888-888888888888"), new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, "Frilansjobb", null, 0 },
                    { new Guid("88888888-8888-8888-8888-888888888888"), 600m, new Guid("d4444444-4444-4444-4444-444444444444"), new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, "Underhållning", null, 1 },
                    { new Guid("99999999-9999-9999-9999-999999999999"), 1000m, new Guid("d2222222-2222-2222-2222-222222222222"), new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, "Sparande", null, 1 },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 2500m, new Guid("d6666666-6666-6666-6666-666666666666"), new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, "Årsbonus", new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 0 }
                });

            migrationBuilder.InsertData(
                table: "RecurringRules",
                columns: new[] { "Id", "Amount", "BudgetTransactionId", "EndDate", "Frequency", "StartDate" },
                values: new object[,]
                {
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 1200m, new Guid("22222222-2222-2222-2222-222222222222"), null, 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 2500m, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, 1, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetTransactions_CategoryId",
                table: "BudgetTransactions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurrenceExceptions_RecurringRuleId",
                table: "RecurrenceExceptions",
                column: "RecurringRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringRules_BudgetTransactionId",
                table: "RecurringRules",
                column: "BudgetTransactionId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecurrenceExceptions");

            migrationBuilder.DropTable(
                name: "RecurringRules");

            migrationBuilder.DropTable(
                name: "BudgetTransactions");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
