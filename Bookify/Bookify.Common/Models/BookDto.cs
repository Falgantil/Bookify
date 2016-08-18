﻿using System.Collections.Generic;

namespace Bookify.Common.Models
{
    public class BookDto : BaseDto
    {
        public string Title { get; set; }

        public string Summary { get; set; }

        public decimal Price { get; set; }

        public int PublishYear { get; set; }

        public AuthorDto Author { get; set; }

        public PublisherDto Publisher { get; set; }
        public List<GenreDto> Genres { get; set; }
    }
}