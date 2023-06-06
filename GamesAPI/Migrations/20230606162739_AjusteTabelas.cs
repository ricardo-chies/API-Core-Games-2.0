using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesAPI.Migrations
{
    public partial class AjusteTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PRECO",
                table: "Jogos",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<decimal>(
                name: "NOTA",
                table: "Jogos",
                type: "decimal(3,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,4)");

            migrationBuilder.UpdateData(
                table: "Jogos",
                keyColumn: "NOME",
                keyValue: null,
                column: "NOME",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "NOME",
                table: "Jogos",
                type: "varchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Jogos",
                keyColumn: "IMAGEM_URL",
                keyValue: null,
                column: "IMAGEM_URL",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "IMAGEM_URL",
                table: "Jogos",
                type: "varchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Jogos",
                keyColumn: "DESCRICAO",
                keyValue: null,
                column: "DESCRICAO",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "DESCRICAO",
                table: "Jogos",
                type: "varchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Jogos",
                keyColumn: "ANO_LANCAMENTO",
                keyValue: null,
                column: "ANO_LANCAMENTO",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ANO_LANCAMENTO",
                table: "Jogos",
                type: "varchar(4)",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "NOME",
                keyValue: null,
                column: "NOME",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "NOME",
                table: "Categorias",
                type: "varchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "IMAGEM_URL",
                keyValue: null,
                column: "IMAGEM_URL",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "IMAGEM_URL",
                table: "Categorias",
                type: "varchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogos_Categorias_CATEGORIA_ID",
                table: "Jogos");

            migrationBuilder.DropIndex(
                name: "IX_Jogos_CATEGORIA_ID",
                table: "Jogos");

            migrationBuilder.DropColumn(
                name: "CATEGORIA_ID",
                table: "Jogos");

            migrationBuilder.AlterColumn<decimal>(
                name: "PRECO",
                table: "Jogos",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PRECO",
                table: "Jogos",
                type: "decimal(14,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)");

            migrationBuilder.AlterColumn<string>(
                name: "NOME",
                table: "Jogos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(80)",
                oldMaxLength: 80)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "IMAGEM_URL",
                table: "Jogos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldMaxLength: 300)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "DESCRICAO",
                table: "Jogos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldMaxLength: 300)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ANO_LANCAMENTO",
                table: "Jogos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(4)",
                oldMaxLength: 4)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "NOME",
                table: "Categorias",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(80)",
                oldMaxLength: 80)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "IMAGEM_URL",
                table: "Categorias",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldMaxLength: 300)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_CATEGORIA_ID",
                table: "Jogos",
                column: "CATEGORIA_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogos_Categorias_CATEGORIA_ID",
                table: "Jogos",
                column: "CATEGORIA_ID",
                principalTable: "Categorias",
                principalColumn: "CATEGORIA_ID");
        }
    }
}
