# ScheduledTasks
C# class library for determining the next time a scheduled task should be run based on defined rules. Based on a last run date the next valid execution date of a scheduled task will be calculated following rules such as "Occurs every 2 day(s). Runs every 1 hour starting at 3:00 AM and ending at 5:00 AM."

Basic Usage:

Create a new ScheduledTask class, providing the following parameters:
Task Name, Description (optional), Enabled, Frequency and FrequencyTime

---

There are 3 provided "Frequency" classes:
-DailyFrequency
Repeats every X number of days.

-WeeklyFrequency
Repeats every X number of weeks on specified days. Create this class with the interval then set the boolean flags for any days you wish to enable. Note that Monday is considered the first day of the week for calculations.

-MonthlyFrequency
Repeats every X number of months on a specific day of the month or on a calculated day of the month. There are two constructors.

The first accepting the number of months between executions and the second being the day number. This will cause execution on a given day number, ex: Day 21 will execute on the 21st of that month. Note that if a month does not contain the specified number of days the next date given will be the next date falling on the interval and containing that number of days.

The second accepting the number of months between executions, the instance of a day to find (First, Second, Third, Fourth, Last) and the type of day (Monday, Tuesday, ..., Sunday, "Day," "Weekend Day," "Weekday.").

--- 

The FrequencyTime class describes the times of day to execute the task on.

The first constructor accepts a DataTime describing the single time the task should run at. The date part of this parameter is ignored.

The second constructor accepts a time between executions, a start time and an end time. The date part of the DataTime parameters is ignored.

