using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projeto.infra.Migrations
{
    /// <inheritdoc />
    public partial class Adicionadoindexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "X-Consultas-Indexes",
                table: "Projetos",
                columns: new[] { "Status", "Nome" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "X-Consultas-Indexes",
                table: "Projetos");
        }
    }
}
