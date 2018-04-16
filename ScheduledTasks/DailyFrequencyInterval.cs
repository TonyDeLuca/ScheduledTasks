using System;

public class DailyFrequencyInterval
{
    public enum DailyFrequencyIntervalType
    {
        OccursOnce = 0,
        OccursEvery
    }

    public enum DailyFrequencyIntervalTimeType
    {
        Hours = 0,
        Minutes,
        Seconds
    }

    public DailyFrequencyIntervalType IntervalType;

    //Used by occurs once
    public DateTime OccursAt;

    //Used by occurs every
    public int IntervalTime;

    public DailyFrequencyIntervalTimeType IntervalTimeType;
    public DateTime StartTime;
    public DateTime EndTime;

    public DailyFrequencyInterval(DateTime occursAt)
    {
        IntervalType = DailyFrequencyIntervalType.OccursOnce;
        OccursAt = occursAt;
    }

    public DailyFrequencyInterval(int intervalTime, DailyFrequencyIntervalTimeType intervalTimeType, DateTime startingTime, DateTime endingTime)
    {
        IntervalType = DailyFrequencyIntervalType.OccursEvery;
        IntervalTime = intervalTime;
        IntervalTimeType = intervalTimeType;
        StartTime = startingTime;
        EndTime = endingTime;
    }

    public string GetSummary()
    {
        //Occurs every first Sunday of every 2 month(s) at 3:00:00 AM. Schedule will be used starting on 4/12/2018.
        if (IntervalType == DailyFrequencyIntervalType.OccursOnce)
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

        if (IntervalType == DailyFrequencyIntervalType.OccursOnce)
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
            if (IntervalTimeType == DailyFrequencyIntervalTimeType.Hours)
            {
                intervalTickLength = new TimeSpan(IntervalTime, 0, 0).Ticks;
            }
            else if (IntervalTimeType == DailyFrequencyIntervalTimeType.Minutes)
            {
                intervalTickLength = new TimeSpan(0, IntervalTime, 0).Ticks;
            }
            else if (IntervalTimeType == DailyFrequencyIntervalTimeType.Seconds)
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