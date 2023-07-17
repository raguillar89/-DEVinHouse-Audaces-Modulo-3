using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Escola.Migrations
{
    /// <inheritdoc />
    public partial class UserAM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuarioTB",
                columns: table => new
                {
                    PK_ID = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    TIPO_ACESSO = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    USUARIO = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    SENHA = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    Interno = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_ID", x => x.PK_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioTB");
        }
    }
}
