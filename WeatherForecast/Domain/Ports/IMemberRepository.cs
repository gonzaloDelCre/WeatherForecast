using WeatherForecast.Domain.Entities.Member;

namespace WeatherForecast.Domain.Ports
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetAllAsync();
        Task<Member?> GetByIdAsync(int id);
        Task<Member> AddAsync(Member member);
        Task<bool> DeleteAsync(int id);
    }
}
