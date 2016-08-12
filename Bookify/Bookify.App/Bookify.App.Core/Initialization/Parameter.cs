namespace Bookify.App.Core.Initialization
{
    /// <summary>
    /// A basic parameter used in constructor injection.
    /// </summary>
    public sealed class Parameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter" /> class.
        /// </summary>
        /// <param name="paramName">Name of the property.</param>
        /// <param name="value">The value.</param>
        public Parameter(string paramName, object value)
        {
            this.ParamName = paramName;
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the name of the parameter that will be used when constructor injecting..
        /// </summary>
        /// <value>
        /// The name of the parameter.
        /// </value>
        public string ParamName { get; set; }

        /// <summary>
        /// Gets or sets the value that will be used when constructor injecting.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public object Value { get; set; }
    }
}