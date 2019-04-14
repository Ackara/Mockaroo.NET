namespace Acklann.Mockaroo.Fields
{
    public partial class DummyImageURLField
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DummyImageURLField"/> class.
        /// </summary>
        /// <param name="height">The height in pixels.</param>
        /// <param name="width">The width pixels.</param>
        public DummyImageURLField(string name, int height, int width) : base(name)
        {
            MinWidth = width;
            MaxWidth = width;
            MinHeight = height;
            MaxHeight = height;
        }

        /// <summary>
        /// Gets or sets the minimum height in pixels.
        /// </summary>
        /// <value>
        /// The minimum height.
        /// </value>
        public int MinHeight { get; set; } = 64;

        /// <summary>
        /// Gets or sets the maximum height in pixels.
        /// </summary>
        /// <value>
        /// The maximum height.
        /// </value>
        public int MaxHeight { get; set; } = 256;

        /// <summary>
        /// Gets or sets the minimum width in pixels.
        /// </summary>
        /// <value>
        /// The minimum width.
        /// </value>
        public int MinWidth { get; set; } = 64;

        /// <summary>
        /// Gets or sets the maximum width in pixels.
        /// </summary>
        /// <value>
        /// The maximum width.
        /// </value>
        public int MaxWidth { get; set; } = 256;

        /// <summary>
        /// Gets or sets the image format. Must be()
        /// </summary>
        /// <value>
        /// The format.
        /// </value>
        public DummyImageURLFormat Format { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{BaseJson()},\"minHeight\":{MinHeight},\"maxHeight\":{MaxHeight},\"minWidth\":{MinWidth},\"maxWidth\":{MaxWidth},\"format\":\"{ConvertToString(Format)}\"}}";
        }

        internal static string ConvertToString(DummyImageURLFormat format)
        {
            switch (format)
            {
                default:
                case DummyImageURLFormat.Random:
                    return "random";

                case DummyImageURLFormat.PNG:
                    return "png";

                case DummyImageURLFormat.GIF:
                    return "gif";

                case DummyImageURLFormat.JPG:
                    return "jpg";
            }
        }
    }
}