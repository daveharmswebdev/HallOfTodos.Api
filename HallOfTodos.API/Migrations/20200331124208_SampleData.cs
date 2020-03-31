using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HallOfTodos.API.Migrations
{
    public partial class SampleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Todos",
                columns: new[] { "Id", "Complete", "CompleteDate", "Description", "Doing", "DueDate", "Todo" },
                values: new object[,]
                {
                    { new Guid("72038873-4a70-455a-afaa-6ad787633ee3"), false, null, "Test 123", "Dave", null, "Buy Organic Eggs from Whole Foods" },
                    { new Guid("7be07a44-1b2b-4d5a-ad96-2ecc3699ae90"), false, null, "Picanha boudin meatloaf turducken ribeye ham hock, chuck flank doner tri-tip swine. T-bone brisket prosciutto buffalo tenderloin. Kielbasa kevin cow, shank meatball beef doner. Turkey picanha beef ribs, bresaola prosciutto shoulder buffalo cupim alcatra drumstick shank pork belly ham capicola. Jerky brisket shankle tail landjaeger. Boudin spare ribs salami t-bone, bresaola pastrami filet mignon pig ham. Pork belly flank shankle tongue cow strip steak picanha short ribs shoulder turkey rump burgdoggen pork frankfurter.", "Dave", null, "Buy cat food" },
                    { new Guid("c76cdf90-2beb-41d8-99c6-c4d0f0e783d9"), false, null, "Test 123", "Dave", null, "take suit to cleaners" },
                    { new Guid("27c7c8b9-5d8f-4334-b43c-38e6a694840c"), false, null, "Test 123", "Dave", null, "yeah" }
                });

            migrationBuilder.InsertData(
                table: "TodoNotes",
                columns: new[] { "Id", "Details", "Title", "TodoId", "WrittenBy" },
                values: new object[,]
                {
                    { new Guid("cb74c57a-88ad-4b90-9075-a7dde972c047"), "see above", "no eggs at Whole foods", new Guid("72038873-4a70-455a-afaa-6ad787633ee3"), null },
                    { new Guid("a59545f8-1698-4b5d-a4e9-ca562876b351"), "plenty of eggs here", "went to kroger", new Guid("72038873-4a70-455a-afaa-6ad787633ee3"), null },
                    { new Guid("06d9ba77-0ba3-4395-b618-61cde7e1d94c"), "see above", "got cat food at pet smart", new Guid("7be07a44-1b2b-4d5a-ad96-2ecc3699ae90"), null },
                    { new Guid("85c72ac1-516c-40cd-858d-11e6c6982ae3"), "plenty of cat food here", "30 dollars a bag", new Guid("7be07a44-1b2b-4d5a-ad96-2ecc3699ae90"), null },
                    { new Guid("977cad2e-94b7-49c7-85b3-6aab233d7e03"), "see above", "used heritage cleaners", new Guid("c76cdf90-2beb-41d8-99c6-c4d0f0e783d9"), null },
                    { new Guid("082e2722-f38d-4563-8289-94c567685d9a"), "plenty of chips here - test", "3 dollars a bag - test", new Guid("27c7c8b9-5d8f-4334-b43c-38e6a694840c"), null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TodoNotes",
                keyColumn: "Id",
                keyValue: new Guid("06d9ba77-0ba3-4395-b618-61cde7e1d94c"));

            migrationBuilder.DeleteData(
                table: "TodoNotes",
                keyColumn: "Id",
                keyValue: new Guid("082e2722-f38d-4563-8289-94c567685d9a"));

            migrationBuilder.DeleteData(
                table: "TodoNotes",
                keyColumn: "Id",
                keyValue: new Guid("85c72ac1-516c-40cd-858d-11e6c6982ae3"));

            migrationBuilder.DeleteData(
                table: "TodoNotes",
                keyColumn: "Id",
                keyValue: new Guid("977cad2e-94b7-49c7-85b3-6aab233d7e03"));

            migrationBuilder.DeleteData(
                table: "TodoNotes",
                keyColumn: "Id",
                keyValue: new Guid("a59545f8-1698-4b5d-a4e9-ca562876b351"));

            migrationBuilder.DeleteData(
                table: "TodoNotes",
                keyColumn: "Id",
                keyValue: new Guid("cb74c57a-88ad-4b90-9075-a7dde972c047"));

            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: new Guid("27c7c8b9-5d8f-4334-b43c-38e6a694840c"));

            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: new Guid("72038873-4a70-455a-afaa-6ad787633ee3"));

            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: new Guid("7be07a44-1b2b-4d5a-ad96-2ecc3699ae90"));

            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: new Guid("c76cdf90-2beb-41d8-99c6-c4d0f0e783d9"));
        }
    }
}
