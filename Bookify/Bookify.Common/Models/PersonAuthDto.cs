namespace Bookify.Common.Models
{
    public class PersonAuthDto
    {
        public PersonDto PersonDto { get; set; }

        public AuthTokenDto AuthTokenDto { get; set; }
    }
}