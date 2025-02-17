using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace project_staff.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), 0, "909ba067-e89f-42b8-99b7-8ec8bf965abc", "admin@example.com", true, "Admin", "User", false, null, "Super", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAEAACcQAAAAEHXz...", null, false, "8cc766e8-e62b-40c3-9e62-5bfb68112d13", false, "admin@example.com" },
                    { new Guid("fa5bb702-8b95-4fa9-9f55-60cc62d3b159"), 0, "e7863294-775c-4973-8ea8-cbdf8593173e", "employee@example.com", true, "Kyrgyz", "Employee", false, null, "Tech", "EMPLOYEE@EXAMPLE.COM", "EMPLOYEE@EXAMPLE.COM", "AQAAAAEAACcQAAAAEHXz...", null, false, "77c95c6c-d3b7-465e-b3a6-9ce34b39f93b", false, "employee@example.com" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "CustomerCompany", "EndDate", "ExecutionCompany", "ManagerId", "Name", "Priority", "StartDate" },
                values: new object[] { new Guid("d97e8cfc-bc6f-4f94-bc0c-68fcb3c6362b"), "Kyrgyz Innovations", new DateTime(2025, 8, 17, 7, 59, 54, 691, DateTimeKind.Utc).AddTicks(9579), "Turkish Tech", new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "Smart City AI", 1, new DateTime(2025, 2, 17, 7, 59, 54, 691, DateTimeKind.Utc).AddTicks(9576) });

            migrationBuilder.InsertData(
                table: "ProjectTasks",
                columns: new[] { "TaskId", "AuthorId", "Comment", "ExecutorId", "Name", "Priority", "ProjectId", "Status" },
                values: new object[] { new Guid("10d7c6f3-85fc-4fa7-a9a1-2f828c053c0c"), new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "First stage of AI implementation.", new Guid("fa5bb702-8b95-4fa9-9f55-60cc62d3b159"), "Develop AI Model", 1, new Guid("d97e8cfc-bc6f-4f94-bc0c-68fcb3c6362b"), 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProjectTasks",
                keyColumn: "TaskId",
                keyValue: new Guid("10d7c6f3-85fc-4fa7-a9a1-2f828c053c0c"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fa5bb702-8b95-4fa9-9f55-60cc62d3b159"));

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: new Guid("d97e8cfc-bc6f-4f94-bc0c-68fcb3c6362b"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"));
        }
    }
}
