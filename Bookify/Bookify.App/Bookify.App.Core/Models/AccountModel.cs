namespace Bookify.App.Core.Models
{
    public class AccountModel : BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsSubscribed { get; set; }
    }
}