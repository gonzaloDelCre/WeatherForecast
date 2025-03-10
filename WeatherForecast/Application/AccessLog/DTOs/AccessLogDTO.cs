namespace WeatherForecast.Application.AccessLog.DTOs
{
    public class AccessLogDTO
    {
        public int AccessID { get; set; }
        public int? MemberID { get; set; } 
        public DateTime? AccessDateTime { get; set; }
        public string? AccessType { get; set; }
        public string AccessStatus { get; set; }
    }
}
