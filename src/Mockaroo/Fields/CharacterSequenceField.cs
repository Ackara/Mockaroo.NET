namespace Acklann.Mockaroo.Fields
{
    public partial class CharacterSequenceField
    {
        /// <summary>
        /// Gets or sets the string of characters, digits, and symbols to generate.
        /// <list type="bullet">
        /// <item><description>Use '#' for random digit.</description></item>
        /// <item><description>Use '@' for random lower case letter.</description></item>
        /// <item><description>Use '^' for random upper case letter.</description></item>
        /// <item><description>Use '*' for random digit or letter.</description></item>
        /// <item><description>Use '$' for random digit or lower case letter.</description></item>
        /// <item><description>Use '%' for random digit or upper case letter.</description></item>
        /// <item><description>Any other character will be included verbatim.</description></item>
        /// </list>
        /// </summary>
        /// <example>
        /// ###-##-#### => 232-66-7439
        /// ***-## => A0c-34
        /// ^222-##:### => Cght-87:485
        /// </example>
        public string Format { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Format))
                return base.ToString();
            else
                return $"{BaseJson()},\"format\":\"{Format}\"}}";
        }
    }
}