using Newtonsoft.Json;

namespace Gigobyte.Mockaroo.Fields
{
    [JsonObject]
    public interface IField
    {
        /// <summary>
        /// Gets or sets the identifier of this field.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("name")]
        string Name { get; set; }

        /// <summary>
        /// Gets the data type.
        /// </summary>
        /// <value>The data type.</value>
        [JsonIgnore]
        DataType Type { get; }

        /// <summary>
        /// Gets or sets the Ruby script to generate data based on custom logic. see more at https://mockaroo.com/help/formulas
        /// </summary>
        /// <value>The formula.</value>
        [JsonProperty("formula")]
        string Formula { get; set; }

        /// <summary>
        /// Gets or sets an integer between 0 and 100 that determines what percent of the generated
        /// values will be null
        /// </summary>
        /// <value>The percent blank.</value>
        [JsonProperty("percentBlank")]
        int BlankPercentage { get; set; }
    }
}