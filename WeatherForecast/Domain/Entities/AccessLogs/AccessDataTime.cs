namespace WeatherForecast.Domain.Entities.AccessLogs
{
    public class AccessDateTime
    {
        public DateTime Value { get; private set; }

        public AccessDateTime(DateTime value)
        {
            if (value == default)
                throw new ArgumentException("La fecha y hora del acceso no pueden ser nulas");
            Value = value;
        }
    }

}
