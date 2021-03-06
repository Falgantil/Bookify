﻿namespace Bookify.Common.Models
{
    public class PersonDto : BaseDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int? PublisherId { get; set; }

        public string PublisherName { get; set; }

        public bool IsSubscribed { get; set; }
        public string Alias { get; set; }
    }
}
