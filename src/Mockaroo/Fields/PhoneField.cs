namespace Acklann.Mockaroo.Fields
{
    public partial class PhoneField
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneField"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="format">The phone number format.</param>
        public PhoneField(string name, string format = DEFAULT_FORMAT) : base(name)
        {
            Format = format ?? throw new System.ArgumentNullException(nameof(format));
        }

        internal const string DEFAULT_FORMAT = "#-(###)###-####";

        /// <summary>
        /// Gets or sets the phone number format. Must be one of the following
        /// <list type="bullet">
        /// <item><term>###-###-####</term></item>
        /// <item><term>(###) ###-####</term></item>
        /// <item><term>### ### ####</term></item>
        /// <item><term>+# ### ### ####</term></item>
        /// <item><term>+# (###) ###-####</term></item>
        /// <item><term>+#-###-###-####</term></item>
        /// <item><term>#-(###)###-####</term></item>
        /// <item><term>##########</term></item>
        /// </list>
        /// </summary>
        /// <value>The format.</value>
        public string Format { get; set; } = DEFAULT_FORMAT;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{BaseJson()},\"format\":\"{Format}\"}}";
        }
    }
}