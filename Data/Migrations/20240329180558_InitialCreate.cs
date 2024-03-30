using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CliniCareApp.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "Dni", "LastName", "Name", "Phone" },
                values: new object[] { 3, "Dato", "73000461W", "Cetina", "Carlota", "654465115" });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "Dni", "LastName", "Name", "Phone" },
                values: new object[] { 4, "Olvido", "73000461J", "Gimenez", "Jesus", "654465115" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
