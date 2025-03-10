using System.ComponentModel.DataAnnotations;

namespace WeatherForecast.Domain.Entities.Members
{
    public class MemberEmail
    {
        public string Value { get; private set; }

        public MemberEmail(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("El correo electronico no puede estar vacio.");
            if (!new EmailAddressAttribute().IsValid(value))
                throw new ArgumentException("El formato del correo electrónico no es valido.");
            Value = value;
        }
    }
}
