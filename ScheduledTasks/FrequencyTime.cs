using System;

/// <summary>
/// FrequencyTime describes when a job should run on a specific day.
/// </summary>
public class FrequencyTime
{

    private DateTime mEndTime;

    private int mIntervalTime;

    private FrequencyTimeUnit mIntervalTimeType;

    private DateTime mOccursAt;

    private DateTime mStartTime;

    private FrequencyTimeType mTimeType;

    /// <summary>
    /// Creates a FrequencyTime that occurs at a specified time.
    /// </summary>
    /// <param name="occursAt">The time this frequency will run at.</param>
    public FrequencyTime(DateTime occursAt)
    {
        mTimeType = FrequencyTimeType.OccursOnce;
        mOccursAt = occursAt;
    }

    /// <summary>
    /// Creates a FrequencyTime that occurs multiple times on the same day following a certain rule.
    /// </summary>
    /// <param name="intervalTime">How often this interval repeats.</param>
    /// <param name="intervalTimeType">The type of repeat, e.g. hours, minutes or seconds.</param>
    /// <param name="startingTime">The time this rule should run at.</param>
    /// <param name="endingTime">The time this rule will stop running at.</param>
    public FrequencyTime(int intervalTime, FrequencyTimeUnit intervalTimeType, DateTime startingTime, DateTime endingTime)
    {
        mTimeType = FrequencyTimeType.OccursEvery;
        mIntervalTime = intervalTime;
        mIntervalTimeType = intervalTimeType;
        mStartTime = startingTime;
        mEndTime = endingTime;
    }

    /// <summary>
    /// A FrequencyTimeType describes how often this interval recurs.
    /// </summary>
    public enum FrequencyTimeType
    {
        OccursOnce = 0,
        OccursEvery
    }

    /// <summary>
    /// A FrequencyTimeUnit describes the unit of time used to describe how often an interval recurs.
    /// </summary>
    public enum FrequencyTimeUnit
    {
        Hours = 0,
        Minutes,
        Seconds
    }

    /// <summary>
    /// The last possible time this job may occur on a given day when using the OccursEvery rule.
    /// </summary>
    public DateTime EndTime
    {
        get { return mEndTime; }

    }

    /// <summary>
    /// How often this interval will recur on a given day, modified by FrequencyTimeUnit. Used by the OccursEvery rule.
    /// </summary>
    public int IntervalTime { get { return mIntervalTime; } }

    /// <summary>
    // The unit of time used to calculate the next interval for the OccursEvery interval type.
    /// </summary>
    public FrequencyTimeUnit IntervalTimeType
    {
        get
        {
            return mIntervalTimeType;
        }
    }

    /// <summary>
    /// The time the interval will occur at on any given day.
    /// </summary>
    public DateTime OccursAt
    {
        get
        {
            return mOccursAt;
        }
    }

    /// <summary>
    /// The first possible time this job will occur on a given day when using the OccursEvery rule.
    /// </summary>
    public DateTime StartTime
    {
        get
        {
            return mStartTime;
        }
    }

    /// <summary>
    /// Describes how often this interval ocurs on a given day.
    /// </summary>
    public FrequencyTimeType TimeType
    {
        get
        {
            return mTimeType;
        }
    }

    /// <summary>
    /// Returns the next available time to execute the specified daily frequency on the given day.
    /// </summary>
    /// <param name="day">The day to find a valid execution time on.</param>
    /// <param name="minDate">The minimum date that is acceptable.</param>
    /// <returns>A datetime of the next valid date. Null if no date found.</returns>
    public DateTime? CalculateNextIntervalTime(DateTime dayToRun, DateTime minimumPossibleDate, bool allowSameDate)
    {
        //Do not bother calculating a next interval for a day that is < minimum date.
        if (dayToRun.Date < minimumPossibleDate.Date)
            return null;

        if (TimeType == FrequencyTimeType.OccursOnce)
        {
            //this occurs once per day at the given time... it is either ahead of the runDateTime time (valid)
            //or already passed, in which case we return null as there are no valid dates on this day.
            DateTime dateTimeToRun = dayToRun.Date.SetTime(OccursAt.Hour, OccursAt.Minute, OccursAt.Second, OccursAt.Millisecond);
            if (dateTimeToRun >= minimumPossibleDate)
            {
                return dateTimeToRun;
            }
            else
            {
                return null;
            }
        }
        else
        {
            //Calculate how long a single interval is.
            long intervalTickLength = 0;
            if (IntervalTimeType == FrequencyTimeUnit.Hours)
            {
                intervalTickLength = new TimeSpan(IntervalTime, 0, 0).Ticks;
            }
            else if (IntervalTimeType == FrequencyTimeUnit.Minutes)
            {
                intervalTickLength = new TimeSpan(0, IntervalTime, 0).Ticks;
            }
            else if (IntervalTimeType == FrequencyTimeUnit.Seconds)
            {
                intervalTickLength = new TimeSpan(0, 0, IntervalTime).Ticks;
            }

            //There are multiple possible repeats on a given day.
            //We need to calculate the next possible interval for the given day
            //after the minimumPossibleDate.

            //The minimum possible date/time to run is equal to the start time.
            DateTime dateTimeToRun = dayToRun.Date.SetTime(StartTime.Hour, StartTime.Minute, StartTime.Second, StartTime.Millisecond);
            DateTime minDate = dateTimeToRun > minimumPossibleDate ? dateTimeToRun : minimumPossibleDate;

            long startDateTick = dateTimeToRun.Ticks;
            long minimumDateTick = minDate.Ticks;
            long difference = minimumDateTick - startDateTick;

            long modDifference = difference % intervalTickLength;

            if (modDifference == 0)
                return minDate;
            else
            {
                DateTime retVal = minDate.AddTicks(-modDifference).AddTicks(intervalTickLength);
                if (retVal.Date > dayToRun.Date)
                {
                    //Overflowed to the next day... don't run.
                    return null;
                }

                return retVal;
            }
        }
    }

    /// <summary>
    /// Returns a string representation describing what time(s) this rule applies to.
    /// </summary>
    /// <returns></returns>
    public string GetSummary()
    {
        //Occurs every first Sunday of every 2 month(s) at 3:00:00 AM. Schedule will be used starting on 4/12/2018.
        if (TimeType == FrequencyTimeType.OccursOnce)
        {
            //at {time}
            return string.Format("at {0}", OccursAt.ToString("HH:mm:ss"));
        }
        else
        {
            //every {interval} {type} between {start} and {end}
            return string.Format("every {0} {1} between {2} and {3}", IntervalTime, IntervalTimeType.ToString().ToLower(), StartTime.ToString("HH:mm:ss"), EndTime.ToString("HH:mm:ss"));
        }
    }
}