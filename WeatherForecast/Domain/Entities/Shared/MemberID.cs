namespace WeatherForecast.Domain.Entities.Shared
{
    public class MemberID
    {
        public int Value { get; private set; }

        public MemberID(int value)
        {
            Value = value;
        }
    }

}
