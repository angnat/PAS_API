﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAS_API.Model
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64? ID
        { get; set; }
        public Int64? FIDDivision
        { get; set; }
        public string? ProjectName
        { get; set; }
        public string? FileNameProjectData
        { get; set; }
        public string? ProjectCode
        { get; set; }
        public string? Server
        { get; set; }
        public string? ProjectGroup
        { get; set; }
        public bool? ExcludedVT
        { get; set; }
        public string? CreatedBy
        { get; set; }
        public DateTime? CreatedOn
        { get; set; }
        public string? CreatedHost
        { get; set; }
        public string? ModifiedBy
        { get; set; }
        public DateTime? ModifiedOn
        { get; set; }
        public string? ModifiedHost
        { get; set; }
    }
}
