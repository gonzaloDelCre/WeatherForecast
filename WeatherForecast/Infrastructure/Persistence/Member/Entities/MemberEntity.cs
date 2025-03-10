using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WeatherForecast.Infrastructure.Persistence.AccessLog.Entities;

namespace WeatherForecast.Infrastructure.Persistence.Member.Entities
{
    public class MemberEntity
    {
        [Key]
        public int MemberID { get; set; }

        [Required]
        [StringLength(100)]
        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [Required]
        [JsonPropertyName("joinDate")]
        public DateTime JoinDate { get; set; } = DateTime.UtcNow;

        //Relation with AccessLog
        public virtual ICollection<AccessLogEntity> AccessLogs { get; set; } = new List<AccessLogEntity>();

    }
}
