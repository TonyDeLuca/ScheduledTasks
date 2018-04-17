using System;
using System.Collections.Generic;

/// <summary>
/// A WeeklyFrequency is a schedule that repeats on a weekly basis.
/// </summary>
public class WeeklyFrequency : Frequency
{
    

    //Days of the week are publicly accessible/changeable without negative side-effects
    //no need to make this a property.
    public bool Friday;
    public bool Monday;
    public bool Saturday;
    public bool Sunday;
    public bool Thursday;
    public bool Tuesday;
    public bool Wednesday;

    /// <summary>
    /// A WeeklyFrequency that repeats every specified number of weeks.
    /// </summary>
    /// <param name="repeatInterval">How many weeks apart this task should repeat.</param>
    public WeeklyFrequency(int repeatInterval) : base()
    {
        if (repeatInterval < 1 || repeatInterval > 99)
        {
            throw new ArgumentOutOfRangeException("repeatInterval");
        }

        mRepeatInterval = repeatInterval;
    }

    /// <summary>
    /// Returns the number of days enabled for this rule.
    /// </summary>
    public int EnabledDayCount
    {
        get
        {
            int count = 0;

            if (Monday) count++;
            if (Tuesday) count++;
            if (Wednesday) count++;
            if (Thursday) count++;
            if (Friday) count++;
            if (Saturday) count++;
            if (Sunday) count++;

            return count;
        }
    }

    /// <summary>
    /// A string list of days enabled for this rule.
    /// </summary>
    /// <returns></returns>
    public List<String> GetEnabledDays()
    {
        List<String> retList = new List<string>();
        if (Monday)
            retList.Add("Monday");
        if (Tuesday)
            retList.Add("Tuesday");
        if (Wednesday)
            retList.Add("Wednesday");
        if (Thursday)
            retList.Add("Thursday");
        if (Friday)
            retList.Add("Friday");
        if (Saturday)
            retList.Add("Saturday");
        if (Sunday)
            retList.Add("Sunday");

        return retList;
    }

    /// <summary>
    /// Returns the next day this rule will run on after the specified day.
    /// </summary>
    /// <param name="afterDay">The day this rule was last run on.</param>
    /// <returns></returns>
    public override DateTime GetNextDay(DateTime afterDay)
    {
        return GetNextDay(afterDay, afterDay.AddDays(1));
    }

    /// <summary>
    /// Returns the next day this rule will run on after the specified date and minimum date.
    /// </summary>
    /// <param name="afterDay">The day this rule was last run on.</param>
    /// <param name="minimumDay">The minimum day that can be returned by this rule.</param>
    /// <returns></returns>
    public override DateTime GetNextDay(DateTime afterDay, DateTime minimumDay)
    {
        List<DateTime> validDays = null;

        //We only care about the 'Day' part, discard time and ensure the minimum day is after the 'afterDay'.
        afterDay = afterDay.Date;
        minimumDay = minimumDay.Date;
        if (minimumDay <= afterDay)
            minimumDay = afterDay.AddDays(1);

        DateTime startWeek = afterDay.StartOfWeek(DayOfWeek.Monday);
        DateTime minimumWeek = minimumDay.StartOfWeek(DayOfWeek.Monday);
        DateTime checkWeek = DateTime.MinValue;

        //No days of the week are enabled so the next possible day of execution is the 1st day of the next week
        //that is > min date.

        if (EnabledDayCount == 0)
        {
            if (minimumWeek == startWeek)
            {
                //If we are in the same week, move to the next week
                checkWeek = minimumWeek.AddDays(RepeatInterval * 7);
            }
            else
            {
                //Calculate the next valid week that falls on the interval.
                double dateDiff = (minimumWeek - startWeek).TotalDays;
                double modDiff = dateDiff % (RepeatInterval * 7);

                if (modDiff == 0)
                    checkWeek = minimumWeek;
                else
                    checkWeek = minimumWeek.AddDays(-modDiff).AddDays(RepeatInterval * 7);
            }

            return GetValidDaysInWeek(checkWeek).Find(days => days.Date >= minimumDay);
        }

        //If we got here then specific days are enabled.

        if (minimumWeek == startWeek)
        {
            //Min/Start week are the same...
            checkWeek = startWeek;
        }
        else
        {
            //Min week is > start, calculate next valid week.
            double dateDiff = (minimumWeek - startWeek).TotalDays;
            double modDiff = dateDiff % (RepeatInterval * 7);

            if (modDiff == 0)
                checkWeek = minimumWeek;
            else
                checkWeek = minimumWeek.AddDays(-modDiff).AddDays(RepeatInterval * 7);
        }

        //First check if there are any valid days in the check week that are >= our min date.
        //Get a list of valid days in the presented week
        validDays = GetValidDaysInWeek(checkWeek);

        //Find any valid dates that also meet the minimum date requirement.
        DateTime nextValid = validDays.Find(days => days.Date > afterDay.Date && days.Date >= minimumDay);
        if (nextValid != DateTime.MinValue)
            return nextValid;

        //If none, move to the next valid week.
        checkWeek = checkWeek.AddDays(RepeatInterval * 7);

        //Find any valid days in the new week.
        validDays = GetValidDaysInWeek(checkWeek);

        //Find any valid dates that also meet the minimum date requirement.
        nextValid = validDays.Find(days => days.Date > afterDay.Date && days.Date >= minimumDay);
        if (nextValid != DateTime.MinValue)
            return nextValid;
        else
            throw new Exception("Unable to find a valid date in the given week.");
    }

    /// <summary>
    /// Gets the next enabled day of a given week after the specified day of the week.
    /// </summary>
    /// <param name="startDay">The day to start the search from.</param>
    /// <returns></returns>
    public DayOfWeek? GetNextEnabledDay(DayOfWeek startDay)
    {
        switch (startDay)
        {
            case DayOfWeek.Monday:
                if (Tuesday)
                    return DayOfWeek.Tuesday;
                else
                    goto case DayOfWeek.Tuesday;
            case DayOfWeek.Tuesday:
                if (Wednesday)
                    return DayOfWeek.Wednesday;
                else
                    goto case DayOfWeek.Wednesday;
            case DayOfWeek.Wednesday:
                if (Thursday)
                    return DayOfWeek.Thursday;
                else
                    goto case DayOfWeek.Thursday;
            case DayOfWeek.Thursday:
                if (Friday)
                    return DayOfWeek.Friday;
                else
                    goto case DayOfWeek.Friday;
            case DayOfWeek.Friday:
                if (Saturday)
                    return DayOfWeek.Saturday;
                else
                    goto case DayOfWeek.Saturday;
            case DayOfWeek.Saturday:
                if (Sunday)
                    return DayOfWeek.Sunday;
                else
                    goto case DayOfWeek.Sunday;
            case DayOfWeek.Sunday:
                return null;
        }

        return null;
    }

    /// <summary>
    /// Returns a string summary of this WeeklyFrequency schedule.
    /// </summary>
    /// <returns></returns>
    public override string GetSummary()
    {
        //Occurs every {0} week(s)
        string retString = string.Format("Occurs every {0} week(s)", RepeatInterval);

        //If any days are enabled add "on {days}"
        string daysString = string.Join(", ", GetEnabledDays());
        if (daysString.Length > 0)
        {
            retString += string.Format(" on {0}", daysString);
        }

        return retString;
    }

    /// <summary>
    /// Returns a list of DateTimes that are valid in the given week for this frequency.
    /// </summary>
    /// <param name="week">The week (Monday based) to obtain a list of days for.</param>
    /// <returns></returns>
    public List<DateTime> GetValidDaysInWeek(DateTime week)
    {
        //Convert the passed in date to the first day of that week.
        week = week.StartOfWeek(DayOfWeek.Monday);

        List<DateTime> validDays = new List<DateTime>();

        if (EnabledDayCount == 0)
        {
            for (int i = 0; i < 7; i++)
            {
                validDays.Add(week.AddDays(i));
            }
        }
        else
        {
            if (Monday) validDays.Add(week);
            if (Tuesday) validDays.Add(week.AddDays(1));
            if (Wednesday) validDays.Add(week.AddDays(2));
            if (Thursday) validDays.Add(week.AddDays(3));
            if (Friday) validDays.Add(week.AddDays(4));
            if (Saturday) validDays.Add(week.AddDays(5));
            if (Sunday) validDays.Add(week.AddDays(6));
        }

        return validDays;
    }
}