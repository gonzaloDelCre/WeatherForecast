namespace WeatherForecast.Domain.Entities.AccessLogs
{
    public class AccessStatus
    {
        public string Value { get; private set; }

        public AccessStatus(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("El estado de acceso no puede estar vacio");
            Value = value;
        }
    }

}
