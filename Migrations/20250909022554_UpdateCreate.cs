using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mottu.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motos_Patios_PatioId",
                table: "Motos");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosPatio_Patios_PatioId",
                table: "UsuariosPatio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patios",
                table: "Patios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Motos",
                table: "Motos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuariosPatio",
                table: "UsuariosPatio");

            migrationBuilder.RenameTable(
                name: "Patios",
                newName: "PATIOS");

            migrationBuilder.RenameTable(
                name: "Motos",
                newName: "MOTOS");

            migrationBuilder.RenameTable(
                name: "UsuariosPatio",
                newName: "USUARIOS_PATIO");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "MOTOS",
                newName: "STATUS");

            migrationBuilder.RenameColumn(
                name: "Placa",
                table: "MOTOS",
                newName: "PLACA");

            migrationBuilder.RenameIndex(
                name: "IX_Motos_PatioId",
                table: "MOTOS",
                newName: "IX_MOTOS_PatioId");

            migrationBuilder.RenameIndex(
                name: "IX_UsuariosPatio_PatioId",
                table: "USUARIOS_PATIO",
                newName: "IX_USUARIOS_PATIO_PatioId");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "PATIOS",
                type: "NVARCHAR2(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AlterColumn<string>(
                name: "STATUS",
                table: "MOTOS",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

            migrationBuilder.AlterColumn<string>(
                name: "PLACA",
                table: "MOTOS",
                type: "NVARCHAR2(7)",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AlterColumn<string>(
                name: "Modelo",
                table: "MOTOS",
                type: "NVARCHAR2(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AddColumn<string>(
                name: "COR",
                table: "MOTOS",
                type: "NVARCHAR2(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SETOR",
                table: "MOTOS",
                type: "NVARCHAR2(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "USUARIOS_PATIO",
                type: "NVARCHAR2(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AlterColumn<string>(
                name: "Funcao",
                table: "USUARIOS_PATIO",
                type: "NVARCHAR2(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "USUARIOS_PATIO",
                type: "NVARCHAR2(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PATIOS",
                table: "PATIOS",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MOTOS",
                table: "MOTOS",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_USUARIOS_PATIO",
                table: "USUARIOS_PATIO",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MOTOS_PATIOS_PatioId",
                table: "MOTOS",
                column: "PatioId",
                principalTable: "PATIOS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_USUARIOS_PATIO_PATIOS_PatioId",
                table: "USUARIOS_PATIO",
                column: "PatioId",
                principalTable: "PATIOS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MOTOS_PATIOS_PatioId",
                table: "MOTOS");

            migrationBuilder.DropForeignKey(
                name: "FK_USUARIOS_PATIO_PATIOS_PatioId",
                table: "USUARIOS_PATIO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PATIOS",
                table: "PATIOS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MOTOS",
                table: "MOTOS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USUARIOS_PATIO",
                table: "USUARIOS_PATIO");

            migrationBuilder.DropColumn(
                name: "COR",
                table: "MOTOS");

            migrationBuilder.DropColumn(
                name: "SETOR",
                table: "MOTOS");

            migrationBuilder.RenameTable(
                name: "PATIOS",
                newName: "Patios");

            migrationBuilder.RenameTable(
                name: "MOTOS",
                newName: "Motos");

            migrationBuilder.RenameTable(
                name: "USUARIOS_PATIO",
                newName: "UsuariosPatio");

            migrationBuilder.RenameColumn(
                name: "STATUS",
                table: "Motos",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "PLACA",
                table: "Motos",
                newName: "Placa");

            migrationBuilder.RenameIndex(
                name: "IX_MOTOS_PatioId",
                table: "Motos",
                newName: "IX_Motos_PatioId");

            migrationBuilder.RenameIndex(
                name: "IX_USUARIOS_PATIO_PatioId",
                table: "UsuariosPatio",
                newName: "IX_UsuariosPatio_PatioId");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Patios",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(120)",
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Motos",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AlterColumn<string>(
                name: "Placa",
                table: "Motos",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(7)",
                oldMaxLength: 7);

            migrationBuilder.AlterColumn<string>(
                name: "Modelo",
                table: "Motos",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "UsuariosPatio",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(120)",
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<string>(
                name: "Funcao",
                table: "UsuariosPatio",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UsuariosPatio",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(200)",
                oldMaxLength: 200);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patios",
                table: "Patios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Motos",
                table: "Motos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuariosPatio",
                table: "UsuariosPatio",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Motos_Patios_PatioId",
                table: "Motos",
                column: "PatioId",
                principalTable: "Patios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosPatio_Patios_PatioId",
                table: "UsuariosPatio",
                column: "PatioId",
                principalTable: "Patios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
