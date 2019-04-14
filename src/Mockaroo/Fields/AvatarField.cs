namespace Acklann.Mockaroo.Fields
{
    public partial class AvatarField
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AvatarField"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="size">The size in pixels.</param>
        public AvatarField(string name, int size) : this(name, size, size)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AvatarField"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="width">The width in pixels.</param>
        /// <param name="height">The height in pixels.</param>
        public AvatarField(string name, int width, int height) : base(name)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Gets or sets the image height in pixels.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height { get; set; } = 50;

        /// <summary>
        /// Gets or sets the image width in pixels.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width { get; set; } = 50;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{BaseJson()},\"height\":{Height},\"width\":{Width}}}";
        }
    }
}