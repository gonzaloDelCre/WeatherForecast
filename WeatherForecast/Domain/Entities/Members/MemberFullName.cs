namespace WeatherForecast.Domain.Entities.Members
{
    public class MemberFullName
    {
        public string Value { get; private set; }

        public MemberFullName(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("El nombre completo no puede estar vacio");
            if (value.Length > 100)
                throw new ArgumentException("El nombre completo es demasiado largo");
            Value = value;
        }
    }
}
