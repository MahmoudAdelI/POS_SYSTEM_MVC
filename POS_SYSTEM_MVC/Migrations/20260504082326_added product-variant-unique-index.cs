using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_SYSTEM_MVC.Migrations
{
    /// <inheritdoc />
    public partial class addedproductvariantuniqueindex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductAttributeValue_SubCategoryId",
                table: "ProductAttributeValue");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValue_SubCategoryId_AttributeId_Value",
                table: "ProductAttributeValue",
                columns: new[] { "SubCategoryId", "AttributeId", "Value" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductAttributeValue_SubCategoryId_AttributeId_Value",
                table: "ProductAttributeValue");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValue_SubCategoryId",
                table: "ProductAttributeValue",
                column: "SubCategoryId");
        }
    }
}
