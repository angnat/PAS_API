using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAS_API.Model
{
    public class AdminUnitTeknikSilver
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64? ID
        { get; set; }
        public string? UnitId
        { get; set; }
        public string? CompanyCode
        { get; set; }
        public string? LaporanID
        { get; set; }
        public string? Kesimpulan
        { get; set; }
        public string? CreatedBy
        { get; set; }
        public DateTime? CreatedOn
        { get; set; }
        public string? ModifiedBy
        { get; set; }
        public DateTime? ModifiedOn
        { get; set; }
    }
}
