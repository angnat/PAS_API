namespace PAS_API.Model.DTO
{
    public class ClusterDTO
    {
        public Int64? ID
        { get; set; }
        public Int64? FIDProject
        { get; set; }
        public string? ClusterName
        { get; set; }
        public string? SLoc
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
