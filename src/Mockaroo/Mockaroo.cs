using System;

namespace Acklann.Mockaroo
{
    /// <summary>
    /// Exposes the static variables and resources used by the Mockaroo API.
    /// </summary>
    public static class Mockaroo
    {
        /// <summary>
        /// The default date format.
        /// </summary>
        public static readonly string DateFormat = "MM/dd/yyyy";

        /// <summary>
        /// Gets the Mockaroo API endpoint.
        /// </summary>
        /// <param name="apiKey">Your API key.</param>
        /// <param name="records">The the number of records to retrieve.</param>
        /// <param name="format">The response format.</param>
        /// <returns>Uri.</returns>
        public static Uri Endpoint(string apiKey, int records, Format format = Format.JSON)
        {
            return new UriBuilder("https", "www.mockaroo.com")
            {
                Path = $"api/generate.{format}".ToLower(),
                Query = $"key={apiKey}&array=true&count={records}"
            }.Uri;
        }

        /// <summary>
        /// The http://mockaroo.com endpoint query parameters.
        /// </summary>
        public struct QueryStringParameter
        {
            /// <summary>
            /// Your api key.
            /// </summary>
            public const string Key = "key";

            /// <summary>
            /// The number of rows to generate.
            /// </summary>
            /// <remarks>
            /// Defaults to 1, or if a saved schema is used, the number of rows specified on the
            /// saved schema. When json format is requested, the result will be an object when size =
            /// 1, an array when size &gt; 1. Free accounts are limited to 5000 records per download.
            /// </remarks>
            public const string Count = "count";

            /// <summary>
            /// (optional) When generating json data, Mockaroo will return an array when count &gt; 1
            /// and an object when count is 1. To always return an array, specify a value of "true"
            /// for this parameter. Defaults to false.
            /// </summary>
            public const string Array = "array";

            /// <summary>
            /// (optional) When generating json data, Mockaroo will omit keys with null values if
            /// this is set to false.
            /// </summary>
            public const string IncludeNulls = "include_nulls";

            /// <summary>
            /// (optional) Only relevant for csv format. Set to false to omit the header row.
            /// Defaults to true.
            /// </summary>
            public const string IncludeHeader = "include_header";

            /// <summary>
            /// (optional) The name of a saved schema to use. If this parameter is not specified, you
            /// must define the fields in the request body as specified below.
            /// </summary>
            public const string Schema = "schema";

            /// <summary>
            /// (optional, only used when format is xml) The name to give to the root element in the document.
            /// </summary>
            public const string RootElement = "root_element";

            /// <summary>
            ///(optional, only used when format is xml) The name to give to each element representing a record.
            /// </summary>
            public const string RecordElement = "record_element";

            /// <summary>
            /// (optional, only used when format is custom) The character to use as a column separator.
            /// </summary>
            public const string Delimiter = "delimiter";

            /// <summary>
            /// (optional, only used when format is custom) "unix" or "windows".
            /// </summary>
            public const string LineEnding = "line_ending";

            /// <summary>
            /// (optional, only used when format is csv, txt, or custom) true to include the BOM
            /// (byte order mark). Defaults to false.
            /// </summary>
            public const string Bom = "bom";

            /// <summary>
            /// (optional) A json array of field specifications. When using JSONP, you must specify
            /// fields using this URL parameter as JSONP does not allow the caller to pass data in
            /// the body of the request.
            /// </summary>
            public const string Fields = "fields";
        }
    }
}