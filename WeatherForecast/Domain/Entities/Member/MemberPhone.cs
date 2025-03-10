using System.ComponentModel.DataAnnotations;

namespace WeatherForecast.Domain.Entities.Member
{
    public class MemberPhone
    {
        public string? Value { get; private set; }

        public MemberPhone(string? value)
        {
            if (!string.IsNullOrEmpty(value) && !new PhoneAttribute().IsValid(value))
                throw new ArgumentException("El formato del telefono no es valido");
            Value = value;
        }
    }
}
