using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projeto.infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoCollation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase(
                collation: "SQL_LATIN1_GENERAL_CP1_CI_AI");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase(
                oldCollation: "SQL_LATIN1_GENERAL_CP1_CI_AI");
        }
    }
}
