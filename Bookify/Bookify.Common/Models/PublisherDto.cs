namespace Bookify.Common.Models
{
    public class PublisherDto : BaseDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PublisherDto"/> is trusted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if trusted; otherwise, <c>false</c>.
        /// </value>
        public bool Trusted { get; set; }
    }
}