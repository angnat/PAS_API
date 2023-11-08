using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PAS_API.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToUnitTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblM_Unit",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitIDOld = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FIDCluster = table.Column<long>(type: "bigint", nullable: false),
                    UnitDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Block = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlockDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModelUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UoMeasure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Watt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lantai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hadap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HadapDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sbu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SbuDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MataAngin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sektor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BACode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BADesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalesOrg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tower = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Floor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DayaListrik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LuasNett = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NoPUPR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SLoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SLocDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedHost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedHost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HistoryData = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblM_Unit", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblT_Unit",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FUnit = table.Column<long>(type: "bigint", nullable: true),
                    FIDCluster = table.Column<long>(type: "bigint", nullable: true),
                    MaterialIDSAP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SONumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TglSO = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NoPPJB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TglPpjb = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TglSewa = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PeriodeSewa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaraBayar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaraBayarDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamaBank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NilaiKpr = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BiayaAdmin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LuasTanah = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LuasBangunan = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Tnh_mtr = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Bgn_mtr = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Dpp_tnh = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Dpp_bgn = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Ppn_tnh = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Ppn_bgn = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HargaJualNet = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FlagStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TglRencanaSerahTerima = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderCancellation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KelasBumi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KelasBangunan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TglPembukuan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PromoCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoAdendumPPJB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TglAdendumPPJB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedHost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedHost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HistoryData = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblT_Unit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblT_Unit_tblM_Unit_FUnit",
                        column: x => x.FUnit,
                        principalTable: "tblM_Unit",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblT_Unit_FUnit",
                table: "tblT_Unit",
                column: "FUnit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblT_Unit");

            migrationBuilder.DropTable(
                name: "tblM_Unit");
        }
    }
}
