using System;

/// <summary>
/// A Frequency is used to describe something that repeats on a predictable schedule.
/// </summary>
public abstract class Frequency
{
    /// <summary>
    /// The RepeatInterval tells how often any given Frequency should repeat, controlled by the abstract method of GetNextDay.
    /// </summary>
    public int RepeatInterval
    {
        get
        {
            return mRepeatInterval;
        }
    }

    /// <summary>
    /// Accessed by RepeatInterval property.
    /// </summary>
    protected int mRepeatInterval = 0;

    /// <summary>
    /// An abstract function that must be implemented to return the next day a Frequency repeats after the specified day.
    /// </summary>
    /// <param name="afterDay">The day a frequency may have been last run.</param>
    /// <returns></returns>
    public abstract DateTime GetNextDay(DateTime afterDay);

    /// <summary>
    /// An abstract function that must be implemented to return the next day a Frequency repeats after the specified day and above a minimum date.
    /// </summary>
    /// <param name="afterDay">The day a frequency may have been last run.</param>
    /// <param name="minimumDay">The minimum date that may be returned that matches the rule.</param>
    /// <returns></returns>
    public abstract DateTime GetNextDay(DateTime afterDay, DateTime minimumDay);

    /// <summary>
    /// Returns a string summary describing this Frequency.
    /// </summary>
    /// <returns></returns>
    public abstract string GetSummary();
}