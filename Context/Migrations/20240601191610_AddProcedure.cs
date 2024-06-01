using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicSoftware.Migrations
{
    /// <inheritdoc />
    public partial class AddProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Procedures",
                columns: table => new
                {
                    ProcedureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcedureName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProcedureDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProcedurePrice = table.Column<decimal>(type: "Decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedures", x => x.ProcedureId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Procedures");
        }
    }
}
