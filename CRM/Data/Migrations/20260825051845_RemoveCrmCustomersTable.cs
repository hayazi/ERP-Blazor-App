using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPBlazorApp.CRM.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCrmCustomersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_CrmCustomers_CustomerId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_CrmCustomers_CustomerId",
                table: "Opportunities");

            migrationBuilder.DropTable(
                name: "CrmCustomers");

            migrationBuilder.DropIndex(
                name: "IX_Opportunities_CustomerId",
                table: "Opportunities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_CustomerId",
                table: "Activities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CrmCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AssignedToUserId = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Company = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrmCustomers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_CustomerId",
                table: "Opportunities",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CustomerId",
                table: "Activities",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_CrmCustomers_CustomerId",
                table: "Activities",
                column: "CustomerId",
                principalTable: "CrmCustomers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_CrmCustomers_CustomerId",
                table: "Opportunities",
                column: "CustomerId",
                principalTable: "CrmCustomers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
