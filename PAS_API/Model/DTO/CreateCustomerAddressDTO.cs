namespace PAS_API.Model.DTO
{
    public class CreateCustomerAddressDTO
    {
        public Int64? FIDCustomer
        { get; set; }
        public string FIDAddressType
        { get; set; }
        public string? City
        { get; set; }
        public string? Street
        { get; set; }
        public string? Postal
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
