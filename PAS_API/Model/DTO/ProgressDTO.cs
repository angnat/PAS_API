namespace PAS_API.Model.DTO
{
    public class ProgressDTO
    {       
        public Int64? ID
        { get; set; }
        public string? UnitID
        { get; set; }
        public string? ProgressBgn
        { get; set; }
        public string? ProgressLunas
        { get; set; }
        public string? CompanyCode
        { get; set; }
        public DateTime? TanggalProgress
        { get; set; }
        public DateTime? TglRevenue
        { get; set; }
        public DateTime? TglCreateKwitansi
        { get; set; }
        public decimal? OutStandingDenda
        { get; set; }
        public string? Status
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
    }
}
