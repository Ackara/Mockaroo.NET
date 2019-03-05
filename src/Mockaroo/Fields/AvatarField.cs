namespace Acklann.Mockaroo.Fields
{
    public partial class AvatarField
    {
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