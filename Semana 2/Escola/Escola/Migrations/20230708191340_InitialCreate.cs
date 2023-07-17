using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Escola.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlunoTB",
                columns: table => new
                {
                    PK_ID = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    SOBRENOME = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    IDADE = table.Column<int>(type: "INT", nullable: false),
                    GENERO = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    TELEFONE = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DATA_NASCIMENTO = table.Column<DateTime>(type: "DATETIME2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ALUNO_ID", x => x.PK_ID);
                });

            migrationBuilder.CreateTable(
                name: "MateriaTB",
                columns: table => new
                {
                    PK_ID = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MATERIA_ID", x => x.PK_ID);
                });

            migrationBuilder.CreateTable(
                name: "TurmaTB",
                columns: table => new
                {
                    PK_ID = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CURSO = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    NOME = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TURMA_ID", x => x.PK_ID);
                });

            migrationBuilder.CreateTable(
                name: "BoletimTB",
                columns: table => new
                {
                    PK_ID = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ORDER_DATA = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    FK_ALUNO_ID = table.Column<int>(type: "INT", nullable: false),
                    AlunoId = table.Column<int>(type: "INT", nullable: false),
                    PK_ID1 = table.Column<int>(type: "INT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BOLETIM_ID", x => x.PK_ID);
                    table.ForeignKey(
                        name: "FK_BoletimTB_AlunoTB_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "AlunoTB",
                        principalColumn: "PK_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoletimTB_AlunoTB_PK_ID1",
                        column: x => x.PK_ID1,
                        principalTable: "AlunoTB",
                        principalColumn: "PK_ID");
                });

            migrationBuilder.CreateTable(
                name: "NotasMateriaTB",
                columns: table => new
                {
                    PK_ID = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOTA = table.Column<int>(type: "INT", nullable: false),
                    FK_MATERIA_ID = table.Column<int>(type: "INT", nullable: false),
                    FK_BOLETIM_ID = table.Column<int>(type: "INT", nullable: false),
                    MateriaId = table.Column<int>(type: "INT", nullable: false),
                    BoletimId = table.Column<int>(type: "INT", nullable: false),
                    FK_BOLETIM_ID1 = table.Column<int>(type: "INT", nullable: true),
                    FK_MATERIA_ID1 = table.Column<int>(type: "INT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOTAS_MATERIA_ID", x => x.PK_ID);
                    table.ForeignKey(
                        name: "FK_NotasMateriaTB_BoletimTB_BoletimId",
                        column: x => x.BoletimId,
                        principalTable: "BoletimTB",
                        principalColumn: "PK_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotasMateriaTB_BoletimTB_FK_BOLETIM_ID1",
                        column: x => x.FK_BOLETIM_ID1,
                        principalTable: "BoletimTB",
                        principalColumn: "PK_ID");
                    table.ForeignKey(
                        name: "FK_NotasMateriaTB_MateriaTB_FK_MATERIA_ID1",
                        column: x => x.FK_MATERIA_ID1,
                        principalTable: "MateriaTB",
                        principalColumn: "PK_ID");
                    table.ForeignKey(
                        name: "FK_NotasMateriaTB_MateriaTB_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "MateriaTB",
                        principalColumn: "PK_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoletimTB_AlunoId",
                table: "BoletimTB",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_BoletimTB_PK_ID1",
                table: "BoletimTB",
                column: "PK_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_NotasMateriaTB_BoletimId",
                table: "NotasMateriaTB",
                column: "BoletimId");

            migrationBuilder.CreateIndex(
                name: "IX_NotasMateriaTB_FK_BOLETIM_ID1",
                table: "NotasMateriaTB",
                column: "FK_BOLETIM_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_NotasMateriaTB_FK_MATERIA_ID1",
                table: "NotasMateriaTB",
                column: "FK_MATERIA_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_NotasMateriaTB_MateriaId",
                table: "NotasMateriaTB",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_TurmaTB_NOME",
                table: "TurmaTB",
                column: "NOME",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotasMateriaTB");

            migrationBuilder.DropTable(
                name: "TurmaTB");

            migrationBuilder.DropTable(
                name: "BoletimTB");

            migrationBuilder.DropTable(
                name: "MateriaTB");

            migrationBuilder.DropTable(
                name: "AlunoTB");
        }
    }
}
