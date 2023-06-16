using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAS_API.Model
{
    public class Unit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ID
        { get; set; }       
        public string UnitID
        { get; set; }
        public string UnitIDOld
        { get; set; }       
        public Int64 FIDCluster
        { get; set; }
        //public string ClusterCode
        //{ get; set; }
        public string? UnitDescription
        { get; set; }
        public string? Block
        { get; set; }
        public string? BlockDesc
        { get; set; }
        public string? Number
        { get; set; }
        public string? ModelUnit
        { get; set; }
        public string? UoMeasure
        { get; set; }
        public string? Watt
        { get; set; }
        public string? Size
        { get; set; }
        public string? Lantai
        { get; set; }
        public string? Hadap
        { get; set; }
        public string? HadapDesc
        { get; set; }
        public string? Sbu
        { get; set; }
        public string? SbuDesc
        { get; set; }
        public string? MataAngin
        { get; set; }
        public string? Sektor
        { get; set; }
        public string? Status
        { get; set; }
        public string? BACode
        { get; set; }
        public string? BADesc
        { get; set; }
        public string? SalesOrg
        { get; set; }
        public string? Tower
        { get; set; }
        public string? Floor
        { get; set; }
        public string? MaterialNumber
        { get; set; }
        public string? DayaListrik
        { get; set; }
        public decimal? LuasNett
        { get; set; }
        public string? NoPUPR
        { get; set; }
        public string? SLoc
        { get; set; }
        public string? SLocDescription
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
