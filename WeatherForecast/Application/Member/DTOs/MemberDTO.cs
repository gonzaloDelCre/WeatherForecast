namespace WeatherForecast.Application.Member.DTOs
{
    public class MemberDTO
    {
        public int MemberID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
