namespace PAS_API.Model.DTO
{
    public class CreateCustomerCommunicationDTO
    {
        public Int64? FIDCommunicationType
        { get; set; }
        public string? FIDCommunicationInfo
        { get; set; }
        public Int64? FIDCustomer
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
