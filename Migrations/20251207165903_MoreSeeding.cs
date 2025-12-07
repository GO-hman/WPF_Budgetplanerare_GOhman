using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WPF_Budgetplanerare_GOhman.Migrations
{
    /// <inheritdoc />
    public partial class MoreSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BudgetTransactions",
                columns: new[] { "Id", "Amount", "Category", "EffectiveDate", "IsRecurrence", "IsRecurring", "Note", "RecurringRuleId", "TransactionType" },
                values: new object[,]
                {
                    { new Guid("11111111-2222-3333-4444-555555555555"), 1600m, 8, new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, "Frilansprojekt", null, 0 },
                    { new Guid("22222222-3333-4444-5555-666666666666"), 500m, 3, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, "Resa till Stockholm", null, 1 },
                    { new Guid("33333333-4444-5555-6666-777777777777"), 300m, 4, new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, "Biobiljetter", null, 1 },
                    { new Guid("44444444-5555-6666-7777-888888888888"), 2000m, 9, new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, "Sommarbonus", new Guid("ffffffff-bbbb-cccc-dddd-eeeeeeeeeeee"), 0 },
                    { new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"), 450m, 4, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, "Gymkort", new Guid("eeeeeeee-aaaa-bbbb-cccc-dddddddddddd"), 1 },
                    { new Guid("cccccccc-dddd-eeee-ffff-111111111111"), 900m, 0, new DateTime(2025, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, "Matinköp", null, 1 },
                    { new Guid("dddddddd-eeee-ffff-1111-222222222222"), 350m, 1, new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, "Internetabonnemang", new Guid("11111111-aaaa-bbbb-cccc-ffffffffffff"), 1 },
                    { new Guid("eeeeeeee-ffff-1111-2222-333333333333"), 120m, 1, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, "Mobilabonnemang", new Guid("22222222-aaaa-bbbb-cccc-ffffffffffff"), 1 },
                    { new Guid("ffffffff-1111-2222-3333-444444444444"), 700m, 9, new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, "Kläder", null, 1 }
                });

            migrationBuilder.UpdateData(
                table: "RecurringRules",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                column: "StartDate",
                value: new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "RecurringRules",
                columns: new[] { "Id", "BudgetTransactionId", "EndDate", "Frequency", "StartDate" },
                values: new object[,]
                {
                    { new Guid("11111111-aaaa-bbbb-cccc-ffffffffffff"), new Guid("dddddddd-eeee-ffff-1111-222222222222"), null, 0, new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("22222222-aaaa-bbbb-cccc-ffffffffffff"), new Guid("eeeeeeee-ffff-1111-2222-333333333333"), null, 0, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("eeeeeeee-aaaa-bbbb-cccc-dddddddddddd"), new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"), null, 0, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("ffffffff-bbbb-cccc-dddd-eeeeeeeeeeee"), new Guid("44444444-5555-6666-7777-888888888888"), null, 1, new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BudgetTransactions",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555555"));

            migrationBuilder.DeleteData(
                table: "BudgetTransactions",
                keyColumn: "Id",
                keyValue: new Guid("22222222-3333-4444-5555-666666666666"));

            migrationBuilder.DeleteData(
                table: "BudgetTransactions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-4444-5555-6666-777777777777"));

            migrationBuilder.DeleteData(
                table: "BudgetTransactions",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-dddd-eeee-ffff-111111111111"));

            migrationBuilder.DeleteData(
                table: "BudgetTransactions",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-1111-2222-3333-444444444444"));

            migrationBuilder.DeleteData(
                table: "RecurringRules",
                keyColumn: "Id",
                keyValue: new Guid("11111111-aaaa-bbbb-cccc-ffffffffffff"));

            migrationBuilder.DeleteData(
                table: "RecurringRules",
                keyColumn: "Id",
                keyValue: new Guid("22222222-aaaa-bbbb-cccc-ffffffffffff"));

            migrationBuilder.DeleteData(
                table: "RecurringRules",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-aaaa-bbbb-cccc-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "RecurringRules",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-bbbb-cccc-dddd-eeeeeeeeeeee"));

            migrationBuilder.DeleteData(
                table: "BudgetTransactions",
                keyColumn: "Id",
                keyValue: new Guid("44444444-5555-6666-7777-888888888888"));

            migrationBuilder.DeleteData(
                table: "BudgetTransactions",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"));

            migrationBuilder.DeleteData(
                table: "BudgetTransactions",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-eeee-ffff-1111-222222222222"));

            migrationBuilder.DeleteData(
                table: "BudgetTransactions",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-ffff-1111-2222-333333333333"));

            migrationBuilder.UpdateData(
                table: "RecurringRules",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                column: "StartDate",
                value: new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
