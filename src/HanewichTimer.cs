using System;

namespace TimHanewich.Toolkit
{
    public class HanewichTimer
    {
        private DateTime? StartTime;
        private DateTime? EndTime;

        public void StartTimer()
        {
            StartTime = DateTime.UtcNow;
        }

        public void StopTimer()
        {
            EndTime = DateTime.UtcNow;
        }

        public TimeSpan GetElapsedTime()
        {
            if (StartTime != null && EndTime != null)
            {
                if (EndTime.Value < StartTime.Value)
                {
                    throw new Exception("The end time was before the start time. Please use the StartTimer and StopTimer methods in order.");
                }

                TimeSpan ts = EndTime.Value - StartTime.Value;
                return ts;
            }
            else
            {
                throw new Exception("You tried to get the elapsed time for a timer that has either not been started or not been stopped yet.");
            }       
        }
    }
}