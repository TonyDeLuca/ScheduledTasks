using System;

/// <summary>
/// FrequencyTime describes when a job should run on a specific day.
/// </summary>
public class FrequencyTime
{
    public enum FrequencyTimeType
    {
        OccursOnce = 0,
        OccursEvery
    }

    public enum FrequencyTimeOption
    {
        Hours = 0,
        Minutes,
        Seconds
    }

    public FrequencyTimeType IntervalType;

    //Used by occurs once
    public DateTime OccursAt;

    //Used by occurs every
    public int IntervalTime;

    public FrequencyTimeOption IntervalTimeType;
    public DateTime StartTime;
    public DateTime EndTime;

    /// <summary>
    /// Creates a FrequencyTime that occurs at a specified time.
    /// </summary>
    /// <param name="occursAt">The time this frequency will run at.</param>
    public FrequencyTime(DateTime occursAt)
    {
        IntervalType = FrequencyTimeType.OccursOnce;
        OccursAt = occursAt;
    }

    /// <summary>
    /// Creates a FrequencyTime that occurs multiple times on the same day following a certain rule.
    /// </summary>
    /// <param name="intervalTime">How often this interval repeats.</param>
    /// <param name="intervalTimeType">The type of repeat, e.g. hours, minutes or seconds.</param>
    /// <param name="startingTime">The time this rule should run at.</param>
    /// <param name="endingTime">The time this rule will stop running at.</param>
    public FrequencyTime(int intervalTime, FrequencyTimeOption intervalTimeType, DateTime startingTime, DateTime endingTime)
    {
        IntervalType = FrequencyTimeType.OccursEvery;
        IntervalTime = intervalTime;
        IntervalTimeType = intervalTimeType;
        StartTime = startingTime;
        EndTime = endingTime;
    }

    /// <summary>
    /// Returns a string representation describing what times this rule applies to.
    /// </summary>
    /// <returns></returns>
    public string GetSummary()
    {
        //Occurs every first Sunday of every 2 month(s) at 3:00:00 AM. Schedule will be used starting on 4/12/2018.
        if (IntervalType == FrequencyTimeType.OccursOnce)
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

        if (IntervalType == FrequencyTimeType.OccursOnce)
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
            if (IntervalTimeType == FrequencyTimeOption.Hours)
            {
                intervalTickLength = new TimeSpan(IntervalTime, 0, 0).Ticks;
            }
            else if (IntervalTimeType == FrequencyTimeOption.Minutes)
            {
                intervalTickLength = new TimeSpan(0, IntervalTime, 0).Ticks;
            }
            else if (IntervalTimeType == FrequencyTimeOption.Seconds)
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
}