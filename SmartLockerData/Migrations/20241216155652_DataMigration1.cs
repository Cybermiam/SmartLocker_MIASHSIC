using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLockerWindows.Migrations
{
    /// <inheritdoc />
    public partial class DataMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContrainteHoraires",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false),
                    AppId = table.Column<int>(type: "int", nullable: false),
                    MaxTime = table.Column<int>(type: "int", nullable: false),
                    BlockTime = table.Column<int>(type: "int", nullable: false),
                    UsedTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContrainteHoraires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContrainteHoraires_Apps_AppId",
                        column: x => x.AppId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContrainteHoraires_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContrainteJours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false),
                    AppId = table.Column<int>(type: "int", nullable: false),
                    MaxTime = table.Column<int>(type: "int", nullable: false),
                    UsedTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContrainteJours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContrainteJours_Apps_AppId",
                        column: x => x.AppId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContrainteJours_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContrainteSemaines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false),
                    AppId = table.Column<int>(type: "int", nullable: false),
                    MondayTime = table.Column<int>(type: "int", nullable: false),
                    TuesdayTime = table.Column<int>(type: "int", nullable: false),
                    WednesdayTime = table.Column<int>(type: "int", nullable: false),
                    ThursdayTime = table.Column<int>(type: "int", nullable: false),
                    FridayTime = table.Column<int>(type: "int", nullable: false),
                    SaturdayTime = table.Column<int>(type: "int", nullable: false),
                    SundayTime = table.Column<int>(type: "int", nullable: false),
                    UsedTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContrainteSemaines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContrainteSemaines_Apps_AppId",
                        column: x => x.AppId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContrainteSemaines_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatistiquesUtilisations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<TimeSpan>(type: "time", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false),
                    AppId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatistiquesUtilisations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatistiquesUtilisations_Apps_AppId",
                        column: x => x.AppId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatistiquesUtilisations_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContrainteHoraires_AppId",
                table: "ContrainteHoraires",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_ContrainteHoraires_UtilisateurId",
                table: "ContrainteHoraires",
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
                name: "IX_ContrainteSemaines_AppId",
                table: "ContrainteSemaines",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_ContrainteSemaines_UtilisateurId",
                table: "ContrainteSemaines",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_StatistiquesUtilisations_AppId",
                table: "StatistiquesUtilisations",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_StatistiquesUtilisations_UtilisateurId",
                table: "StatistiquesUtilisations",
                column: "UtilisateurId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContrainteHoraires");

            migrationBuilder.DropTable(
                name: "ContrainteJours");

            migrationBuilder.DropTable(
                name: "ContrainteSemaines");

            migrationBuilder.DropTable(
                name: "StatistiquesUtilisations");

            migrationBuilder.DropTable(
                name: "Apps");

            migrationBuilder.DropTable(
                name: "Utilisateurs");
        }
    }
}
