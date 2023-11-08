using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAS_API.Model
{
    public class TUnit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64? ID
        { get; set; }     
        public Int64? FUnit
        { get; set; }
        [ForeignKey("FUnit")]
        public Unit Unit
        { get; set; }
        public Int64? FIDCluster
        { get; set; }
        public string? MaterialIDSAP
        { get; set; }
        public string? CustomerID
        { get; set; }
        public string? SONumber
        { get; set; }
        public DateTime? TglSO
        { get; set; }
        public string? NoPPJB
        { get; set; }
        public DateTime? TglPpjb
        { get; set; }
        public DateTime? TglSewa
        { get; set; }
        public string? PeriodeSewa
        { get; set; }
        public string? CaraBayar
        { get; set; }
        public string? CaraBayarDescription
        { get; set; }
        public string? NamaBank
        { get; set; }
        public decimal? NilaiKpr
        { get; set; }
        public decimal? BiayaAdmin
        { get; set; }
        public decimal? Discount
        { get; set; }
        public decimal? LuasTanah
        { get; set; }
        public decimal? LuasBangunan
        { get; set; }
        public decimal? Tnh_mtr
        { get; set; }
        public decimal? Bgn_mtr
        { get; set; }
        public decimal? Dpp_tnh
        { get; set; }
        public decimal? Dpp_bgn
        { get; set; }
        public decimal? Ppn_tnh
        { get; set; }
        public decimal? Ppn_bgn
        { get; set; }
        public decimal? HargaJualNet
        { get; set; }
        public string? FlagStatus
        { get; set; }
        public DateTime? TglRencanaSerahTerima
        { get; set; }
        public string? Status
        { get; set; }
        public string? OrderCancellation
        { get; set; }
        public string? KelasBumi
        { get; set; }
        public string? KelasBangunan
        { get; set; }
        public DateTime? TglPembukuan
        { get; set; }
        public string? PromoCode
        { get; set; }
        public string? NoAdendumPPJB
        { get; set; }
        public DateTime? TglAdendumPPJB
        { get; set; }
        public string? CreatedBy
        { get; set; }
        public DateTime? CreatedDate
        { get; set; }
        public string? CreatedHost
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
