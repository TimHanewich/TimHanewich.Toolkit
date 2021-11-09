using System;

namespace TimHanewich.Toolkit.Geo
{
    public class Distance
    {
        private float _Miles;

        public static Distance FromMiles(float miles)
        {
            Distance ToReturn = new Distance();
            ToReturn._Miles = miles;
            return ToReturn;
        }

        public float Miles
        {
            get
            {
                return _Miles;
            }
        }

        public float Kilometers
        {
            get
            {
                return _Miles * 1.60934f;
            }
        }

        public float Feet
        {
            get
            {
                return _Miles * 5280f;
            }
        }
        
        public float Meters
        {
            get
            {
                return _Miles * 1609.34f;
            }
        }

        public float Inches
        {
            get
            {
                return _Miles * 63360f;
            }
        }
    }
}