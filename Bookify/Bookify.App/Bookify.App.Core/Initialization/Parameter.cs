namespace Bookify.App.Core.Initialization
{
    public sealed class Parameter
    {
        public string PropertyName { get; set; }

        public object Value { get; set; }

        public Parameter(string propertyName, object value)
        {
            this.PropertyName = propertyName;
            this.Value = value;
        }
    }
}