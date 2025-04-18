﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WeatherForecast.Infrastructure.Persistence.Members.Entities;

namespace WeatherForecast.Infrastructure.Persistence.AccessLogs.Entities
{
    public class AccessLogEntity
    {
        [Key] 
        public int AccessID { get; set; }

        [ForeignKey("Member")]
        public int? MemberID { get; set; }

        [Required]
        public DateTime AccessDateTime { get; set; } = DateTime.UtcNow;

        [StringLength(50)]
        public string? AccessType { get; set; }

        [Required]
        [StringLength(20)]
        public string? AccessStatus { get; set; }

        // Relation with Member
        public virtual MemberEntity? Member { get; set; }
    }

}
