using Microsoft.EntityFrameworkCore;
using Notebook.Data.Models;
using Notebook.Data.Models.Calendar;

namespace Notebook.Data
{
    public static class ModelBuilderExtension
    {
        static ModelBuilderExtension()
        {
            var calendar = GetCalendar();

            Appointments = new HashSet<Appointment>();

            Notes = new HashSet<Note>();

            Procedures = new HashSet<Procedure>();

            Hours = GetHours(calendar);

            Days = GetDays(calendar);

            Months = GetMonths(calendar);

            Years = new HashSet<Year>
            {
                new Year()
                {
                    YearId = 1,
                    CalendarId = 1,
                    YearNum = DateTime.UtcNow.Year
                }
            };

            NotebookCalendars = new HashSet<NotebookCalendar>
            {
                 new NotebookCalendar
                 {
                     CalendarId = 1,
                 }
            };

            Procedures = new HashSet<Procedure>
            {
                new Procedure
                {
                    ProcedureId = 1,
                    ProcedureName = "Обикновен лак",
                    ProcedureTime = 1,
                },
                new Procedure
                {
                    ProcedureId = 2,
                    ProcedureName = "Гел лак",
                    ProcedureTime = 2,
                },
                new Procedure
                {
                    ProcedureId = 3,
                    ProcedureName = "Изграждане",
                    ProcedureTime = 2,
                },
                new Procedure
                {
                    ProcedureId = 4,
                    ProcedureName = "Поддръжка",
                    ProcedureTime = 2,
                },
            };



        }
        public static ICollection<Appointment> Appointments { get; }
        public static ICollection<Hour> Hours { get; }
        public static ICollection<Day> Days { get; }
        public static ICollection<Month> Months { get; }
        public static ICollection<Year> Years { get; }
        public static ICollection<Note> Notes { get; }
        public static ICollection<Procedure> Procedures { get; }
        public static ICollection<NotebookCalendar> NotebookCalendars { get; }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>().HasData(Appointments);
            modelBuilder.Entity<Hour>().HasData(Hours);
            modelBuilder.Entity<Day>().HasData(Days);
            modelBuilder.Entity<Month>().HasData(Months);
            modelBuilder.Entity<Year>().HasData(Years);
            modelBuilder.Entity<Note>().HasData(Notes);
            modelBuilder.Entity<Procedure>().HasData(Procedures);
            modelBuilder.Entity<NotebookCalendar>().HasData(NotebookCalendars);


        }

        static HashSet<Hour> GetHours(Dictionary<string, Dictionary<int, string>> calendar)
        {
            var id = 1;
            var hours = new HashSet<Hour>();
            var dayId = 1;
            foreach (var month in calendar)
            {
                foreach (var day in month.Value)
                {
                    for (int i = 8; i <= 16; i++)
                    {

                        hours.Add(new Hour
                        {
                            HourId = id++,
                            Clock = i,
                            DayId = dayId
                        });
                    }
                    dayId++;
                }
            }
            return hours;

        }
        static HashSet<Month> GetMonths(Dictionary<string, Dictionary<int, string>> calendar)
        {
            var months = new HashSet<Month>();
            var monthNum = 1;
            foreach (var month in calendar)
            {
                months.Add(new Month
                {
                    MonthNum = monthNum,
                    MonthId = monthNum,
                    MonthName = month.Key,
                    YearId = 1
                });
                monthNum++;
            }

            return months;
        }
        static HashSet<Day> GetDays(Dictionary<string, Dictionary<int, string>> calendar)
        {
            var days = new HashSet<Day>();
            var dayOfYear = DateTime.UtcNow.DayOfYear;

            var d = 1;
            var m = 1;
            bool workingDay = false;
            foreach (var month in calendar)
            {
                foreach (var day in month.Value)
                {
                    if (d >= dayOfYear)
                    {
                        if (day.Value == "Неделя")
                        {
                            workingDay = false;
                        }
                        else
                        {
                            workingDay = true;
                        }
                    }

                    days.Add(new Day
                    {
                        DayId = d,
                        Date = day.Key,
                        DayOfWeek = day.Value,
                        WorkingDay = workingDay,
                        MonthId = m
                    });
                    d++;
                }
                m++;
            }
            return days;
        }
        static Dictionary<string, Dictionary<int, string>> GetCalendar()
        {
            var calendar = new Dictionary<string, Dictionary<int, string>>();
            var days = new Dictionary<int, string>();

            List<string> monthsOfYear = ("Януари, Февруари, Март, Април, Май, Юни, Юли, Август, Септември, Октомври, Ноември, Декември")
                .Split(", ")
                .ToList();
            List<string> daysOfWeek = ("Понеделник, Вторник, Сряда, Четвъртък, Петък, Събота, Неделя")
                .Split(", ")
                .ToList();

            for (int m = 1; m <= 12; m++)
            {
                var today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
                string day = today.DayOfWeek.ToString();
                int dayOfWeek = SwitchDay(day);
                var daysInMonth = DateTime.DaysInMonth(DateTime.UtcNow.Year, m);

                for (int d = 1; d <= daysInMonth; d++)
                {
                    if (dayOfWeek == 8)
                    {
                        dayOfWeek = 1;
                    };

                    days.Add(d, daysOfWeek[dayOfWeek - 1]);
                    dayOfWeek++;
                }
                calendar.Add(monthsOfYear[m - 1], days);
                days = new Dictionary<int, string>();
            }

            return calendar;
        }

        static int SwitchDay(string day)
        {
            switch (day.ToLower())
            {
                case "monday":
                    return 1;

                case "tuesday":
                    return 2;

                case "wednesday":
                    return 3;

                case "thursday":
                    return 4;

                case "friday":
                    return 5;

                case "saturday":
                    return 6;

                case "sunday":
                    return 7;

                default:
                    return 0;
            }
        }
    }

}
