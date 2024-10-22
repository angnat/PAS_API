using Microsoft.EntityFrameworkCore;

namespace PAS_API.Model
{
    [Keyless]
    public class vw_Company_Header
    {
        public string? CompanyCode
        { get; set; }
        public string? CompanyDesc
        { get; set; }
        public string? ProjectCode
        { get; set; }
        public string? ProjectDesc
        { get; set; }
        public string? SBUCode
        { get; set; }
        public string? SBUDesc
        { get; set; }
        public int? IsJV
        { get; set; }
    }
}
