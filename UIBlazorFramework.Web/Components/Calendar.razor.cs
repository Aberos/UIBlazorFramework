using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace UIBlazorFramework.Web.Components
{
    public partial class Calendar
    {
        private int _monthWeekCount;

        private DateTime _startDate;

        private Dictionary<int, DateTime[]> _monthDictionary;

        private GregorianCalendar _gc;

        protected override void OnInitialized()
        {
            _startDate = DateTime.Now;
            _gc = new GregorianCalendar();
            _monthDictionary = new Dictionary<int, DateTime[]>();
            LoadDictionaryMonth(_startDate);
            base.OnInitialized();
        }

        private void LoadDictionaryMonth(DateTime startDate)
        {
            int daysInMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
            for(var d = 1; d<= daysInMonth; d++)
            {
                var date = new DateTime(startDate.Year, startDate.Month, d);
                var weekNumber = GetWeekNumberOfMonth(date);
                AddMonthDictionary(weekNumber, date);
            }
        }

        private void AddMonthDictionary(int week, DateTime date)
        {
            if(_monthDictionary.TryGetValue(week, out var dates))
            {
                var indexDate = (int)date.DayOfWeek;
                dates[indexDate] = date;
            }
            else
            {
                DateTime[] arrayDates =  new DateTime[7];
                var indexDate = (int)date.DayOfWeek;
                arrayDates[indexDate] = date;
                _monthDictionary.Add(week, arrayDates);
            }
        }

        private int GetWeekNumberOfMonth(DateTime date)
        {
            DateTime first = new DateTime(date.Year, date.Month, 1);
            var result = GetWeekOfYear(date) - GetWeekOfYear(first) + 1;
            return result;
        }

        private int GetWeekOfYear(DateTime date)
        {
            return _gc.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }
    }
}
