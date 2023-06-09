using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoonDate
{
    public class HijriMonth
    {
        private static List<String> monthNames = new List<String>() 
        {
            "Muharram", 
            "Safar",
            "Rabi al-Awwal",
            "Rabi al-Thani",
            "Jumada al-Awwal",
            "Jumada al-Thani",
            "Rajab",
            "Shaban",
            "Ramadan",
            "Shawwal",
            "Dhul Qadah",
            "Dhul Hijjah"
        };

        public static String GetNextMonth(String thisMonth)
        {
            String thisMonthL = thisMonth.ToLower();
            
            for (int i = 0; i < monthNames.Count; i++) 
            {
                String monthName = monthNames[i];
                if(monthName.ToLower().Equals(thisMonthL))
                {
                    if (i == (monthNames.Count - 1))
                        return monthNames[0];
                    else
                        return monthNames[i + 1];
                }

            }

            return "BADDATA";
        }
    }
}
