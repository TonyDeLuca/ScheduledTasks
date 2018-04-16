using System;

/// <summary>
/// A DailyFrequency is a schedule that repeats on a monthly basis.
/// </summary>
public class DailyFrequency : Frequency
{
    /// <summary>
    /// A DailyFrequency task that repeats every given number of days.
    /// </summary>
    /// <param name="repeatInterval">Repeats every given number of days.</param>
    public DailyFrequency(int repeatInterval) : base()
    {
        if (repeatInterval < 1 || repeatInterval > 99)
        {
            throw new ArgumentOutOfRangeException("repeatInterval");
        }

        RepeatInterval = repeatInterval;
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
    /// Returns the next day valid for this frequency interval after the given day.
    /// </summary>
    /// <param name="afterDay">The day used as the basis for this frequency interval.</param>
    /// <param name="minimumDay">The minimum possible day returnable.</param>
    /// <returns></returns>
    public override DateTime GetNextDay(DateTime afterDay, DateTime minimumDay)
    {
        //We only care about the 'Day' part, discard time and ensure the minimum day is after the 'afterDay'.
        afterDay = afterDay.Date;
        minimumDay = minimumDay.Date;
        if (minimumDay <= afterDay)
            minimumDay = afterDay.AddDays(1);

        double dateDiff = (minimumDay - afterDay).TotalDays;
        double modDiff = dateDiff % RepeatInterval;
        return minimumDay.AddDays(modDiff);
    }

    /// <summary>
    /// Gets a string describing this DailyFrequency.
    /// </summary>
    /// <returns></returns>
    public override string GetSummary()
    {
        //Occurs every {0} day(s)
        return string.Format("Occurs every {0} day(s)", RepeatInterval);
    }
}