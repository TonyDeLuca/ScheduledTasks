using System;

/// <summary>
/// A MonthlyFrequency is a schedule that repeats on a monthly basis.
/// </summary>
public class MonthlyFrequency : Frequency
{


    private DateTimeExtensions.CalculatedDayType mCalculatedDayType;

    private DateTimeExtensions.CalculcatedDayOption mCalculcatedDayOption;

    private int mDayOfTheMonth;

    private MonthlyFrequencyType mFrequencyType;

    /// <summary>
    /// A MonthlyFrequency task that repeats on a certain day of the month every specified number of months.
    /// </summary>
    /// <param name="repeatInterval">Repeater every X months.</param>
    /// <param name="dayNumber">The day number to repeat.</param>
    public MonthlyFrequency(int repeatInterval, int dayNumber) : base()
    {
        if (repeatInterval < 1 || repeatInterval > 99)
        {
            throw new ArgumentOutOfRangeException("repeatInterval");
        }

        if (dayNumber < 1 || dayNumber > 31)
        {
            throw new ArgumentOutOfRangeException("dayNumber");
        }

        mFrequencyType = MonthlyFrequencyType.DayOfTheMonth;
        mRepeatInterval = repeatInterval;
        mDayOfTheMonth = dayNumber;
    }

    /// <summary>
    /// A MonthlyFrequency task that repeats on a calculated day of the month every specified number of months.
    /// </summary>
    /// <param name="repeatInterval"></param>
    /// <param name="dayType"></param>
    /// <param name="dayOption"></param>
    public MonthlyFrequency(int repeatInterval, DateTimeExtensions.CalculatedDayType dayType, DateTimeExtensions.CalculcatedDayOption dayOption) : base()
    {
        mFrequencyType = MonthlyFrequencyType.CalculatedDay;
        mCalculatedDayType = dayType;
        mCalculcatedDayOption = dayOption;
        mRepeatInterval = repeatInterval;
    }

    /// <summary>
    /// The type of frequency for this type, either a specific day of the month or a calculated rule.
    /// </summary>
    public enum MonthlyFrequencyType
    {
        DayOfTheMonth = 0,
        CalculatedDay
    }

    public DateTimeExtensions.CalculatedDayType CalculatedDayType
    {
        get
        {
            return mCalculatedDayType;
        }
    }
    /// <summary>
    /// The calculated day of the month option, describing the day to select.
    /// </summary>
    public DateTimeExtensions.CalculcatedDayOption CalculcatedDayOption {
        get
        {
            return mCalculcatedDayOption;
        }
    }
    /// <summary>
    /// The day of the month this frequency rule will set, if the monthly frequency type is DayOfTheMonth.
    /// </summary>
    public int DayOfTheMonth
    {
        get
        {
            return mDayOfTheMonth;
        }
    }
    /// <summary>
    /// The type of frequency this MonthlyFrequency describes.
    /// </summary>
    public MonthlyFrequencyType FrequencyType
    {
        get
        {
            return mFrequencyType;
        }
    }
    /// <summary>
    /// Returns the next day valid for this frequency interval after the given day.
    /// </summary>
    /// <param name="afterDay"></param>
    /// <returns></returns>
    public override DateTime GetNextDay(DateTime afterDay)
    {
        return GetNextDay(afterDay, afterDay.AddDays(1));
    }

    /// <summary>
    /// Returns the next day after a specific day that matches this rule that is greater than the minimum day specified.
    /// </summary>
    /// <param name="afterDay">The day this rule was last run on.</param>
    /// <param name="minimumDay">The minimum day that can be returned by this rule.</param>
    /// <returns></returns>
    public override DateTime GetNextDay(DateTime afterDay, DateTime minimumDay)
    {
        //We only care about the 'Day' part, discard time and ensure the minimum day is after the 'afterDay'.
        afterDay = afterDay.Date;
        minimumDay = minimumDay.Date;

        if (minimumDay <= afterDay)
            minimumDay = afterDay.AddDays(1);

        DateTime startMonth = afterDay.StartOfMonth();
        DateTime minMonth = minimumDay.StartOfMonth();

        if (FrequencyType == MonthlyFrequencyType.DayOfTheMonth)
        {
            //For 'Day of the Month' logic:
            //If the day has yet to occur in the specified month, and is > minimumDay, we return that day.
            //If not, we search future months for that date.

            DateTime checkDate = DateTime.MinValue;

            if (startMonth == minMonth
                && afterDay.Date.Day <= DayOfTheMonth
                && DateTime.DaysInMonth(afterDay.Year, afterDay.Month) >= DayOfTheMonth)
            {
                DateTime possibleDay = new DateTime(afterDay.Year, afterDay.Month, DayOfTheMonth);
                if (possibleDay >= minimumDay)
                {
                    checkDate = possibleDay;
                }
            }

            while (checkDate < minimumDay)
            {
                //Calculate the next month and day valid for this rule.
                //Calculate the next valid week that falls on the interval.
                double dateDiff = minMonth.TotalMonths(startMonth);
                int modDiff = (int)dateDiff % RepeatInterval;

                if (modDiff == 0)
                    checkDate = startMonth.AddMonths(RepeatInterval);
                else
                    checkDate = startMonth.AddMonths(-modDiff).AddMonths(RepeatInterval);

                //Check if the next month has enough days;
                if (DateTime.DaysInMonth(checkDate.Year, checkDate.Month) >= DayOfTheMonth)
                {
                    checkDate = new DateTime(checkDate.Year, checkDate.Month, DayOfTheMonth);
                }
                else
                {
                    checkDate = startMonth.AddMonths(RepeatInterval);
                }
            }

            return checkDate;
        }
        else
        {
            DateTime checkDate = afterDay.Date;
            bool foundDate = false;

            while (foundDate == false)
            {
                //This logic is followed for any future months.
                DateTime findDate = checkDate.FindInstanceOfDay(CalculcatedDayOption, CalculatedDayType);
                if (findDate.Date >= minimumDay)
                {
                    checkDate = findDate;
                    foundDate = true;
                }
                else
                {
                    //not enough days in given month.
                    //move to next month.
                    checkDate = checkDate.AddMonths(RepeatInterval);
                }
            }

            return checkDate;
        }
    }

    /// <summary>
    /// Gets a string summary of this rule.
    /// </summary>
    /// <returns></returns>
    public override string GetSummary()
    {
        if (FrequencyType == MonthlyFrequencyType.DayOfTheMonth)
        {
            //Occurs every {0} month(s) on day {1}
            return string.Format("Occurs every {0} month(s) on day {1} of the month", RepeatInterval, DayOfTheMonth);
        }
        else
        {
            return string.Format("Occurs every {0} {1} of every {2} month(s)", CalculatedDayType.ToString().ToLower(), CalculcatedDayOption.ToString(), RepeatInterval);
        }
    }
}