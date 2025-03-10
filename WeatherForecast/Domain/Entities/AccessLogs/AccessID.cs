namespace WeatherForecast.Domain.Entities.AccessLogs
{
    public class AccessID
    {
        public int Value { get; private set; }

        public AccessID(int value)
        {
            if (value <= 0) throw new ArgumentException("El ID de acceso debe ser mayor que 0");
            Value = value;
        }
    }

}
