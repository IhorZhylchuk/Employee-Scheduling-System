using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureIdentity.Models
{
    public class GetDays
    {
        public int GetDay(IQueryable<int> day)
        {
            foreach (var i in day)   
                return i;
            return 0; 
            
        }
        public int GetDatesDTime(DateTime dateTime_1, DateTime dateTime_2)
        {
            return (int)(dateTime_1 - dateTime_2).TotalDays;
        }
       
        public int WeekDays(DateTime date_start, DateTime date_end)
        {
            var dates = new List<DateTime>();
            for (var day = date_start; day <= date_end; day = day.AddDays(1)){ 
                if (day.DayOfWeek != DayOfWeek.Sunday && day.DayOfWeek != DayOfWeek.Saturday)
                {
                    dates.Add(day);
                }
            }
            return dates.Count();
        }
    }
}
