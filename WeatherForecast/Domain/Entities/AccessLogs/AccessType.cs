namespace WeatherForecast.Domain.Entities.AccessLogs
{
    public class AccessType
    {
        public string? Value { get; private set; }

        public AccessType(string? value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("El tipo de acceso no puede estar vacio");
            Value = value;
        }
    }

}
