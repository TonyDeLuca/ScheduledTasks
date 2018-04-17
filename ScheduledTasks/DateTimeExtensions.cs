using System;


/// <summary>
/// DateTimeExtensions provide additional functionality to DateTime objects useful for this class library.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Describes the instance of a day to find in a given month.
    /// </summary>
    public enum CalculatedDayType
    {
        First = 1,
        Second,
        Third,
        Fourth,
        Last
    }

    /// <summary>
    /// Describes the day type to find in a given month.
    /// </summary>
    public enum CalculcatedDayOption
    {
        Monday = 0,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday,
        Day,
        Weekday,
        WeekendDay
    }

    /// <summary>
    /// Calculates specific date rules for the given month.
    /// </summary>
    /// <param name="dt">The month and year to use.</param>
    /// <param name="dayOption">The option we are calculating.</param>
    /// <param name="instance">The instance of the option being calculated.</param>
    /// <returns>The date of the instance of this option.</returns>
    public static DateTime FindInstanceOfDay(this DateTime dt, CalculcatedDayOption dayOption, CalculatedDayType instance)
    {
        //The instance number corresponds to
        int instanceNumber = (int)instance;
        DateTime searchDay = new DateTime(dt.Year, dt.Month, 1);

        int daysInMonth = DateTime.DaysInMonth(dt.Year, dt.Month);

        if (dayOption == CalculcatedDayOption.Monday || dayOption == CalculcatedDayOption.Tuesday ||
            dayOption == CalculcatedDayOption.Wednesday || dayOption == CalculcatedDayOption.Thursday ||
            dayOption == CalculcatedDayOption.Friday || dayOption == CalculcatedDayOption.Saturday ||
            dayOption == CalculcatedDayOption.Sunday)
        {
            //Converting to DayOfWeek used by DateTime.

            DayOfWeek dayOfWeek = DayOfWeek.Monday;
            switch (dayOption)
            {
                case CalculcatedDayOption.Monday:
                    dayOfWeek = DayOfWeek.Monday;
                    break;

                case CalculcatedDayOption.Tuesday:
                    dayOfWeek = DayOfWeek.Tuesday;
                    break;

                case CalculcatedDayOption.Wednesday:
                    dayOfWeek = DayOfWeek.Wednesday;
                    break;

                case CalculcatedDayOption.Thursday:
                    dayOfWeek = DayOfWeek.Thursday;
                    break;

                case CalculcatedDayOption.Friday:
                    dayOfWeek = DayOfWeek.Friday;
                    break;

                case CalculcatedDayOption.Saturday:
                    dayOfWeek = DayOfWeek.Saturday;
                    break;

                case CalculcatedDayOption.Sunday:
                    dayOfWeek = DayOfWeek.Monday;
                    break;
            }

            for (int i = 1; i <= 7; i++)
            {
                if (searchDay.DayOfWeek == dayOfWeek)
                {
                    //Found the first instance.

                    //If there are not at least 28 days after the first instance
                    //then max instance is 4 otherwise it is 5.
                    if (instanceNumber >= 5)
                    {
                        int instanceDay = searchDay.Day;

                        if (daysInMonth - instanceDay < 28)
                        {
                            instanceNumber = 4;
                        }
                        else
                        {
                            instanceNumber = 5;
                        }
                    }
                    return searchDay.AddDays(7 * (instanceNumber - 1));
                }

                //Move to the next day.
                searchDay = searchDay.AddDays(1);
            }
        }
        else if (dayOption == CalculcatedDayOption.Day)
        {
            if (instanceNumber < 5)
            {
                return searchDay.AddDays(instanceNumber - 1);
            }
            else
            {
                //last day of the month
                return searchDay.AddMonths(1).AddDays(-1);
            }
        }
        else if (dayOption == CalculcatedDayOption.Weekday)
        {
            if (instanceNumber < 5)
            {
                int instanceCount = 0;
                for (int i = 1; i <= daysInMonth; i++)
                {
                    //Counting each instance of a weekday.
                    if (searchDay.DayOfWeek == DayOfWeek.Monday || searchDay.DayOfWeek == DayOfWeek.Tuesday
                        || searchDay.DayOfWeek == DayOfWeek.Wednesday || searchDay.DayOfWeek == DayOfWeek.Thursday
                        || searchDay.DayOfWeek == DayOfWeek.Friday)
                    {
                        instanceCount++;
                    }

                    if (instanceCount == instanceNumber)
                    {
                        //we found the nth instance of this weekend day.
                        return searchDay;
                    }

                    searchDay = searchDay.AddDays(1);
                }
            }
            else
            {
                //last weekend day of the month
                DateTime lastInstance = new DateTime();
                for (int i = 1; i <= daysInMonth; i++)
                {
                    //Counting each instance of a weekeday.
                    if (searchDay.DayOfWeek == DayOfWeek.Monday || searchDay.DayOfWeek == DayOfWeek.Tuesday
                        || searchDay.DayOfWeek == DayOfWeek.Wednesday || searchDay.DayOfWeek == DayOfWeek.Thursday
                        || searchDay.DayOfWeek == DayOfWeek.Friday)
                    {
                        lastInstance = searchDay;
                    }
                    searchDay = searchDay.AddDays(1);
                }

                return lastInstance;
            }
        }
        else if (dayOption == CalculcatedDayOption.WeekendDay)
        {
            if (instanceNumber < 5)
            {
                int instanceCount = 0;
                for (int i = 1; i < daysInMonth; i++)
                {
                    //Counting each instance of saturday or sunday.
                    if (searchDay.DayOfWeek == DayOfWeek.Saturday || searchDay.DayOfWeek == DayOfWeek.Sunday)
                    {
                        instanceCount++;
                    }

                    if (instanceCount == instanceNumber)
                    {
                        //we found the nth instance of this weekend day.
                        return searchDay;
                    }

                    searchDay = searchDay.AddDays(1);
                }
            }
            else
            {
                //last weekend day of the month
                DateTime lastInstance = new DateTime();
                for (int i = 1; i < daysInMonth; i++)
                {
                    //Counting each instance of saturday or sunday.
                    if (searchDay.DayOfWeek == DayOfWeek.Saturday || searchDay.DayOfWeek == DayOfWeek.Sunday)
                    {
                        lastInstance = searchDay;
                    }
                    searchDay = searchDay.AddDays(1);
                }

                return lastInstance;
            }
        }

        return new DateTime();
    }

    /// <summary>
    /// Sets the time of a DateTime object to a specified time.
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="hours"></param>
    /// <param name="minutes"></param>
    /// <param name="seconds"></param>
    /// <param name="milliseconds"></param>
    /// <returns></returns>
    public static DateTime SetTime(this DateTime dt, int hours, int minutes, int seconds, int milliseconds)
    {
        DateTime retVal = dt.Date;
        retVal = retVal.AddHours(hours);
        retVal = retVal.AddMinutes(minutes);
        retVal = retVal.AddSeconds(seconds);
        retVal = retVal.AddMilliseconds(milliseconds);
        return retVal;
    }

    /// <summary>
    /// Returns the date and time without any ticks after the millisecond.
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static DateTime StartOfMillisecond(this DateTime dt)
    {
        return dt.AddTicks(-(dt.Ticks % 10000000));
    }

    /// <summary>
    /// Returns the first day of the month.
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static DateTime StartOfMonth(this DateTime dt)
    {
        int diff = dt.Day - 1;
        return dt.AddDays(-diff).Date;
    }

    /// <summary>
    /// Returns the first day of the week specified..
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="startOfWeek">The day of the week considered the first day of the week.</param>
    /// <returns></returns>
    public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
    {
        int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        return dt.AddDays(-1 * diff).Date;
    }

    /// <summary>
    /// The total months between two dates.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static int TotalMonths(this DateTime start, DateTime end)
    {
        return (start.Year * 12 + start.Month) - (end.Year * 12 + end.Month);
    }
}