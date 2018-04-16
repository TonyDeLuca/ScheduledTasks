using System;

/// <summary>
/// A ScheduleDate is assigned to a ScheduledTask. It describes what days this rule repeats on and the time those rules run at.
/// </summary>
public class ScheduleDate
{
    private Frequency ScheduledFrequency;
    private FrequencyTime ScheduledFrequencyInterval;

    /// <summary>
    /// Creates a ScheduleDate with the given frequency and interval.
    /// </summary>
    /// <param name="frequency"></param>
    /// <param name="frequencyInterval"></param>
    public ScheduleDate(Frequency frequency, FrequencyTime frequencyInterval)
    {
        ScheduledFrequency = frequency ?? throw new ArgumentNullException("frequency");
        ScheduledFrequencyInterval = frequencyInterval ?? throw new ArgumentNullException("frequencyInterval");
    }

    /// <summary>
    /// Returns the next time this schedule should run given the last run date.
    /// </summary>
    /// <returns></returns>
    public DateTime GetScheduledDate(DateTime lastRunDate)
    {
        lastRunDate = lastRunDate.StartOfMillisecond();
        DateTime currentDate = SystemTime.Now();
        if (ScheduledFrequency is DailyFrequency)
        {
            DailyFrequency frequency = (DailyFrequency)ScheduledFrequency;

            #region DailyFrequencyCalculation

            //The minimum date is a minimum of either todays date or the next run date
            //whichever is greater.
            DateTime nextRunDate = lastRunDate;
            DateTime todaysDate = SystemTime.Now();
            DateTime minDate = nextRunDate > todaysDate ? nextRunDate : todaysDate;
            minDate = minDate.StartOfMillisecond().AddSeconds(1);

            if (nextRunDate.Date < minDate.Date)
                nextRunDate = frequency.GetNextDay(nextRunDate, minDate);

            bool setDateTime = false;
            while (setDateTime == false)
            {
                DateTime? nextValidTime = ScheduledFrequencyInterval.CalculateNextIntervalTime(nextRunDate, minDate, true);
                if (nextValidTime != null)
                {
                    nextRunDate = nextValidTime.Value;
                    setDateTime = true;
                }

                if (setDateTime == false)
                    nextRunDate = frequency.GetNextDay(nextRunDate, minDate);
            }

            return nextRunDate;

            #endregion DailyFrequencyCalculation
        }
        else if (ScheduledFrequency is WeeklyFrequency)
        {
            WeeklyFrequency frequency = (WeeklyFrequency)ScheduledFrequency;

            #region WeeklyFrequencyCalculation

            //Increment last run date until date >= today's date.
            DateTime nextRunDate = lastRunDate;
            DateTime lastRunWeek = lastRunDate.StartOfWeek(DayOfWeek.Monday);
            DateTime thisWeek = currentDate.StartOfWeek(DayOfWeek.Monday);
            DateTime minDate = nextRunDate > currentDate ? nextRunDate : currentDate;
            minDate = minDate.StartOfMillisecond().AddSeconds(1);
            DateTime currentValidDay = frequency.GetNextDay(minDate.AddDays(-1));

            minDate = currentValidDay > minDate ? currentValidDay : minDate;

            DateTime minWeek = minDate.StartOfWeek(DayOfWeek.Monday);

            //Ignore any weeks less than the week we want to check
            if (nextRunDate.Date.StartOfWeek(DayOfWeek.Monday) < minWeek.Date)
                nextRunDate = frequency.GetNextDay(nextRunDate, minDate);

            bool setDateTime = false;
            while (setDateTime == false)
            {
                DateTime? nextValidTime = ScheduledFrequencyInterval.CalculateNextIntervalTime(nextRunDate, minDate, true);
                if (nextValidTime != null)
                {
                    nextRunDate = nextValidTime.Value;
                    setDateTime = true;
                }

                if (setDateTime == false)
                    nextRunDate = frequency.GetNextDay(nextRunDate, minDate);
            }

            return nextRunDate;

            #endregion WeeklyFrequencyCalculation
        }
        else if (ScheduledFrequency is MonthlyFrequency)
        {
            MonthlyFrequency frequency = (MonthlyFrequency)ScheduledFrequency;

            #region MonthlyFrequencyCalculation

            //Increment last run date until date >= today's date.
            DateTime nextRunDate = lastRunDate;
            DateTime lastRunMonth = lastRunDate.StartOfMonth();
            DateTime thisMonth = SystemTime.Now().StartOfMonth();
            DateTime minDate = lastRunDate > currentDate ? lastRunDate : currentDate;
            DateTime currentValidDay = frequency.GetNextDay(minDate.AddDays(-1));
            minDate = minDate.StartOfMillisecond().AddSeconds(1);
            minDate = currentValidDay > minDate ? currentValidDay : minDate;
            DateTime minMonth = minDate.StartOfMonth();

            //Ignore any months less than the month we want to check
            if (nextRunDate.Date.StartOfMonth() < minMonth)
                nextRunDate = frequency.GetNextDay(nextRunDate, minDate);

            bool setDateTime = false;
            while (setDateTime == false)
            {
                DateTime? nextValidTime = ScheduledFrequencyInterval.CalculateNextIntervalTime(nextRunDate, minDate, true);
                if (nextValidTime != null)
                {
                    nextRunDate = nextValidTime.Value;
                    setDateTime = true;
                }

                if (setDateTime == false)
                    nextRunDate = frequency.GetNextDay(nextRunDate, minDate);
            }

            return nextRunDate;

            #endregion MonthlyFrequencyCalculation
        }

        return new DateTime();
    }

    /// <summary>
    /// Returns a summary describing the schedule of this scheduled task.
    /// </summary>
    /// <returns></returns>
    public string GetScheduleSummary()
    {
        return ScheduledFrequency.GetSummary() + " " + ScheduledFrequencyInterval.GetSummary() + ".";
    }
}