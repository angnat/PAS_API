using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAS_API.Model
{
    public class AdminUnitTeknik
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64? ID
        { get; set; }
        public string? IDKontraktor
        { get; set; }
        public Int64? FIDAdminUnit
        { get; set; }
        [ForeignKey("FIDAdminUnit")]
        public Unit Unit
        { get; set; }
        public string? NamaKontraktor
        { get; set; }
        public decimal? Panjang
        { get; set; }
        public decimal? Lebar
        { get; set; }
        public decimal? LuasTanah
        { get; set; }
        public decimal? LuasBangunan
        { get; set; }
        public string? Sektor
        { get; set; }
        public string? Type
        { get; set; }
        public string? NoSP3
        { get; set; }
        public string? ContactPerson
        { get; set; }
        public string? NoPLN
        { get; set; }
        public string? Daya
        { get; set; }
        public DateTime? TglPasang
        { get; set; }
        public string? IDMeterAir
        { get; set; }
        public bool? MeterAir
        { get; set; }
        public bool? PLNDibebankan
        { get; set; }
        public bool? SiapBAST
        { get; set; }
        public DateTime? TglAirTerpasang
        { get; set; }
        public DateTime? TglRencanaSelesai
        { get; set; }
        public DateTime? TglRealisasiSelesai
        { get; set; }
        public bool? FinalChecklist
        { get; set; }
        public bool? VTComplete
        { get; set; }
        public bool? QAComplete
        { get; set; }
        public string? ValidQABy
        { get; set; }
        public DateTime? TglQAComplate
        { get; set; }
        public DateTime? TglValidTeknik
        { get; set; }
        public string? ValidTeknikBy
        { get; set; }
        public string? NoSPK
        { get; set; }
        public DateTime? TglSPK
        { get; set; }
        public string? CreatedBy
        { get; set; }
        public DateTime? CreatedDate
        { get; set; }
        public string ?CreatedHost
        { get; set; }
        public string? ModifiedBy
        { get; set; }
        public DateTime? ModifiedDate
        { get; set; }
        public string? ModifiedHost
        { get; set; }
        public string? HistoryData
        { get; set; }
    }
}
