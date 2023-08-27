using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projeto.infra.Migrations
{
    /// <inheritdoc />
    public partial class Permitidoacentoseadicionadostatusenomeindexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProdutosEmEstoque",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutosEmEstoque", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projetos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataEntrega = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProdutoUtilizadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantidadeUtilizado = table.Column<int>(type: "INTEGER", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Valor = table.Column<double>(type: "REAL", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projetos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projetos_ProdutosEmEstoque_ProdutoUtilizadoId",
                        column: x => x.ProdutoUtilizadoId,
                        principalTable: "ProdutosEmEstoque",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projetos_Nome",
                table: "Projetos",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_Projetos_ProdutoUtilizadoId",
                table: "Projetos",
                column: "ProdutoUtilizadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Projetos_Status",
                table: "Projetos",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projetos");

            migrationBuilder.DropTable(
                name: "ProdutosEmEstoque");
        }
    }
}
