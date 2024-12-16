using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLockerWindows.Migrations
{
    /// <inheritdoc />
    public partial class NewDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContrainteHoraires_Apps_AppId",
                table: "ContrainteHoraires");

            migrationBuilder.DropForeignKey(
                name: "FK_ContrainteHoraires_Utilisateurs_UtilisateurId",
                table: "ContrainteHoraires");

            migrationBuilder.DropForeignKey(
                name: "FK_ContrainteJours_Apps_AppId",
                table: "ContrainteJours");

            migrationBuilder.DropForeignKey(
                name: "FK_ContrainteJours_Utilisateurs_UtilisateurId",
                table: "ContrainteJours");

            migrationBuilder.DropForeignKey(
                name: "FK_ContrainteSemaines_Apps_AppId",
                table: "ContrainteSemaines");

            migrationBuilder.DropForeignKey(
                name: "FK_ContrainteSemaines_Utilisateurs_UtilisateurId",
                table: "ContrainteSemaines");

            migrationBuilder.DropForeignKey(
                name: "FK_StatistiquesUtilisations_Apps_AppId",
                table: "StatistiquesUtilisations");

            migrationBuilder.DropForeignKey(
                name: "FK_StatistiquesUtilisations_Utilisateurs_UtilisateurId",
                table: "StatistiquesUtilisations");

            migrationBuilder.DropIndex(
                name: "IX_StatistiquesUtilisations_AppId",
                table: "StatistiquesUtilisations");

            migrationBuilder.DropIndex(
                name: "IX_StatistiquesUtilisations_UtilisateurId",
                table: "StatistiquesUtilisations");

            migrationBuilder.DropIndex(
                name: "IX_ContrainteSemaines_AppId",
                table: "ContrainteSemaines");

            migrationBuilder.DropIndex(
                name: "IX_ContrainteSemaines_UtilisateurId",
                table: "ContrainteSemaines");

            migrationBuilder.DropIndex(
                name: "IX_ContrainteJours_AppId",
                table: "ContrainteJours");

            migrationBuilder.DropIndex(
                name: "IX_ContrainteJours_UtilisateurId",
                table: "ContrainteJours");

            migrationBuilder.DropIndex(
                name: "IX_ContrainteHoraires_AppId",
                table: "ContrainteHoraires");

            migrationBuilder.DropIndex(
                name: "IX_ContrainteHoraires_UtilisateurId",
                table: "ContrainteHoraires");

            migrationBuilder.DropColumn(
                name: "UtilisateurId",
                table: "StatistiquesUtilisations");

            migrationBuilder.DropColumn(
                name: "UtilisateurId",
                table: "ContrainteSemaines");

            migrationBuilder.DropColumn(
                name: "UtilisateurId",
                table: "ContrainteJours");

            migrationBuilder.DropColumn(
                name: "UtilisateurId",
                table: "ContrainteHoraires");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "StatistiquesUtilisations",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Date",
                table: "StatistiquesUtilisations",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "UtilisateurId",
                table: "StatistiquesUtilisations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UtilisateurId",
                table: "ContrainteSemaines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UtilisateurId",
                table: "ContrainteJours",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UtilisateurId",
                table: "ContrainteHoraires",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StatistiquesUtilisations_AppId",
                table: "StatistiquesUtilisations",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_StatistiquesUtilisations_UtilisateurId",
                table: "StatistiquesUtilisations",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_ContrainteSemaines_AppId",
                table: "ContrainteSemaines",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_ContrainteSemaines_UtilisateurId",
                table: "ContrainteSemaines",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_ContrainteJours_AppId",
                table: "ContrainteJours",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_ContrainteJours_UtilisateurId",
                table: "ContrainteJours",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_ContrainteHoraires_AppId",
                table: "ContrainteHoraires",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_ContrainteHoraires_UtilisateurId",
                table: "ContrainteHoraires",
                column: "UtilisateurId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContrainteHoraires_Apps_AppId",
                table: "ContrainteHoraires",
                column: "AppId",
                principalTable: "Apps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContrainteHoraires_Utilisateurs_UtilisateurId",
                table: "ContrainteHoraires",
                column: "UtilisateurId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContrainteJours_Apps_AppId",
                table: "ContrainteJours",
                column: "AppId",
                principalTable: "Apps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContrainteJours_Utilisateurs_UtilisateurId",
                table: "ContrainteJours",
                column: "UtilisateurId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContrainteSemaines_Apps_AppId",
                table: "ContrainteSemaines",
                column: "AppId",
                principalTable: "Apps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContrainteSemaines_Utilisateurs_UtilisateurId",
                table: "ContrainteSemaines",
                column: "UtilisateurId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StatistiquesUtilisations_Apps_AppId",
                table: "StatistiquesUtilisations",
                column: "AppId",
                principalTable: "Apps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StatistiquesUtilisations_Utilisateurs_UtilisateurId",
                table: "StatistiquesUtilisations",
                column: "UtilisateurId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
