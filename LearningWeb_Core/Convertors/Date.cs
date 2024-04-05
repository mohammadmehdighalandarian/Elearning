using System.Globalization;

namespace LearningWeb_Core.Convertors
{
    public static class Date
    {
        public static string To_Shamsi(this DateTime date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            int year = persianCalendar.GetYear(date);
            int month = persianCalendar.GetMonth(date);
            int day = persianCalendar.GetDayOfMonth(date);

            return $"{year}/{month}/{day}";
        }
        
        
    }
}
