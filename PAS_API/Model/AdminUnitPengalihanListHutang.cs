namespace PAS_API.Model
{
    public class AdminUnitPengalihanListHutang
    {
        public Int64? ID
        { get; set; }
        public Int64? FIDPengalihan
        { get; set; }
        public Int64? TypeHutangID
        { get; set; }
        public Int64? AdminUnitID
        { get; set; }
        public decimal? Jumlah
        { get; set; }
        public string? ReffNumber
        { get; set; }
        public decimal? JumlahBayar
        { get; set; }
        public string? Keterangan
        { get; set; }
        public DateTime? TglBayar
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
