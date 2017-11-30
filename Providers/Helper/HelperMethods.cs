using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providers.Helper
{
    public class HelperMethods
    {
        public static long ConversionInMinute(string WorkingHours)
        {

            string[] parts = WorkingHours.Split('.');
            int hours = 0;
            int mintues = 0;
            if (parts.Length > 1)
            {
                hours = Convert.ToInt32(parts[0]);
                mintues = Convert.ToInt32((parts[1]));
            }
            else
            {
                hours = Convert.ToInt32(parts[0]);
                mintues = 0;
            }

            int TotalMintues = hours * 60 + mintues;
            return TotalMintues;
        }
        public static decimal ConversionInHour(long durationMin)
        {
            long hours = (Convert.ToInt64(durationMin) - Convert.ToInt64(durationMin) % 60) / 60;
            decimal total = Convert.ToDecimal("" + hours + "." + Convert.ToDecimal(durationMin - hours * 60));
            return total;
        }
    }
}
