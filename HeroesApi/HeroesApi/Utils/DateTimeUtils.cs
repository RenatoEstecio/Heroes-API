using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesApi.Utils
{
    public static class DateTimeUtils
    {

        public static string GetDateTimeNowToString()
        {
            string min = DateTime.Now.Minute.ToString();
            if (min.Length < 2)
                min = "0" + min;
            return ("Data de Processamento: " 
                + DateTime.Now.Date.ToShortDateString().Replace("/", "_")
                + " " + DateTime.Now.Hour + ":" 
                + min).Replace(":","_");
        }
    }
}
