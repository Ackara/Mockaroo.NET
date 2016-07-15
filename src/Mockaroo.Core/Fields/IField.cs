using System.Runtime.Serialization;

namespace Gigobyte.Mockaroo.Fields
{
    /// <summary>
    /// Defines methods and properties that represents a Mockaroo data type.
    /// </summary>
    public interface IField
    {
        /// <summary>
        /// Gets or sets the identifier of this field.
        /// </summary>
        /// <value>The name.</value>
        [DataMember(Name = "name")]
        string Name { get; set; }

        /// <summary>
        /// Gets the data type.
        /// </summary>
        /// <value>The data type.</value>
        [DataMember(Name = "type")]
        DataType Type { get; }

        /// <summary>
        /// Gets or sets the Ruby script to generate data based on custom logic. see more at https://mockaroo.com/help/formulas
        /// </summary>
        /// <value>The formula.</value>
        [DataMember(Name = "formula")]
        string Formula { get; set; }

        /// <summary>
        /// Gets or sets an integer between 0 and 100 that determines what percent of the generated
        /// values will be null
        /// </summary>
        /// <value>The percent blank.</value>
        [DataMember(Name = "percentBlank")]
        int BlankPercentage { get; set; }

        /// <summary>
        /// Converts this instance into its equivalent json representation.
        /// </summary>
        /// <returns>A json representation of the instance.</returns>
        string ToJson();
    }
}