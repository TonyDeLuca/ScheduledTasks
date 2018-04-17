using System;

/// <summary>
/// A ScheduledTask describes a task that may be implemented and the schedule that it uses.
/// </summary>
public class ScheduledTask
{

    /// <summary>
    /// If this job is enabled or not, used for informational purposes to the implementing application.
    /// </summary>
    public bool Enabled;

    /// <summary>
    /// The TaskDescription is used associate a friendly description with this scheduled task, useed for informational purpoes to the implementing application.
    /// </summary>
    public string TaskDescription;

    private ScheduleDate mSchedule;

    private string mTaskName;

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
    /// Describes when this ScheduledTask occurs.
    /// </summary>
    public ScheduleDate Schedule
    {
        get
        {
            return mSchedule;
        }
        set
        {
            mSchedule = value ?? throw new ArgumentNullException("Schedule");
        }
    }

    /// <summary>
    /// The TaskName is used associate a friendly name with this scheduled task, useed for informational purpoes to the implementing application.
    /// </summary>
    public string TaskName
    {
        get
        {

            return mTaskName;
        }
        set
        {
            if (value == null)
            {
                throw new ArgumentNullException("TaskName");
            }

            if (value.Trim().Length == 0)
            {
                throw new ArgumentOutOfRangeException("TaskName");
            }

            mTaskName = value;
        }
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