using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HastaneRandevuUygulaması.Migrations
{
    public partial class hastane : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hasta",
                columns: table => new
                {
                    hastaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hastaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hastaLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hasta", x => x.hastaId);
                });

            migrationBuilder.CreateTable(
                name: "il",
                columns: table => new
                {
                    ilId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ilAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_il", x => x.ilId);
                });

            migrationBuilder.CreateTable(
                name: "ilce",
                columns: table => new
                {
                    ilceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ilceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ilID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ilce", x => x.ilceID);
                    table.ForeignKey(
                        name: "FK_ilce_il_ilID",
                        column: x => x.ilID,
                        principalTable: "il",
                        principalColumn: "ilId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hastane",
                columns: table => new
                {
                    hastaneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hastaneAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ilceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hastane", x => x.hastaneId);
                    table.ForeignKey(
                        name: "FK_Hastane_ilce_ilceId",
                        column: x => x.ilceId,
                        principalTable: "ilce",
                        principalColumn: "ilceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Poliklinik",
                columns: table => new
                {
                    poliklinikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    poliklinikName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hastaneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poliklinik", x => x.poliklinikId);
                    table.ForeignKey(
                        name: "FK_Poliklinik_Hastane_hastaneId",
                        column: x => x.hastaneId,
                        principalTable: "Hastane",
                        principalColumn: "hastaneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doktor",
                columns: table => new
                {
                    doktorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doktorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    poliklinikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doktor", x => x.doktorId);
                    table.ForeignKey(
                        name: "FK_Doktor_Poliklinik_poliklinikId",
                        column: x => x.poliklinikId,
                        principalTable: "Poliklinik",
                        principalColumn: "poliklinikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Randevu",
                columns: table => new
                {
                    randevuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    randevuTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    doktorId = table.Column<int>(type: "int", nullable: false),
                    hastaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevu", x => x.randevuId);
                    table.ForeignKey(
                        name: "FK_Randevu_Doktor_doktorId",
                        column: x => x.doktorId,
                        principalTable: "Doktor",
                        principalColumn: "doktorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Randevu_Hasta_hastaId",
                        column: x => x.hastaId,
                        principalTable: "Hasta",
                        principalColumn: "hastaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doktor_poliklinikId",
                table: "Doktor",
                column: "poliklinikId");

            migrationBuilder.CreateIndex(
                name: "IX_Hastane_ilceId",
                table: "Hastane",
                column: "ilceId");

            migrationBuilder.CreateIndex(
                name: "IX_ilce_ilID",
                table: "ilce",
                column: "ilID");

            migrationBuilder.CreateIndex(
                name: "IX_Poliklinik_hastaneId",
                table: "Poliklinik",
                column: "hastaneId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_doktorId",
                table: "Randevu",
                column: "doktorId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_hastaId",
                table: "Randevu",
                column: "hastaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Randevu");

            migrationBuilder.DropTable(
                name: "Doktor");

            migrationBuilder.DropTable(
                name: "Hasta");

            migrationBuilder.DropTable(
                name: "Poliklinik");

            migrationBuilder.DropTable(
                name: "Hastane");

            migrationBuilder.DropTable(
                name: "ilce");

            migrationBuilder.DropTable(
                name: "il");
        }
    }
}
