using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TodoApi.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Priority = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Medium"),
                    Category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Todos",
                columns: new[] { "Id", "Category", "CompletedAt", "CreatedAt", "Description", "Priority", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Development", null, new DateTime(2025, 7, 14, 16, 52, 39, 785, DateTimeKind.Utc).AddTicks(6466), "Set up the initial .NET API project with Entity Framework and PostgreSQL", "High", "Complete project setup", null },
                    { 2, "Development", null, new DateTime(2025, 7, 14, 16, 52, 39, 785, DateTimeKind.Utc).AddTicks(6761), "Create endpoints for Create, Read, Update, and Delete operations for todos", "High", "Implement CRUD operations", null },
                    { 3, "DevOps", null, new DateTime(2025, 7, 14, 16, 52, 39, 785, DateTimeKind.Utc).AddTicks(6762), "Configure GitHub Actions for continuous integration and deployment", "Medium", "Set up CI/CD pipeline", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todos_CreatedAt",
                table: "Todos",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_IsCompleted",
                table: "Todos",
                column: "IsCompleted");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_Priority",
                table: "Todos",
                column: "Priority");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todos");
        }
    }
}
