using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoInterdisciplinarII.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Login = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Nivel = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
                    Senha = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Postagens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Imagem = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Conteudo = table.Column<string>(type: "TEXT", nullable: false),
                    Ativo = table.Column<int>(type: "INTEGER", nullable: false),
                    IdUsuarioFk = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Postagens_Usuarios_IdUsuarioFk",
                        column: x => x.IdUsuarioFk,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Conteudo = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    IdUsuarioFk = table.Column<int>(type: "INTEGER", nullable: false),
                    IdPostagemFk = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comentarios_Postagens_IdPostagemFk",
                        column: x => x.IdPostagemFk,
                        principalTable: "Postagens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comentarios_Usuarios_IdUsuarioFk",
                        column: x => x.IdUsuarioFk,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Curtidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdUsuarioFk = table.Column<int>(type: "INTEGER", nullable: false),
                    IdPostagemFk = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curtidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Curtidas_Postagens_IdPostagemFk",
                        column: x => x.IdPostagemFk,
                        principalTable: "Postagens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Curtidas_Usuarios_IdUsuarioFk",
                        column: x => x.IdUsuarioFk,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_IdPostagemFk",
                table: "Comentarios",
                column: "IdPostagemFk");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_IdUsuarioFk",
                table: "Comentarios",
                column: "IdUsuarioFk");

            migrationBuilder.CreateIndex(
                name: "IX_Curtidas_IdPostagemFk",
                table: "Curtidas",
                column: "IdPostagemFk");

            migrationBuilder.CreateIndex(
                name: "IX_Curtidas_IdUsuarioFk",
                table: "Curtidas",
                column: "IdUsuarioFk");

            migrationBuilder.CreateIndex(
                name: "IX_Postagens_IdUsuarioFk",
                table: "Postagens",
                column: "IdUsuarioFk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "Curtidas");

            migrationBuilder.DropTable(
                name: "Postagens");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
