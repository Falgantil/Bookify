using System.Collections.Generic;

namespace Bookify.Common.Models
{
    public class DetailedBookDto : BookDto
    {
        /// <summary>
        /// Gets or sets the feedback.
        /// </summary>
        /// <value>
        /// The feedback.
        /// </value>
        public IEnumerable<BookFeedbackDto> Feedback { get; set; }

        /// <summary>
        /// Gets or sets the orders.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        public IEnumerable<BookOrderDto> Orders { get; set; }

        /// <summary>
        /// Gets or sets the history.
        /// </summary>
        /// <value>
        /// The history.
        /// </value>
        public IEnumerable<BookHistoryDto> History { get; set; }
    }
}