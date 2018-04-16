using System;

/// <summary>
/// A ScheduledTask describes a task that may be implemented and the schedule that it uses.
/// </summary>
public class ScheduledTask
{
    public bool Enabled;
    public ScheduleDate Schedule;
    public string TaskDescription;
    public string TaskName;

    /// <summary>
    /// Creates a ScheduledTask.
    /// </summary>
    /// <param name="taskName">The name of the task.</param>
    /// <param name="taskDescription">A description for this task.</param>
    /// <param name="enabled">The status of whether or not the task is enabled.</param>
    /// <param name="frequency">How often (on what days) this task should be run.</param>
    /// <param name="dailyFrequencyInterval">How often (at what times) this task should be run.</param>
    public ScheduledTask(string taskName, string taskDescription, bool enabled, Frequency frequency, FrequencyTime dailyFrequencyInterval)
    {
        TaskName = taskName;
        TaskDescription = taskDescription;
        Enabled = enabled;

        Schedule = new ScheduleDate(frequency, dailyFrequencyInterval);
    }

    /// <summary>
    /// Returns the next date and time this scheduled task should be run at.
    /// </summary>
    /// <returns></returns>
    public DateTime GetNextRunDate(DateTime lastRunDate)
    {
        return Schedule.GetScheduledDate(lastRunDate);
    }
}