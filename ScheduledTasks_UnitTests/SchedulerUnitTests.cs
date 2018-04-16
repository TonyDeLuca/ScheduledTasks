using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace TaskSchedulerUnitTests
{
    public static class DateTimeExtensions
    {
        public static DateTime SetTime(this DateTime dt, int hours, int minutes, int seconds, int milliseconds)
        {
            DateTime retVal = dt.Date;
            retVal = retVal.AddHours(hours);
            retVal = retVal.AddMinutes(minutes);
            retVal = retVal.AddSeconds(seconds);
            retVal = retVal.AddMilliseconds(milliseconds);
            return retVal;
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }

    [TestClass]
    public class SchedulerUnitTests
    {
        #region DateTime_Methods

        [TestMethod]
        public void Tests_FindInstanceOfDay()
        {
            //This test case ensures that we are always grabbing the proper 1st day of the month
            //using our custom extension method

            //4/11/2018

            DateTime testMonth = new DateTime(2018, 4, 11);

            //There are 5 instances of Monday in this month.
            DateTime firstMonday = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.Monday, global::DateTimeExtensions.CalculatedDayType.First);
            DateTime expectedValue = new DateTime(2018, 4, 2);
            Assert.AreEqual(expectedValue, firstMonday);

            DateTime secondMonday = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.Monday, global::DateTimeExtensions.CalculatedDayType.Second);
            expectedValue = new DateTime(2018, 4, 9);
            Assert.AreEqual(expectedValue, secondMonday);

            DateTime thirdMonday = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.Monday, global::DateTimeExtensions.CalculatedDayType.Third);
            expectedValue = new DateTime(2018, 4, 16);
            Assert.AreEqual(expectedValue, thirdMonday);

            DateTime fourthMonday = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.Monday, global::DateTimeExtensions.CalculatedDayType.Fourth);
            expectedValue = new DateTime(2018, 4, 23);
            Assert.AreEqual(expectedValue, fourthMonday);

            DateTime lastMonday = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.Monday, global::DateTimeExtensions.CalculatedDayType.Last);
            expectedValue = new DateTime(2018, 4, 30);
            Assert.AreEqual(expectedValue, lastMonday);

            //There are 4 instance of Tuesday in this month.
            DateTime firstTuesday = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.Tuesday, global::DateTimeExtensions.CalculatedDayType.First);
            expectedValue = new DateTime(2018, 4, 3);
            Assert.AreEqual(expectedValue, firstTuesday);

            DateTime secondTuesday = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.Tuesday, global::DateTimeExtensions.CalculatedDayType.Second);
            expectedValue = new DateTime(2018, 4, 10);
            Assert.AreEqual(expectedValue, secondTuesday);

            DateTime thirdTuesday = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.Tuesday, global::DateTimeExtensions.CalculatedDayType.Third);
            expectedValue = new DateTime(2018, 4, 17);
            Assert.AreEqual(expectedValue, thirdTuesday);

            DateTime fourthTuesday = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.Tuesday, global::DateTimeExtensions.CalculatedDayType.Fourth);
            expectedValue = new DateTime(2018, 4, 24);
            Assert.AreEqual(expectedValue, fourthTuesday);

            DateTime lastTuesday = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.Tuesday, global::DateTimeExtensions.CalculatedDayType.Last);
            expectedValue = new DateTime(2018, 4, 24);
            Assert.AreEqual(expectedValue, lastTuesday);

            //Find instance of "Day"
            DateTime firstDay = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.Day, global::DateTimeExtensions.CalculatedDayType.First);
            expectedValue = new DateTime(2018, 4, 1);
            Assert.AreEqual(expectedValue, firstDay);

            DateTime secondDay = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.Day, global::DateTimeExtensions.CalculatedDayType.Second);
            expectedValue = new DateTime(2018, 4, 2);
            Assert.AreEqual(expectedValue, secondDay);

            DateTime thirdDay = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.Day, global::DateTimeExtensions.CalculatedDayType.Third);
            expectedValue = new DateTime(2018, 4, 3);
            Assert.AreEqual(expectedValue, thirdDay);

            DateTime fourthDay = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.Day, global::DateTimeExtensions.CalculatedDayType.Fourth);
            expectedValue = new DateTime(2018, 4, 4);
            Assert.AreEqual(expectedValue, fourthDay);

            DateTime lastDay = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.Day, global::DateTimeExtensions.CalculatedDayType.Last);
            expectedValue = new DateTime(2018, 4, 30);
            Assert.AreEqual(expectedValue, lastDay);

            //Find instance of "Weekday"
            DateTime firstWeekday = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.Weekday, global::DateTimeExtensions.CalculatedDayType.First);
            expectedValue = new DateTime(2018, 4, 2);
            Assert.AreEqual(expectedValue, firstWeekday);

            DateTime secondWeekday = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.Weekday, global::DateTimeExtensions.CalculatedDayType.Second);
            expectedValue = new DateTime(2018, 4, 3);
            Assert.AreEqual(expectedValue, secondWeekday);

            DateTime thirdWeekday = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.Weekday, global::DateTimeExtensions.CalculatedDayType.Third);
            expectedValue = new DateTime(2018, 4, 4);
            Assert.AreEqual(expectedValue, thirdWeekday);

            DateTime fourthWeekday = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.Weekday, global::DateTimeExtensions.CalculatedDayType.Fourth);
            expectedValue = new DateTime(2018, 4, 5);
            Assert.AreEqual(expectedValue, fourthWeekday);

            DateTime lastWeekday = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.Weekday, global::DateTimeExtensions.CalculatedDayType.Last);
            expectedValue = new DateTime(2018, 4, 30);
            Assert.AreEqual(expectedValue, lastWeekday);

            //Find instance of "Weekend Day"
            DateTime firstWeekendDay = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.WeekendDay, global::DateTimeExtensions.CalculatedDayType.First);
            expectedValue = new DateTime(2018, 4, 1);
            Assert.AreEqual(expectedValue, firstWeekendDay);

            DateTime secondWeekendDay = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.WeekendDay, global::DateTimeExtensions.CalculatedDayType.Second);
            expectedValue = new DateTime(2018, 4, 7);
            Assert.AreEqual(expectedValue, secondWeekendDay);

            DateTime thirdWeekendDay = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.WeekendDay, global::DateTimeExtensions.CalculatedDayType.Third);
            expectedValue = new DateTime(2018, 4, 8);
            Assert.AreEqual(expectedValue, thirdWeekendDay);

            DateTime fourthWeekendDay = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.WeekendDay, global::DateTimeExtensions.CalculatedDayType.Fourth);
            expectedValue = new DateTime(2018, 4, 14);
            Assert.AreEqual(expectedValue, fourthWeekendDay);

            DateTime lastWeekendDay = testMonth.FindInstanceOfDay(global::DateTimeExtensions.CalculcatedDayOption.WeekendDay, global::DateTimeExtensions.CalculatedDayType.Last);
            expectedValue = new DateTime(2018, 4, 29);
            Assert.AreEqual(expectedValue, lastWeekendDay);
        }

        [TestMethod]
        public void Tests_StartOfMonth()
        {
            //This test case ensures that we are always grabbing the proper 1st day of the month
            //using our custom extension method

            //Start of the current month
            DateTime test1 = DateTime.Now.StartOfMonth();
            DateTime expected1 = new DateTime(test1.Year, test1.Month, 1);
            Assert.AreEqual(expected1, test1);

            //Start of March 11 2018
            DateTime test2 = new DateTime(2018, 3, 11).StartOfMonth();
            DateTime expected2 = new DateTime(2018, 3, 1);
            Assert.AreEqual(expected2, test2);

            //Start of Oct 24 2024
            DateTime test3 = new DateTime(2024, 10, 24).StartOfMonth();
            DateTime expected3 = new DateTime(2024, 10, 1);
            Assert.AreEqual(expected3, test3);
        }

        [TestMethod]
        public void Tests_StartOfWeek()
        {
            //This test case ensures that we are always grabbing the proper day of the week (Monday)
            //for all given dates/times

            DateTime expectedDate = new DateTime(2018, 4, 9); //Monday April 9 2018
            DateTime monday = new DateTime(2018, 4, 9).StartOfWeek(DayOfWeek.Monday);
            DateTime tuesday = new DateTime(2018, 4, 10).StartOfWeek(DayOfWeek.Monday);
            DateTime wednesday = new DateTime(2018, 4, 11).StartOfWeek(DayOfWeek.Monday);
            DateTime thursday = new DateTime(2018, 4, 12).StartOfWeek(DayOfWeek.Monday);
            DateTime friday = new DateTime(2018, 4, 13).StartOfWeek(DayOfWeek.Monday);
            DateTime saturday = new DateTime(2018, 4, 14).StartOfWeek(DayOfWeek.Monday);
            DateTime sunday = new DateTime(2018, 4, 15).StartOfWeek(DayOfWeek.Monday);
            Assert.AreEqual(monday, expectedDate);
            Assert.AreEqual(tuesday, expectedDate);
            Assert.AreEqual(wednesday, expectedDate);
            Assert.AreEqual(thursday, expectedDate);
            Assert.AreEqual(friday, expectedDate);
            Assert.AreEqual(saturday, expectedDate);
            Assert.AreEqual(sunday, expectedDate);
            Assert.AreEqual(monday.DayOfWeek, DayOfWeek.Monday);

            DateTime dayBeforeMonday = new DateTime(2018, 4, 8).StartOfWeek(DayOfWeek.Monday);
            Assert.AreNotEqual(dayBeforeMonday, expectedDate);
        }

        #endregion DateTime_Methods

        #region Daily_OccursOnce

        [TestMethod]
        public void Tests_DailyOccurOnce_8_YearLong()
        {
            //Case: LastRunDate starts at the startTime and increments to the last run time.
            //Runs every day at 9:00 PM
            //Repeat interval 1 days
            //Expected to run at the top of the hour after the last run date.

            //System time increments in 58 min, 34 sec, 311ms increments...
            //This can be lowered for additional testing, however may take significant time.

            DateTime lastRunDate = new DateTime(2010, 12, 31, 23, 0, 0);

            DateTime occursAt = DateTime.MinValue.SetTime(21, 0, 0, 0); ;
            DateTime systemTimeStart = new DateTime(2010, 12, 31).SetTime(23, 30, 42, 123);
            DateTime systemTimeEnd = new DateTime(2012, 1, 1).SetTime(23, 59, 59, 999);
            TimeSpan systemTimeIncrement = new TimeSpan(0, 0, 58, 31, 311);

            int interval = 1;

            DailyFrequency df1 = new DailyFrequency(interval);
            FrequencyTime dfi1 = new FrequencyTime(occursAt);
            ScheduleDate scheduledDate1 = new ScheduleDate(df1, dfi1);

            SystemTime.SetDateTime(systemTimeStart);
            while (SystemTime.Now() <= systemTimeEnd)
            {
                DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);

                DateTime expectedDate = SystemTime.Now().SetTime(21, 0, 0, 0);

                if (SystemTime.Now() >= SystemTime.Now().SetTime(21, 0, 0, 0))
                {
                    //Set the last run date to be the last execution time.
                    lastRunDate = expectedDate;

                    //It should now occur on the next day.
                    expectedDate = expectedDate.AddDays(1);
                }

                Assert.AreEqual(expectedDate, taskDate1);

                SystemTime.SetDateTime(SystemTime.Now().Add(systemTimeIncrement));
            }
            SystemTime.ResetDateTime();
        }

        [TestMethod]
        public void Tests_DailyOccursOnce_1()
        {
            //Case 1: LastRunDate 10 days in the past
            //Runs daily at 12:00 AM
            //Repeat interval 1 day
            //Expected to run tomorrow at 12:00 AM.
            DateTime lastRunDate = DateTime.Now.AddDays(-10);

            DateTime occursAt = DateTime.MinValue;
            occursAt = occursAt.SetTime(0, 0, 0, 0);

            DailyFrequency df1 = new DailyFrequency(1);
            FrequencyTime dfi1 = new FrequencyTime(occursAt);
            ScheduleDate scheduledDate1 = new ScheduleDate(df1, dfi1);

            DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);

            Assert.AreEqual(DateTime.Now.Date.AddDays(1), taskDate1);
        }

        [TestMethod]
        public void Tests_DailyOccursOnce_2()
        {
            //Case 2: LastRunDate 10 days in the past
            //Runs daily at 11:59:59 PM
            //Repeat interval 1 day
            //Expected to run tonight at 11:59:59 PM.
            DateTime lastRunDate = DateTime.Now.AddDays(-10);

            DateTime occursAt = DateTime.MinValue;
            occursAt = occursAt.SetTime(23, 59, 59, 999);

            DailyFrequency df1 = new DailyFrequency(1);
            FrequencyTime dfi1 = new FrequencyTime(occursAt);
            ScheduleDate scheduledDate1 = new ScheduleDate(df1, dfi1);

            DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);

            DateTime expectedDate = DateTime.Now.Date;
            expectedDate = expectedDate.SetTime(23, 59, 59, 999);
            Assert.AreEqual(expectedDate, taskDate1);
        }

        [TestMethod]
        public void Tests_DailyOccursOnce_3()
        {
            //Case 3: LastRunDate is right now
            //Runs daily at 12:00:00 AM
            //Repeat interval 1 day
            //Expected Result: Runs tomorrow at 12:00:00 AM
            DateTime lastRunDate = DateTime.Now;

            DateTime occursAt = DateTime.MinValue;
            occursAt = occursAt.SetTime(0, 0, 0, 0);

            DailyFrequency df1 = new DailyFrequency(1);
            FrequencyTime dfi1 = new FrequencyTime(occursAt);
            ScheduleDate scheduledDate1 = new ScheduleDate(df1, dfi1);

            DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);

            DateTime expectedDate = DateTime.Now.Date.AddDays(1);
            expectedDate = expectedDate.SetTime(0, 0, 0, 0);
            Assert.AreEqual(expectedDate, taskDate1);
        }

        [TestMethod]
        public void Tests_DailyOccursOnce_4()
        {
            //Case 4: LastRunDate is right now
            //Runs daily at 11:59:59 PM
            //Repeat interval 1 day
            //Expected Result: Runs tonight at 11:59:59 PM
            DateTime lastRunDate = DateTime.Now;

            DateTime occursAt = DateTime.MinValue;
            occursAt = occursAt.SetTime(23, 59, 59, 999);

            DailyFrequency df1 = new DailyFrequency(1);
            FrequencyTime dfi1 = new FrequencyTime(occursAt);
            ScheduleDate scheduledDate1 = new ScheduleDate(df1, dfi1);

            DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);

            DateTime expectedDate = DateTime.Now.Date;
            expectedDate = expectedDate.SetTime(23, 59, 59, 999);
            Assert.AreEqual(expectedDate, taskDate1);
        }

        [TestMethod]
        public void Tests_DailyOccursOnce_5()
        {
            //Case 4: LastRunDate is at 2:00 PM tomorrow
            //Runs daily at 12:00:00 AM
            //Repeat interval 1 day
            //Expected Result: Runs in 2 days 12:00:00 AM
            DateTime lastRunDate = DateTime.Now.Date.AddDays(1).SetTime(14, 0, 0, 0);

            DateTime occursAt = DateTime.MinValue;
            occursAt = occursAt.SetTime(0, 0, 0, 0);

            DailyFrequency df1 = new DailyFrequency(1);
            FrequencyTime dfi1 = new FrequencyTime(occursAt);
            ScheduleDate scheduledDate1 = new ScheduleDate(df1, dfi1);

            DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);
            DateTime expectedDate = DateTime.Now.Date.AddDays(2).SetTime(0, 0, 0, 0);
            Assert.AreEqual(taskDate1, expectedDate);
        }

        [TestMethod]
        public void Tests_DailyOccursOnce_6()
        {
            //Case 4: LastRunDate is at 2:00 PM tomorrow
            //Runs daily at 11:59:59 AM
            //Repeat interval 1 day
            //Expected Result: Runs tomorrow at 11:59:59 PM.
            DateTime lastRunDate = DateTime.Now.Date.AddDays(1).SetTime(14, 0, 0, 0);

            DateTime occursAt = DateTime.MinValue;
            occursAt = occursAt.SetTime(23, 59, 59, 999);

            DailyFrequency df1 = new DailyFrequency(1);
            FrequencyTime dfi1 = new FrequencyTime(occursAt);
            ScheduleDate scheduledDate1 = new ScheduleDate(df1, dfi1);

            DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);
            DateTime expectedDate = DateTime.Now.Date.AddDays(1).SetTime(23, 59, 59, 999);
            Assert.AreEqual(taskDate1, expectedDate);
        }

        [TestMethod]
        public void Tests_DailyOccursOnce_7_TickTest()
        {
            //The system time is set to 4:59:59:999 on 3/11/1987
            //This job was last run on 3/10/1987 at 5:00:00.000 AM.
            //This job is scheduled daily at 5:00:00.000 AM.
            //It repeats once per day.

            //Tick Test: Start before the scheduled time
            //Increment through it by 1 millisecond
            //Ensure every possible tick returns the expected value.

            DateTime lastRunDate = new DateTime(1987, 3, 10, 5, 0, 0);
            DateTime occursAt = DateTime.MinValue.SetTime(5, 0, 0, 0); ;
            DateTime systemTimeStart = new DateTime(1987, 3, 11).SetTime(4, 59, 59, 999);

            DateTime expectedChangeTime = new DateTime(1987, 3, 11).SetTime(5, 0, 0, 0);

            DateTime systemTimeEnd = new DateTime(1987, 3, 11).SetTime(5, 0, 0, 1);

            DailyFrequency df1 = new DailyFrequency(1);
            FrequencyTime dfi1 = new FrequencyTime(occursAt);
            ScheduleDate scheduledDate1 = new ScheduleDate(df1, dfi1);

            SystemTime.SetDateTime(systemTimeStart);
            while (SystemTime.Now() <= systemTimeEnd)
            {
                DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);

                DateTime expectedDate = expectedChangeTime;
                if (SystemTime.Now() >= expectedChangeTime)
                {
                    expectedDate = new DateTime(1987, 3, 12).SetTime(5, 0, 0, 0);
                }

                Assert.AreEqual(taskDate1, expectedDate);

                SystemTime.SetDateTime(SystemTime.Now().AddTicks(1));
            }
            SystemTime.ResetDateTime();
        }

        [TestMethod]
        public void Tests_DailyOccursOnce_8()
        {
            //Case 8: LastRunDate 30 years in the past
            //Runs daily at 12:00 AM
            //Repeat interval 1 day
            //Expected to run tomorrow at 12:00 AM.
            DateTime lastRunDate = DateTime.Now.AddYears(-10);

            DateTime occursAt = DateTime.MinValue;
            occursAt = occursAt.SetTime(0, 0, 0, 0);

            DailyFrequency df1 = new DailyFrequency(1);
            FrequencyTime dfi1 = new FrequencyTime(occursAt);
            ScheduleDate scheduledDate1 = new ScheduleDate(df1, dfi1);

            DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);

            Assert.AreEqual(DateTime.Now.Date.AddDays(1), taskDate1);
        }

        #endregion Daily_OccursOnce

        #region Daily_OccursEvery

        [TestMethod]
        public void Tests_DailyOccurEvery_1()
        {
            //Case 1: LastRunDate 10 days in the past
            //Runs every 1 hour starting at 12:00:00 AM and ending at 11:59:59 PM.
            //Repeat interval 1 day
            //Expected to run at the top of the next hour.
            DateTime lastRunDate = DateTime.Now.AddDays(-10);
            DateTime startTime = DateTime.MinValue.SetTime(0, 0, 0, 0);
            DateTime endTime = DateTime.MinValue.SetTime(23, 59, 59, 999);
            int repeatInterval = 1;

            int everyTime = 1;
            FrequencyTime.FrequencyTimeOption everyType = FrequencyTime.FrequencyTimeOption.Hours;

            DateTime expectedValue = DateTime.Now.Date.AddHours(DateTime.Now.Hour + 1);

            DailyFrequency df1 = new DailyFrequency(repeatInterval);
            FrequencyTime dfi1 = new FrequencyTime(everyTime, everyType, startTime, endTime);
            ScheduleDate scheduledDate1 = new ScheduleDate(df1, dfi1);
            DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);
            Assert.AreEqual(taskDate1, expectedValue);
        }

        [TestMethod]
        public void Tests_DailyOccurEvery_2()
        {
            //Case 2: LastRunDate 10 days in the past
            //Runs every 1 minute starting at 12:00:00 AM and ending at 11:59:59 PM.
            //Repeat interval 1 day
            //Expected to run at the top of the next minute.
            DateTime lastRunDate = DateTime.Now.AddDays(-10);
            DateTime startTime = DateTime.MinValue.SetTime(0, 0, 0, 0);
            DateTime endTime = DateTime.MinValue.SetTime(23, 59, 59, 999);
            int repeatInterval = 1;

            int everyTime = 1;
            FrequencyTime.FrequencyTimeOption everyType = FrequencyTime.FrequencyTimeOption.Minutes;

            DateTime expectedValue = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute + 1);

            DailyFrequency df1 = new DailyFrequency(repeatInterval);
            FrequencyTime dfi1 = new FrequencyTime(everyTime, everyType, startTime, endTime);
            ScheduleDate scheduledDate1 = new ScheduleDate(df1, dfi1);
            DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);
            Assert.AreEqual(taskDate1, expectedValue);
        }

        [TestMethod]
        public void Tests_DailyOccurEvery_3()
        {
            //Case 2: LastRunDate 10 days in the past
            //Runs every 1 second starting at 12:00:00 AM and ending at 11:59:59 PM.
            //Repeat interval 1 day
            //Expected to run at the top of the next second.
            DateTime lastRunDate = DateTime.Now.AddDays(-10);
            DateTime startTime = DateTime.MinValue.SetTime(0, 0, 0, 0);
            DateTime endTime = DateTime.MinValue.SetTime(23, 59, 59, 999);
            int repeatInterval = 1;

            int everyTime = 1;
            FrequencyTime.FrequencyTimeOption everyType = FrequencyTime.FrequencyTimeOption.Seconds;

            DailyFrequency df1 = new DailyFrequency(repeatInterval);
            FrequencyTime dfi1 = new FrequencyTime(everyTime, everyType, startTime, endTime);
            ScheduleDate scheduledDate1 = new ScheduleDate(df1, dfi1);
            DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);
            DateTime expectedValue = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second + 1);

            Assert.AreEqual(taskDate1, expectedValue);
        }

        [TestMethod]
        public void Tests_DailyOccurEvery_4()
        {
            //Case 3: LastRunDate 12:00:00 AM this morning
            //Runs every 1 hour starting at 12:00:00 AM and ending at 11:59:59 PM.
            //Repeat interval 1 day
            //Expected to run at the top of the next hour.
            DateTime lastRunDate = DateTime.Now.Date;
            DateTime startTime = DateTime.MinValue.SetTime(0, 0, 0, 0);
            DateTime endTime = DateTime.MinValue.SetTime(23, 59, 59, 999);
            int repeatInterval = 1;

            int everyTime = 1;
            FrequencyTime.FrequencyTimeOption everyType = FrequencyTime.FrequencyTimeOption.Hours;

            DailyFrequency df1 = new DailyFrequency(repeatInterval);
            FrequencyTime dfi1 = new FrequencyTime(everyTime, everyType, startTime, endTime);
            ScheduleDate scheduledDate1 = new ScheduleDate(df1, dfi1);
            DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);
            DateTime expectedValue = DateTime.Now.Date.AddHours(DateTime.Now.Hour + 1);

            Assert.AreEqual(taskDate1, expectedValue);
        }

        [TestMethod]
        public void Tests_DailyOccurEvery_5()
        {
            //Case 3: LastRunDate is at the top of the next hour
            //Runs every 1 hour starting at 12:00:00 AM and ending at 11:59:59 PM.
            //Repeat interval 1 day
            //Expected to run at the top of the hour after the last run date.
            DateTime lastRunDate = DateTime.Now.Date.AddHours(DateTime.Now.Hour + 1);
            DateTime startTime = DateTime.MinValue.SetTime(0, 0, 0, 0);
            DateTime endTime = DateTime.MinValue.SetTime(23, 59, 59, 999);
            int repeatInterval = 1;

            int everyTime = 1;
            FrequencyTime.FrequencyTimeOption everyType = FrequencyTime.FrequencyTimeOption.Hours;

            DateTime expectedValue = DateTime.Now.Date.AddHours(DateTime.Now.Hour + 2);

            DailyFrequency df1 = new DailyFrequency(repeatInterval);
            FrequencyTime dfi1 = new FrequencyTime(everyTime, everyType, startTime, endTime);
            ScheduleDate scheduledDate1 = new ScheduleDate(df1, dfi1);
            DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);

            Assert.AreEqual(expectedValue, taskDate1);
        }

        [TestMethod]
        public void Tests_DailyOccurEvery_6_TickTest()
        {
            //Case: LastRunDate starts at the startTime and increments to the last run time.
            //This test case increments through every possible 'Tick' of system time between a previous
            //run and when it should change, ensuring it doesn't change to the next possible date too early.
            //Runs every 1 hour starting at 12:00:00 AM and ending at 11:59:59 PM.
            //Repeat interval 1 days
            //Expected to run at the top of the hour after the last run date.

            int repeatInterval = 1;
            int everyTime = 1;
            DateTime startTime = DateTime.MinValue.SetTime(0, 0, 0, 0);
            DateTime endTime = DateTime.MinValue.SetTime(23, 59, 59, 999);

            DateTime startDateTime = new DateTime(2040, 1, 1).SetTime(0, 59, 59, 999);
            DateTime endDateTime = new DateTime(2040, 1, 1).SetTime(1, 0, 0, 1);
            DateTime lastRunDate = new DateTime(2040, 1, 1).SetTime(0, 0, 0, 0);

            FrequencyTime.FrequencyTimeOption everyType = FrequencyTime.FrequencyTimeOption.Hours;
            DailyFrequency df1 = new DailyFrequency(repeatInterval);
            FrequencyTime dfi1 = new FrequencyTime(everyTime, everyType, startTime, endTime);
            ScheduleDate scheduledDate1 = new ScheduleDate(df1, dfi1);

            SystemTime.SetDateTime(startDateTime);

            DateTime lastTaskValue = DateTime.MinValue;

            int loopCount = 0;
            while (SystemTime.Now() <= endDateTime)
            {
                loopCount++;

                DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);
                DateTime expectedValue = SystemTime.Now().SetTime(SystemTime.Now().Hour, 0, 0, 0).AddHours(1);

                if (taskDate1 != lastTaskValue)
                {
                    Debug.WriteLine("Changed Date at Loop {0} from {1} to {2}", loopCount, lastTaskValue, taskDate1);
                    lastTaskValue = taskDate1;
                }

                //Verify
                Assert.AreEqual(expectedValue, taskDate1);

                //Move to the next tick
                SystemTime.SetDateTime(SystemTime.Now().AddTicks(1));
            }
            SystemTime.ResetDateTime();
        }

        [TestMethod]
        public void Tests_DailyOccurEvery_6_TickTest2()
        {
            //Case: LastRunDate starts at the startTime and increments to the last run time.
            //This test case increments through every possible 'Tick' of system time between a previous
            //run and when it should change, ensuring it doesn't change to the next possible date too early.
            //Runs every 1 hour starting at 12:00:00 AM and ending at 11:59:59 PM.
            //Repeat interval 1 days
            //Expected to run at the top of the hour after the last run date.

            //NOTE: In this case the last run date is updated after
            //Time >= TaskDate
            int repeatInterval = 1;
            int everyTime = 1;
            DateTime startTime = DateTime.MinValue.SetTime(0, 0, 0, 0);
            DateTime endTime = DateTime.MinValue.SetTime(23, 59, 59, 999);

            DateTime startDateTime = new DateTime(2040, 1, 1).SetTime(0, 59, 59, 999);
            DateTime endDateTime = new DateTime(2040, 1, 1).SetTime(1, 0, 0, 1);
            DateTime lastRunDate = new DateTime(2040, 1, 1).SetTime(0, 0, 0, 0);

            FrequencyTime.FrequencyTimeOption everyType = FrequencyTime.FrequencyTimeOption.Hours;
            DailyFrequency df1 = new DailyFrequency(repeatInterval);
            FrequencyTime dfi1 = new FrequencyTime(everyTime, everyType, startTime, endTime);
            ScheduleDate scheduledDate1 = new ScheduleDate(df1, dfi1);

            SystemTime.SetDateTime(startDateTime);

            DateTime lastTaskValue = DateTime.MinValue;

            int loopCount = 0;
            while (SystemTime.Now() <= endDateTime)
            {
                loopCount++;

                DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);
                DateTime expectedValue = SystemTime.Now().SetTime(SystemTime.Now().Hour, 0, 0, 0).AddHours(1);

                if (taskDate1 != lastTaskValue)
                {
                    Debug.WriteLine("Changed Date at Loop {0} from {1} to {2}", loopCount, lastTaskValue, taskDate1);
                    lastTaskValue = taskDate1;
                }

                if (SystemTime.Now() >= taskDate1)
                {
                    lastRunDate = taskDate1;
                }

                //Verify
                Assert.AreEqual(expectedValue, taskDate1);

                //Move to the next tick
                SystemTime.SetDateTime(SystemTime.Now().AddTicks(1));
            }
            SystemTime.ResetDateTime();
        }

        [TestMethod]
        public void Tests_DailyOccurEvery_6_YearLong()
        {
            //Case: LastRunDate starts at the startTime and increments to the last run time.
            //Runs every 1 hour starting at 12:00:00 AM and ending at 11:59:59 PM.
            //Repeat interval 1 days
            //Expected to run at the top of the hour after the last run date.

            int repeatInterval = 1;
            int everyTime = 1;
            DateTime startTime = DateTime.MinValue.SetTime(0, 0, 0, 0);
            DateTime endTime = DateTime.MinValue.SetTime(23, 59, 59, 999);
            DateTime startDateTime = new DateTime(2039, 12, 31, 0, 0, 0);
            DateTime endDateTime = new DateTime(2041, 1, 1, 23, 59, 59);
            DateTime lastRunDate = startDateTime;

            FrequencyTime.FrequencyTimeOption everyType = FrequencyTime.FrequencyTimeOption.Hours;
            DailyFrequency df1 = new DailyFrequency(repeatInterval);
            FrequencyTime dfi1 = new FrequencyTime(everyTime, everyType, startTime, endTime);
            ScheduleDate scheduledDate1 = new ScheduleDate(df1, dfi1);

            SystemTime.SetDateTime(startDateTime);
            while (lastRunDate <= endDateTime)
            {
                DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);
                DateTime expectedValue = SystemTime.Now().SetTime(SystemTime.Now().Hour, 0, 0, 0).AddHours(1);
                Assert.AreEqual(expectedValue, taskDate1);

                SystemTime.SetDateTime(SystemTime.Now().AddMinutes(1));
                lastRunDate = SystemTime.Now();
            }
            SystemTime.ResetDateTime();
        }

        #endregion Daily_OccursEvery

        #region Weekly_OccursOnce

        [TestMethod]
        public void Tests_WeeklyOccursOnce_1()
        {
            //Case 1: LastRunDate 10 days in the past
            //Runs every Sunday at 1:00 AM
            //Repeat interval 1 week
            //Expected to run on Sunday at 11:59:59 PM.
            DateTime lastRunDate = DateTime.Now.AddDays(-10);
            DateTime occursAt = DateTime.MinValue.SetTime(23, 59, 59, 999);

            int repeatInterval = 1;

            WeeklyFrequency wf1 = new WeeklyFrequency(repeatInterval);
            wf1.Sunday = true;

            DateTime expectedValue = DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(6).SetTime(23, 59, 59, 999);

            FrequencyTime dfi1 = new FrequencyTime(occursAt);
            ScheduleDate scheduledDate1 = new ScheduleDate(wf1, dfi1);
            DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);
            Assert.AreEqual(expectedValue, taskDate1);
        }

        [TestMethod]
        public void Tests_WeeklyOccursOnce_2()
        {
            //Case 2: LastRunDate is next monday at 1:00 AM
            //Runs every Monday,Wednesday,Friday at 1:00 AM
            //Repeat interval 1 week
            //Expected to run on Wednesday at 1:00:00 AM.
            DateTime lastRunDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(7).SetTime(1, 0, 0, 0);
            DateTime occursAt = DateTime.MinValue.SetTime(1, 0, 0, 0);

            int repeatInterval = 1;

            WeeklyFrequency wf1 = new WeeklyFrequency(repeatInterval);
            wf1.Monday = true;
            wf1.Wednesday = true;
            wf1.Friday = true;
            DateTime expectedValue = DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(9).SetTime(1, 0, 0, 0);

            FrequencyTime dfi1 = new FrequencyTime(occursAt);
            ScheduleDate scheduledDate1 = new ScheduleDate(wf1, dfi1);
            DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);
            Assert.AreEqual(expectedValue, taskDate1);
        }

        [TestMethod]
        public void Tests_WeeklyOccursOnce_3_TickTest()
        {
            //Case 3: LastRunDate is next monday at 1:00 AM
            //Runs every Monday at 12:00 AM
            //Repeat interval 1 week
            //Expected to run on Monday at 12:00:00 AM the next week.

            
            DateTime systemTimeStart = new DateTime(2018, 5, 20).SetTime(23, 59, 59, 999);
            DateTime lastRunDate = systemTimeStart;
            DateTime systemTimeEnd = new DateTime(2018, 5, 21).SetTime(0, 0, 0, 1);

            DateTime occursAt = DateTime.MinValue.SetTime(0, 0, 0, 0);
            int repeatInterval = 1;

            WeeklyFrequency wf1 = new WeeklyFrequency(repeatInterval);
            wf1.Monday = true;


            FrequencyTime dfi1 = new FrequencyTime(occursAt);
            ScheduleDate scheduledDate1 = new ScheduleDate(wf1, dfi1);
            

            SystemTime.SetDateTime(systemTimeStart);


            while (SystemTime.Now() <= systemTimeEnd)
            {

                DateTime currentTime = SystemTime.Now();
                DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);

                DateTime expectedValue = currentTime.StartOfWeek(DayOfWeek.Monday).AddDays(7).SetTime(0, 0, 0, 0);
                Assert.AreEqual(expectedValue, taskDate1);
                SystemTime.SetDateTime(currentTime.AddTicks(1));
            }

            SystemTime.ResetDateTime();
        }

        #endregion Weekly_OccursOnce

        #region Weekly_OccursEvery

        [TestMethod]
        public void Tests_WeeklyOccursEvery_1()
        {
            //Case 1: LastRunDate is 3/11/2018
            //Runs every 1 hour on Monday starting at 12:00:00 AM and ending at 11:59:59 PM.
            //Repeat interval 1 week
            //Expected to run on the next monday at 12:00:00 AM.

            DateTime lastRunDate = new DateTime(2018, 3, 11);
            DateTime systemTime = new DateTime(2018, 3, 22).SetTime(23, 59, 59, 999); //Sunday

            SystemTime.SetDateTime(systemTime);

            DateTime startTime = DateTime.MinValue.SetTime(0, 0, 0, 0);
            DateTime endTime = DateTime.MinValue.SetTime(23, 59, 59, 999);
            int repeatInterval = 1;

            int everyTime = 1;
            FrequencyTime.FrequencyTimeOption everyType = FrequencyTime.FrequencyTimeOption.Hours;

            WeeklyFrequency wf1 = new WeeklyFrequency(repeatInterval);
            wf1.Monday = true;

            DateTime expectedValue = systemTime.StartOfWeek(DayOfWeek.Monday).AddDays(7);

            FrequencyTime dfi1 = new FrequencyTime(everyTime, everyType, startTime, endTime);
            ScheduleDate scheduledDate1 = new ScheduleDate(wf1, dfi1);
            DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);
            Assert.AreEqual(expectedValue, taskDate1);

            SystemTime.ResetDateTime();
        }

        [TestMethod]
        public void Tests_WeeklyOccursEvery_2()
        {
            //Case 2: LastRunDate is 3/11/2018
            //Runs every 1 hour on Tuesday,Thursday starting at 3:00:00 AM and ending at 11:59:59 PM.
            //Repeat interval 1 week
            //Expected to run on the tuesday at 3:00:00 AM.

            DateTime lastRunDate = new DateTime(2018, 3, 11);
            DateTime systemTime = new DateTime(2018, 3, 22).SetTime(23, 59, 59, 999); //Sunday

            SystemTime.SetDateTime(systemTime);

            DateTime startTime = DateTime.MinValue.SetTime(3, 0, 0, 0);
            DateTime endTime = DateTime.MinValue.SetTime(23, 59, 59, 999);
            int repeatInterval = 1;

            int everyTime = 1;
            FrequencyTime.FrequencyTimeOption everyType = FrequencyTime.FrequencyTimeOption.Hours;

            WeeklyFrequency wf1 = new WeeklyFrequency(repeatInterval);
            wf1.Tuesday = true;
            wf1.Thursday = true;

            DateTime expectedValue = systemTime.StartOfWeek(DayOfWeek.Monday).AddDays(8).SetTime(3,0,0,0);

            FrequencyTime dfi1 = new FrequencyTime(everyTime, everyType, startTime, endTime);
            ScheduleDate scheduledDate1 = new ScheduleDate(wf1, dfi1);
            DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);
            Assert.AreEqual(expectedValue, taskDate1);

            SystemTime.ResetDateTime();
        }

        [TestMethod]
        public void Tests_WeeklyOccursEvery_3()
        {
            //Case 1: LastRunDate is next Monday at 23:00:00 PM
            //Runs every 1 hour on Monday & Tuesday starting at 3:00:00 AM and ending at 11:59:59 PM.
            //Repeat interval 1 week
            //Expected to run on the next Tuesday at 3:00:00 AM.
            DateTime lastRunDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(7).SetTime(23, 0, 0, 0);
            DateTime startTime = DateTime.MinValue.SetTime(3, 0, 0, 0);
            DateTime endTime = DateTime.MinValue.SetTime(23, 59, 59, 999);
            int repeatInterval = 1;

            int everyTime = 1;
            FrequencyTime.FrequencyTimeOption everyType = FrequencyTime.FrequencyTimeOption.Hours;

            WeeklyFrequency wf1 = new WeeklyFrequency(repeatInterval);
            wf1.Monday = true;
            wf1.Tuesday = true;

            DateTime expectedValue = DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(8).SetTime(3, 0, 0, 0);

            FrequencyTime dfi1 = new FrequencyTime(everyTime, everyType, startTime, endTime);
            ScheduleDate scheduledDate1 = new ScheduleDate(wf1, dfi1);
            DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);
            Assert.AreEqual(taskDate1, expectedValue);
        }

        [TestMethod]
        public void Tests_WeeklyOccursEvery_4()
        {
            //Case 4: LastRunDate is next Monday at 23:00:00 PM
            //Runs every 1 hour on Monday & Wednesday starting at 3:00:00 AM and ending at 11:59:59 PM.
            //Repeat interval 1 week
            //Expected to run on the next Wednesday at 3:00:00 AM.
            DateTime lastRunDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(7).SetTime(23, 0, 0, 0);
            DateTime startTime = DateTime.MinValue.SetTime(3, 0, 0, 0);
            DateTime endTime = DateTime.MinValue.SetTime(23, 59, 59, 999);
            int repeatInterval = 1;

            int everyTime = 1;
            FrequencyTime.FrequencyTimeOption everyType = FrequencyTime.FrequencyTimeOption.Hours;

            WeeklyFrequency wf1 = new WeeklyFrequency(repeatInterval);
            wf1.Monday = true;
            wf1.Wednesday = true;

            DateTime expectedValue = DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(9).SetTime(3, 0, 0, 0);

            FrequencyTime dfi1 = new FrequencyTime(everyTime, everyType, startTime, endTime);
            ScheduleDate scheduledDate1 = new ScheduleDate(wf1, dfi1);
            DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);
            Assert.AreEqual(taskDate1, expectedValue);
        }

        #endregion Weekly_OccursEvery

        #region DayOfTheMonth

        [TestMethod]
        public void Tests_MonthlyDayOf_OccursEvery_1()
        {
            //Case: Last Run Date is Now
            //Runs on Day 1 of the month
            //Starts at 12:00 AM
            //Ends at 23:59:59.999 PM.
            //Every 1 month
            //Expected: Runs at 12:00 AM next month.

            DateTime lastRunDate = SystemTime.Now();
            DateTime startTime = DateTime.MinValue.SetTime(0, 0, 0, 0);
            DateTime endTime = DateTime.MinValue.SetTime(23, 59, 59, 999);
            DateTime expectedValue = DateTime.Now.StartOfMonth().AddMonths(1);

            int repeatInterval = 1;
            int dayNumber = 1;
            int everyTime = 1;
            FrequencyTime.FrequencyTimeOption everyType = FrequencyTime.FrequencyTimeOption.Hours;

            MonthlyFrequency date = new MonthlyFrequency(repeatInterval, dayNumber);
            FrequencyTime time = new FrequencyTime(everyTime, everyType, startTime, endTime);

            ScheduleDate scheduledDate = new ScheduleDate(date, time);

            DateTime taskDate = scheduledDate.GetScheduledDate(lastRunDate);
            Assert.AreEqual(expectedValue, taskDate);
        }

        [TestMethod]
        public void Tests_MonthlyDayOf_OccursEvery_2()
        {
            //Case: Last Run Date 4/1/2018 at 12:00 AM.
            //System Date is 4/1/2018 at 12:00 AM.
            //Runs on Day 1 of the month
            //Starts at 12:00 AM
            //Ends at 23:59:59.999 PM.
            //Every 1 month
            //Expected: Runs at 1:00 AM on 4/1.

            DateTime systemTime = new DateTime(2018, 4, 1).SetTime(0, 0, 0, 0);
            SystemTime.SetDateTime(systemTime);
            DateTime lastRunDate = new DateTime(2018, 4, 1).SetTime(0, 0, 0, 0);

            DateTime startTime = DateTime.MinValue.SetTime(0, 0, 0, 0);
            DateTime endTime = DateTime.MinValue.SetTime(23, 59, 59, 999);
            DateTime expectedValue = new DateTime(2018, 4, 1).SetTime(1, 0, 0, 0);

            int repeatInterval = 1;
            int dayNumber = 1;
            int everyTime = 1;
            FrequencyTime.FrequencyTimeOption everyType = FrequencyTime.FrequencyTimeOption.Hours;

            MonthlyFrequency date = new MonthlyFrequency(repeatInterval, dayNumber);
            FrequencyTime time = new FrequencyTime(everyTime, everyType, startTime, endTime);
            ScheduleDate scheduledDate = new ScheduleDate(date, time);
            DateTime taskDate = scheduledDate.GetScheduledDate(lastRunDate);
            Assert.AreEqual(expectedValue, taskDate);
            SystemTime.ResetDateTime();
        }

        [TestMethod]
        public void Tests_MonthlyDayOf_OccursEvery_3_TickTest()
        {
            //Case: Last Run Date 12/31/2018 at 11:00:00 PM.
            //System Date is 12/31/2018 at 11:59:59.999 PM.
            //Runs on Day 1 of the month
            //Starts at 12:00:00.000
            //Ends at 23:59:59.999.
            //Every 1 month
            //Expected: Task runs at 12:00 AM on 1/1 when run on 12/31, then 1:00 AM on 1/1 when run on 1/1.

            DateTime systemTimeStart = new DateTime(2018, 12, 31).SetTime(23, 59, 59, 999);
            DateTime systemTimeEnd = new DateTime(2019, 1, 1).SetTime(0, 0, 0, 1);
            DateTime lastRunDate = new DateTime(2018, 12, 31).SetTime(23, 0, 0, 0);

            DateTime startTime = DateTime.MinValue.SetTime(0, 0, 0, 0);
            DateTime endTime = DateTime.MinValue.SetTime(23, 59, 59, 999);

            int repeatInterval = 1;
            int dayNumber = 1;
            int everyTime = 1;
            FrequencyTime.FrequencyTimeOption everyType = FrequencyTime.FrequencyTimeOption.Hours;

            MonthlyFrequency date = new MonthlyFrequency(repeatInterval, dayNumber);
            FrequencyTime time = new FrequencyTime(everyTime, everyType, startTime, endTime);
            ScheduleDate scheduledDate = new ScheduleDate(date, time);

            SystemTime.SetDateTime(systemTimeStart);
            int tickCount = 0;
            while (SystemTime.Now() <= systemTimeEnd)
            {
                tickCount++;
                DateTime taskDate = scheduledDate.GetScheduledDate(lastRunDate);
                DateTime expectedValue = SystemTime.Now().Year == 2018 ? new DateTime(2019, 1, 1).SetTime(0, 0, 0, 0) : new DateTime(2019, 1, 1).SetTime(1, 0, 0, 0);
                Assert.AreEqual(expectedValue, taskDate);
                SystemTime.SetDateTime(SystemTime.Now().AddTicks(1));
            }
            SystemTime.ResetDateTime();
        }

        [TestMethod]
        public void Tests_MonthlyDayOf_OccursOnce_1()
        {
            //Case 1: LastRunDate is today
            //Runs on the 1st day of the month at 12:00 AM
            //Repeat interval 1 month
            //Expected to run the first day of next month
            DateTime lastRunDate = DateTime.Now;
            DateTime occursAt = DateTime.MinValue.SetTime(0, 0, 0, 0);
            int repeatInterval = 1;
            int dayOfMonth = 1;

            MonthlyFrequency mf1 = new MonthlyFrequency(repeatInterval, dayOfMonth);
            DateTime expectedValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1);
            FrequencyTime dfi1 = new FrequencyTime(occursAt);
            ScheduleDate scheduledDate1 = new ScheduleDate(mf1, dfi1);
            DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);
            Assert.AreEqual(expectedValue, taskDate1);
        }

        [TestMethod]
        public void Tests_MonthlyDayOf_OccursOnce_2()
        {
            //Case 2: LastRunDate is today
            //Runs on the 1st day of the month at 12:00 AM
            //Repeat interval 2 month
            //Expected to run the first day of 2 months from now
            DateTime lastRunDate = DateTime.Now;
            DateTime occursAt = DateTime.MinValue.SetTime(0, 0, 0, 0);
            int repeatInterval = 2;
            int dayOfMonth = 1;

            MonthlyFrequency mf1 = new MonthlyFrequency(repeatInterval, dayOfMonth);
            DateTime expectedValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dayOfMonth).AddMonths(2);
            FrequencyTime dfi1 = new FrequencyTime(occursAt);
            ScheduleDate scheduledDate1 = new ScheduleDate(mf1, dfi1);
            DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);
            Assert.AreEqual(expectedValue, taskDate1);
        }

        [TestMethod]
        public void Tests_MonthlyDayOf_OccursOnce_3()
        {
            //Case 3: LastRunDate is today
            //Runs on the 5th day of the month at 8:00 PM
            //Repeat interval 2 month
            //Expected to run the 5th day of 2 months from now
            DateTime lastRunDate = DateTime.Now;
            DateTime occursAt = DateTime.MinValue.SetTime(20, 0, 0, 0);
            int repeatInterval = 2;
            int dayOfMonth = 5;

            MonthlyFrequency mf1 = new MonthlyFrequency(repeatInterval, dayOfMonth);
            DateTime expectedValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dayOfMonth).AddMonths(2).SetTime(20, 0, 0, 0);
            FrequencyTime dfi1 = new FrequencyTime(occursAt);
            ScheduleDate scheduledDate1 = new ScheduleDate(mf1, dfi1);
            DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);
            Assert.AreEqual(expectedValue, taskDate1);
        }

        [TestMethod]
        public void Tests_MonthlyDayOf_OccursOnce_4()
        {
            //Case 3: LastRunDate is today
            //Runs on the last day of this month at 11:59:59 PM
            //Repeat interval 1 month
            //Expected: last day of month at 11:59:59 PM

            DateTime lastRunDate = DateTime.Now;
            DateTime occursAt = DateTime.MinValue.SetTime(23, 59, 59, 0);

            int repeatInterval = 1;
            int dayOfMonth = DateTime.DaysInMonth(lastRunDate.Year, lastRunDate.Month);

            MonthlyFrequency mf1 = new MonthlyFrequency(repeatInterval, dayOfMonth);
            DateTime expectedValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dayOfMonth).SetTime(23, 59, 59, 0);
            FrequencyTime dfi1 = new FrequencyTime(occursAt);
            ScheduleDate scheduledDate1 = new ScheduleDate(mf1, dfi1);
            DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);
            Assert.AreEqual(expectedValue, taskDate1);
        }

        [TestMethod]
        public void Tests_MonthlyDayOf_OccursOnce_5()
        {
            //Case: The first sunday of every 2 month. Last run day was January 1.
            //Runs on the last day of this month at 11:59:59 PM
            //Repeat interval 1 month
            //Expected: last day of month at 11:59:59 PM

            DateTime lastRunDate = DateTime.Now;
            DateTime occursAt = DateTime.MinValue.SetTime(23, 59, 59, 0);

            int repeatInterval = 1;
            int dayOfMonth = DateTime.DaysInMonth(lastRunDate.Year, lastRunDate.Month);

            MonthlyFrequency mf1 = new MonthlyFrequency(repeatInterval, dayOfMonth);
            DateTime expectedValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dayOfMonth).SetTime(23, 59, 59, 0);
            FrequencyTime dfi1 = new FrequencyTime(occursAt);
            ScheduleDate scheduledDate1 = new ScheduleDate(mf1, dfi1);
            DateTime taskDate1 = scheduledDate1.GetScheduledDate(lastRunDate);
            Assert.AreEqual(expectedValue, taskDate1);
        }
        #endregion DayOfTheMonth

        #region GetNextDay_Tests

        [TestMethod]
        public void Tests_GetNextDay_Daily_1()
        {
            //Day 4/14/2018
            //Interval: 1
            //Expected: 4/15/2018.
            DateTime startDay = new DateTime(2018, 4, 14);
            DailyFrequency dfTest = new DailyFrequency(1);
            DateTime testDay = dfTest.GetNextDay(startDay);
            DateTime expectedValue = new DateTime(2018, 4, 15);
            Assert.AreEqual(expectedValue, testDay);
        }

        [TestMethod]
        public void Tests_GetNextDay_Daily_2()
        {
            //Day 4/14/2018
            //Interval: 2
            //Expected: 4/16/2018.
            DateTime startDay = new DateTime(2018, 4, 14);
            int interval = 2;
            DailyFrequency dfTest = new DailyFrequency(interval);
            DateTime testDay = dfTest.GetNextDay(startDay);
            DateTime expectedValue = startDay.AddDays(interval);
            Assert.AreEqual(expectedValue, testDay);
        }

        [TestMethod]
        public void Tests_GetNextDay_Daily_3()
        {
            //Day: Now
            //Interval: 3
            //Expected: Today + 3 days
            DateTime startDay = DateTime.Now;
            int interval = 2;
            DailyFrequency dfTest = new DailyFrequency(interval);
            DateTime testDay = dfTest.GetNextDay(startDay);
            DateTime expectedValue = startDay.AddDays(interval).Date;
            Assert.AreEqual(expectedValue, testDay);
        }

        [TestMethod]
        public void Tests_GetNextDay_Daily_4()
        {
            //Start Day is 4/1/2018
            //Interval = 2 days.
            //Valid days are 4/3,5,7,9,11,13....
            //Minimum date: 4/10.
            //Expected return: 4/11.
            int interval = 2;

            DateTime startDay = new DateTime(2018, 4, 1);
            DateTime minDay = new DateTime(2018, 4, 10);
            DateTime expectDay = new DateTime(2018, 4, 11);

            DailyFrequency dfTest = new DailyFrequency(interval);
            DateTime nextDay = dfTest.GetNextDay(startDay, minDay);

            Assert.AreEqual(expectDay, nextDay);
        }

        [TestMethod]
        public void Tests_GetNextDay_Monthly_1()
        {
            //Day: March 20 2018
            //Interval: 1 Month
            //Day of the Month: 31
            //Expected: March 31 2018

            DateTime startDay = new DateTime(2018, 3, 20);
            int interval = 1;
            int dayOfTheMonth = 31;

            MonthlyFrequency mfTest = new MonthlyFrequency(interval, dayOfTheMonth);
            DateTime testDay = mfTest.GetNextDay(startDay);

            DateTime expectedValue = new DateTime(2018, 3, 31);
            Assert.AreEqual(expectedValue, testDay);
        }

        [TestMethod]
        public void Tests_GetNextDay_Monthly_2()
        {
            //Day: April 10 2018
            //Interval: 1 Month
            //Day of the Month: 31
            //Expected: May 31 2018 (no day 31 in April)

            DateTime startDay = new DateTime(2018, 4, 10);
            int interval = 1;
            int dayOfTheMonth = 31;

            MonthlyFrequency mfTest = new MonthlyFrequency(interval, dayOfTheMonth);
            DateTime testDay = mfTest.GetNextDay(startDay);

            DateTime expectedValue = new DateTime(2018, 5, 31);
            Assert.AreEqual(expectedValue, testDay);
        }

        [TestMethod]
        public void Tests_GetNextDay_Monthly_3()
        {
            //Day: April 10 2018
            //Interval: 1 Month
            //Day of the Month: 1
            //Expected: May 1 2018

            DateTime startDay = new DateTime(2018, 4, 10);
            int interval = 1;
            int dayOfTheMonth = 1;

            MonthlyFrequency mfTest = new MonthlyFrequency(interval, dayOfTheMonth);
            DateTime testDay = mfTest.GetNextDay(startDay);

            DateTime expectedValue = new DateTime(2018, 5, 1);
            Assert.AreEqual(expectedValue, testDay);
        }

        [TestMethod]
        public void Tests_GetNextDay_Monthly_4()
        {
            //Day: April 10 2018
            //Interval: 2 Month
            //Day of the Month: 1
            //Expected: June 1 2018

            DateTime startDay = new DateTime(2018, 4, 10);
            int interval = 2;
            int dayOfTheMonth = 1;

            MonthlyFrequency mfTest = new MonthlyFrequency(interval, dayOfTheMonth);
            DateTime testDay = mfTest.GetNextDay(startDay);

            DateTime expectedValue = new DateTime(2018, 6, 1);
            Assert.AreEqual(expectedValue, testDay);
        }

        [TestMethod]
        public void Tests_GetNextDay_Monthly_5()
        {
            //Day: January 31 2018
            //Interval: 1 Month
            //Day of the Month: 28
            //Expected: February 28 2018

            DateTime startDay = new DateTime(2018, 1, 31);
            int interval = 1;
            int dayOfTheMonth = 28;

            MonthlyFrequency mfTest = new MonthlyFrequency(interval, dayOfTheMonth);
            DateTime testDay = mfTest.GetNextDay(startDay);

            DateTime expectedValue = new DateTime(2018, 2, 28);
            Assert.AreEqual(expectedValue, testDay);
        }

        [TestMethod]
        public void Tests_GetNextDay_Weekly_1()
        {
            //Day: Now
            //Interval: 1 week
            //Days Enabled: None
            //Expected: Next Monday

            DateTime startDay = DateTime.Now;
            int interval = 1;
            WeeklyFrequency wfTest = new WeeklyFrequency(interval);
            DateTime testDay = wfTest.GetNextDay(startDay);
            DateTime expectedValue = startDay.StartOfWeek(DayOfWeek.Monday).AddDays(7).Date;
            Assert.AreEqual(expectedValue, testDay);
        }

        [TestMethod]
        public void Tests_GetNextDay_Weekly_2()
        {
            //Day: Sunday March 11 2018
            //Interval: 2 week
            //Days Enabled: None
            //Expected: Monday March 19 2018

            DateTime startDay = new DateTime(2018, 3, 11);
            int interval = 2;
            WeeklyFrequency wfTest = new WeeklyFrequency(interval);
            DateTime testDay = wfTest.GetNextDay(startDay);
            DateTime expectedValue = new DateTime(2018, 3, 19);
            Assert.AreEqual(expectedValue, testDay);
        }

        [TestMethod]
        public void Tests_GetNextDay_Weekly_3()
        {
            //Day: Sunday March 11 2018
            //Interval: 2 week
            //Days Enabled: Tuesday, Thursday
            //Expected: Tuesday March 20 2018

            DateTime startDay = new DateTime(2018, 3, 11);
            int interval = 2;
            WeeklyFrequency wfTest = new WeeklyFrequency(interval);
            wfTest.Tuesday = true;
            wfTest.Thursday = true;
            DateTime testDay = wfTest.GetNextDay(startDay);
            DateTime expectedValue = new DateTime(2018, 3, 20);
            Assert.AreEqual(expectedValue, testDay);
        }

        [TestMethod]
        public void Tests_GetNextDay_Weekly_4()
        {
            //Day: Tuesday March 20 2018
            //Interval: 2 week
            //Days Enabled: Tuesday, Thursday
            //Expected: Thursday March 22 2018

            DateTime startDay = new DateTime(2018, 3, 20);
            int interval = 2;
            WeeklyFrequency wfTest = new WeeklyFrequency(interval);
            wfTest.Tuesday = true;
            wfTest.Thursday = true;
            DateTime testDay = wfTest.GetNextDay(startDay);
            DateTime expectedValue = new DateTime(2018, 3, 22);
            Assert.AreEqual(expectedValue, testDay);
        }

        [TestMethod]
        public void Tests_GetNextDay_Weekly_5()
        {
            //Day: March 1 2018  (Thursday)
            //Interval: 3 Weeks
            //Days Enabled: Tuesday, Thursday
            //Min Date: April 1 2018
            //Expected: April 10 2018

            int interval = 3;
            DateTime startDay = new DateTime(2018, 3, 1);
            DateTime minDate = new DateTime(2018, 4, 1);
            DateTime expectedDate = new DateTime(2018, 4, 10);

            WeeklyFrequency wfTest = new WeeklyFrequency(interval);
            wfTest.Tuesday = true;
            wfTest.Thursday = true;

            DateTime testDay = wfTest.GetNextDay(startDay, minDate);

            Assert.AreEqual(expectedDate, testDay);
        }

        [TestMethod]
        public void Tests_GetNextDay_Weekly_6()
        {
            //Day: March 1 2018  (Thursday)
            //Interval: 3 Weeks
            //Days Enabled: (None) ... first available day is returned in the next valid week.
            //Min Date: April 1 2018
            //Expected: April 9 2018

            int interval = 3;
            DateTime startDay = new DateTime(2018, 3, 1);
            DateTime minDate = new DateTime(2018, 4, 1);
            DateTime expectedDate = new DateTime(2018, 4, 9);

            WeeklyFrequency wfTest = new WeeklyFrequency(interval);

            DateTime testDay = wfTest.GetNextDay(startDay, minDate);

            Assert.AreEqual(expectedDate, testDay);
        }

        [TestMethod]
        public void Tests_GetNextDay_Weekly_7()
        {
            //Day: March 1 2018  (Thursday)
            //Interval: 4 Weeks
            //Days Enabled: (None) ... first available day is returned in the next valid week.
            //Min Date: April 1 2018
            //Expected: April 1 2018 (The last day of the 4th week)

            int interval = 4;
            DateTime startDay = new DateTime(2018, 3, 1);
            DateTime minDate = new DateTime(2018, 4, 1);
            DateTime expectedDate = new DateTime(2018, 4, 1);

            WeeklyFrequency wfTest = new WeeklyFrequency(interval);

            DateTime testDay = wfTest.GetNextDay(startDay, minDate);

            Assert.AreEqual(expectedDate, testDay);
        }

        #endregion GetNextDay_Tests
    }
}