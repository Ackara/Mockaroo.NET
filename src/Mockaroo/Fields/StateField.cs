namespace Acklann.Mockaroo.Fields
{
    public partial class StateField
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateField"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="onlyUsPlaces">if set to <c>true</c> restrict locations to the United States.</param>
        public StateField(string name, bool onlyUsPlaces) : base(name)
        {
            OnlyUSPlaces = onlyUsPlaces;
        }

        /// <summary>
        /// Determines wether to restrict locations to the United States.
        /// </summary>
        /// <value><c>true</c> if restrict locations to the United States; otherwise, <c>false</c>.</value>
        public bool OnlyUSPlaces { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{BaseJson()},\"onlyUSPlaces\":{(OnlyUSPlaces ? "true" : "false")}}}";
        }
    }
}