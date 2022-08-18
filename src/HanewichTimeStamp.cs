using System;
using System.Collections.Generic;

namespace TimHanewich.Toolkit
{
    //FORMAT: YYYYMMDDHHMMSSmilliseconds
    public class HanewichTimeStamp
    {
        public int? Year {get; set;}
        public int? Month {get; set;}
        public int? Day {get; set;}
        public int? Hour {get; set;}
        public int? Minute {get; set;}
        public int? Second {get; set;}
        public int? Millisecond {get; set;}

        public HanewichTimeStamp()
        {

        }

        public HanewichTimeStamp(DateTime timestamp)
        {
            Year = timestamp.Year;
            Month = timestamp.Month;
            Day = timestamp.Day;
            Hour = timestamp.Hour;
            Minute = timestamp.Minute;
            Second = timestamp.Second;
            Millisecond = timestamp.Millisecond;
        }

        public static HanewichTimeStamp Parse(string ts)
        {

            HanewichTimeStamp hts = new HanewichTimeStamp();

            //year
            try
            {
                hts.Year = Convert.ToInt32(ts.Substring(0, 4));
            }
            catch
            {
                hts.Year = null;
            }

            //month
            try
            {
                hts.Month = Convert.ToInt32(ts.Substring(4, 2));
            }
            catch
            {
                hts.Month = null;
            }

            //day
            try
            {
                hts.Day = Convert.ToInt32(ts.Substring(6, 2));
            }
            catch
            {
                hts.Day = null;
            }

            //hour
            try
            {
                hts.Hour = Convert.ToInt32(ts.Substring(8, 2));
            }
            catch
            {
                hts.Hour = null;
            }


            //minute
            try
            {
                hts.Minute = Convert.ToInt32(ts.Substring(10, 2));
            }
            catch
            {
                hts.Minute = null;
            }

            //second
            try
            {
                hts.Second = Convert.ToInt32(ts.Substring(12, 2));
            }
            catch
            {
                hts.Second = null;
            }

            //millisecond
            try
            {
                hts.Millisecond = Convert.ToInt32(ts.Substring(14));
            }
            catch
            {
                hts.Millisecond = null;
            }

            return hts;
        }

        public override string ToString()
        {
            string ToReturn = "";

            if (Year.HasValue)
            {
                ToReturn = ToReturn + Year.Value.ToString("0000");
                if (Month.HasValue)
                {
                    ToReturn = ToReturn + Month.Value.ToString("00");
                    if (Day.HasValue)
                    {
                        ToReturn = ToReturn + Day.Value.ToString("00");
                        if (Hour.HasValue)
                        {
                            ToReturn = ToReturn + Hour.Value.ToString("00");
                            if (Minute.HasValue)
                            {
                                ToReturn = ToReturn + Minute.Value.ToString("00");
                                if (Second.HasValue)
                                {
                                    ToReturn = ToReturn + Second.Value.ToString("00");
                                    if (Millisecond.HasValue)
                                    {
                                        ToReturn = ToReturn + Millisecond.ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return ToReturn;
        }
        
        public DateTime ToDateTime()
        {
            int _Year = 0;
            int _Month = 0;
            int _Day = 0;
            int _Hour = 0;
            int _Minute = 0;
            int _Second = 0;
            int _Millisecond = 0;

            if (Year.HasValue)
            {
                _Year = Year.Value;
            }
            if (Month.HasValue)
            {
                _Month = Month.Value;
            }
            if (Day.HasValue)
            {
                _Day = Day.Value;
            }
            if (Hour.HasValue)
            {
                _Hour = Hour.Value;
            }
            if (Minute.HasValue)
            {
                _Minute = Minute.Value;
            }
            if (Second.HasValue)
            {
                _Second = Second.Value;
            }
            if (Millisecond.HasValue)
            {
                _Millisecond = Millisecond.Value;
            }

            DateTime ToReturn = new DateTime(_Year, _Month, _Day, _Hour, _Minute, _Second, _Millisecond);
            return ToReturn;
        }
    }
}