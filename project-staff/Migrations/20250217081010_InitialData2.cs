using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace project_staff.Migrations
{
    /// <inheritdoc />
    public partial class InitialData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f79d5f27-68e7-4f87-8cfc-0053b681e966", "fa96cd60-61e7-40c2-994b-66c2170df6ba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fa5bb702-8b95-4fa9-9f55-60cc62d3b159"),
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "88db8975-ca39-4262-bdbe-1bae4b16d8f8", "48b1bc86-cb18-4564-b6a0-eac6d044f8da" });

            migrationBuilder.InsertData(
                table: "ProjectEmployees",
                columns: new[] { "EmployeesId", "ProjectsId" },
                values: new object[,]
                {
                    { new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), new Guid("d97e8cfc-bc6f-4f94-bc0c-68fcb3c6362b") },
                    { new Guid("fa5bb702-8b95-4fa9-9f55-60cc62d3b159"), new Guid("d97e8cfc-bc6f-4f94-bc0c-68fcb3c6362b") }
                });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: new Guid("d97e8cfc-bc6f-4f94-bc0c-68fcb3c6362b"),
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 17, 8, 10, 9, 167, DateTimeKind.Utc).AddTicks(8086), new DateTime(2025, 2, 17, 8, 10, 9, 167, DateTimeKind.Utc).AddTicks(8083) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeesId", "ProjectsId" },
                keyValues: new object[] { new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), new Guid("d97e8cfc-bc6f-4f94-bc0c-68fcb3c6362b") });

            migrationBuilder.DeleteData(
                table: "ProjectEmployees",
                keyColumns: new[] { "EmployeesId", "ProjectsId" },
                keyValues: new object[] { new Guid("fa5bb702-8b95-4fa9-9f55-60cc62d3b159"), new Guid("d97e8cfc-bc6f-4f94-bc0c-68fcb3c6362b") });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "909ba067-e89f-42b8-99b7-8ec8bf965abc", "8cc766e8-e62b-40c3-9e62-5bfb68112d13" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fa5bb702-8b95-4fa9-9f55-60cc62d3b159"),
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e7863294-775c-4973-8ea8-cbdf8593173e", "77c95c6c-d3b7-465e-b3a6-9ce34b39f93b" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: new Guid("d97e8cfc-bc6f-4f94-bc0c-68fcb3c6362b"),
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 17, 7, 59, 54, 691, DateTimeKind.Utc).AddTicks(9579), new DateTime(2025, 2, 17, 7, 59, 54, 691, DateTimeKind.Utc).AddTicks(9576) });
        }
    }
}
