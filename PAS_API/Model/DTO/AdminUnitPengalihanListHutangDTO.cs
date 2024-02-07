namespace PAS_API.Model.DTO
{
    public class AdminUnitPengalihanListHutangDTO
    {
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
    }
}
