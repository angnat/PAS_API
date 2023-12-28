using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAS_API.Model
{
    public class CustomerCommunication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64? ID
        { get; set; }
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
