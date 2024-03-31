using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CliniCareApp.Data.Migrations
{
    public partial class AppointmentMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUrgent = table.Column<bool>(type: "bit", nullable: false),
                    PatientDni = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "Dni", "LastName", "Name", "Phone" },
                values: new object[] { 3, "Dato", "73000461W", "Cetina", "Carlota", "654465115" });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "Dni", "LastName", "Name", "Phone" },
                values: new object[] { 4, "Olvido", "73000461J", "Gimenez", "Jesus", "654465115" });
        }
    }
}
