namespace Gigobyte.Mockaroo
{
    public enum ResponseFormat
    {
        /// <summary>
        /// Comma separated values. The first row will contain the field names. Subsequent rows will
        /// contain the generated data values.
        /// </summary>
        CSV,
        /// <summary>
        /// Results are returned as a json object. Results will be returned as an array if the
        /// "size" query string parameter is greater than 1.
        /// </summary>
        JSON,
        /// <summary>
        /// Tab-separated values. The first row will contain the field names. Subsequent rows will
        /// contain the generated data values.
        /// </summary>
        TXT
    }
}