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
    }
}