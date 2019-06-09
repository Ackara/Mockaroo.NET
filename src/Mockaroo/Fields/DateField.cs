using System;
using System.Globalization;

namespace Acklann.Mockaroo.Fields
{
    public partial class DateField
    {
        internal const string DEFAULT_FORMAT = "MM/dd/yyyy";

        /// <summary>
        /// Gets or sets the minimum date.
        /// </summary>
        /// <value>The minimum date.</value>
        public DateTime Min { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the maximum date.
        /// </summary>
        /// <value>The maximum date.</value>
        public DateTime Max { get; set; } = DateTime.MaxValue;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{BaseJson()},\"min\":\"{Min.Date.ToString(DEFAULT_FORMAT, CultureInfo.InvariantCulture)}\",\"max\":\"{Max.ToString(DEFAULT_FORMAT, CultureInfo.InvariantCulture)}\"}}";
        }

        #region Date Ranges

        /// <summary>
        /// Creates a new <see cref="DateField"/> instance that span from the begining to the end of the day.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="currentDate">The current date.</param>
        /// <returns></returns>
        public static DateField ForToday(string name, DateTime? currentDate = null)
        {
            if (currentDate == null) currentDate = DateTime.UtcNow;

            return new DateField(name)
            {
                Min = Clone(currentDate.Value, 0, 0, 0),
                Max = Clone(currentDate.Value, 23, 59, 59)
            };
        }

        /// <summary>
        /// Creates a new <see cref="DateField"/> instance that span from the begining to the end of the week.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="currentDate">The current date.</param>
        /// <returns></returns>
        public static DateField ForThisWeek(string name, DateTime? currentDate = null)
        {
            if (currentDate == null) currentDate = DateTime.UtcNow;

            return new DateField(name)
            {
                Min = GetFirstDayOfWeek(currentDate.Value),
                Max = GetLastDayOfWeek(currentDate.Value)
            };
        }

        /// <summary>
        /// Creates a new <see cref="DateField"/> instance that span from the begining to the end of the month.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="currentDate">The current date.</param>
        /// <returns></returns>
        public static DateField ForThisMonth(string name, DateTime? currentDate = null)
        {
            if (currentDate == null) currentDate = DateTime.UtcNow;

            return new DateField(name)
            {
                Min = GetFirstDayOfMonth(currentDate.Value),
                Max = GetLastDayOfMonth(currentDate.Value)
            };
        }

        /// <summary>
        /// Creates a new <see cref="DateField"/> instance that span from the begining to the end of the year.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="currentDate">The current date.</param>
        /// <returns></returns>
        public static DateField ForThisYear(string name, DateTime? currentDate = null)
        {
            if (currentDate == null) currentDate = DateTime.UtcNow;

            return new DateField(name)
            {
                Min = GetFirstDayOfYear(currentDate.Value),
                Max = GetLastDayOfYear(currentDate.Value)
            };
        }

        /// <summary>
        /// Gets the first day of week.
        /// </summary>
        /// <param name="currentDate">The current date.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfWeek(DateTime currentDate, CultureInfo culture = null)
        {
            if (culture == null) culture = CultureInfo.CurrentCulture;

            DateTime result = Clone(currentDate);
            DayOfWeek firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;
            while (result.DayOfWeek != firstDayOfWeek)
                result = result.AddDays(-1);

            return result;
        }

        /// <summary>
        /// Gets the last day of week.
        /// </summary>
        /// <param name="currentDate">The current date.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public static DateTime GetLastDayOfWeek(DateTime currentDate, CultureInfo culture = null)
        {
            if (culture == null) culture = CultureInfo.CurrentCulture;

            DateTime result = Clone(currentDate, 23, 59, 59);
            DayOfWeek firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;
            DayOfWeek lastDayOfWeek = (firstDayOfWeek == 0 ? DayOfWeek.Saturday : (firstDayOfWeek - 1));

            while (result.DayOfWeek != lastDayOfWeek)
                result = result.AddDays(1);

            return result;
        }

        /// <summary>
        /// Gets the first day of month.
        /// </summary>
        /// <param name="currentDate">The current date.</param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfMonth(DateTime currentDate)
        {
            return new DateTime(currentDate.Year, currentDate.Month, 1, 0, 0, 0, currentDate.Kind);
        }

        /// <summary>
        /// Gets the last day of month.
        /// </summary>
        /// <param name="currentDate">The current date.</param>
        /// <returns></returns>
        public static DateTime GetLastDayOfMonth(DateTime currentDate)
        {
            return GetFirstDayOfMonth(currentDate).AddMonths(1).AddSeconds(-1);
        }

        /// <summary>
        /// Gets the first day of year.
        /// </summary>
        /// <param name="currentDate">The current date.</param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfYear(DateTime currentDate)
        {
            return new DateTime(currentDate.Year, 1, 1, 0, 0, 0, currentDate.Kind);
        }

        /// <summary>
        /// Gets the last day of year.
        /// </summary>
        /// <param name="currentDate">The current date.</param>
        /// <returns></returns>
        public static DateTime GetLastDayOfYear(DateTime currentDate)
        {
            return GetFirstDayOfYear(currentDate).AddYears(1).AddSeconds(-1);
        }

        internal static DateTime Clone(DateTime anchor, int hour = 0, int min = 0, int sec = 0)
        {
            return new DateTime(anchor.Year, anchor.Month, anchor.Day, hour, min, sec, anchor.Kind);
        }

        #endregion Date Ranges
    }
}