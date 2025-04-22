using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudCarros.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoModelos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Usuarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Concessionarias",
                columns: table => new
                {
                    ConcessionariaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapacidadeMaximaVeiculos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concessionarias", x => x.ConcessionariaId);
                });

            migrationBuilder.CreateTable(
                name: "Fabricantes",
                columns: table => new
                {
                    FabricanteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PaisDeOrigem = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AnoDeFundacao = table.Column<int>(type: "int", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fabricantes", x => x.FabricanteId);
                });

            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    VeiculoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AnoFabricacao = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FabricanteId = table.Column<int>(type: "int", nullable: false),
                    FabricanteId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.VeiculoId);
                    table.ForeignKey(
                        name: "FK_Veiculos_Fabricantes_FabricanteId1",
                        column: x => x.FabricanteId1,
                        principalTable: "Fabricantes",
                        principalColumn: "FabricanteId");
                });

            migrationBuilder.CreateTable(
                name: "Vendas",
                columns: table => new
                {
                    VendaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConcessionariaId = table.Column<int>(type: "int", nullable: false),
                    ConcessionariaId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FabricanteId = table.Column<int>(type: "int", nullable: false),
                    FabricanteId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VeiculoId = table.Column<int>(type: "int", nullable: false),
                    VeiculoId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClienteNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClienteCPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClienteTelefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataVenda = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrecoVenda = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumeroProtocolo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendas", x => x.VendaId);
                    table.ForeignKey(
                        name: "FK_Vendas_Concessionarias_ConcessionariaId1",
                        column: x => x.ConcessionariaId1,
                        principalTable: "Concessionarias",
                        principalColumn: "ConcessionariaId");
                    table.ForeignKey(
                        name: "FK_Vendas_Fabricantes_FabricanteId1",
                        column: x => x.FabricanteId1,
                        principalTable: "Fabricantes",
                        principalColumn: "FabricanteId");
                    table.ForeignKey(
                        name: "FK_Vendas_Veiculos_VeiculoId1",
                        column: x => x.VeiculoId1,
                        principalTable: "Veiculos",
                        principalColumn: "VeiculoId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_FabricanteId1",
                table: "Veiculos",
                column: "FabricanteId1");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_ConcessionariaId1",
                table: "Vendas",
                column: "ConcessionariaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_FabricanteId1",
                table: "Vendas",
                column: "FabricanteId1");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_VeiculoId1",
                table: "Vendas",
                column: "VeiculoId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vendas");

            migrationBuilder.DropTable(
                name: "Concessionarias");

            migrationBuilder.DropTable(
                name: "Veiculos");

            migrationBuilder.DropTable(
                name: "Fabricantes");

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
