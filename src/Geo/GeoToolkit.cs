using System;

namespace TimHanewich.Toolkit.Geo
{
    public class GeoToolkit
    {
        public static Distance MeasureDistance(Geolocation loc1, Geolocation loc2)
        {
            //Convert to radians
            double loc1_lat = Convert.ToDouble(loc1.Latitude) / (180d / Math.PI);
            double loc1_lon = Convert.ToDouble(loc1.Longitude) / (180d / Math.PI);
            double loc2_lat = Convert.ToDouble(loc2.Latitude) / (180d / Math.PI);
            double loc2_lon = Convert.ToDouble(loc2.Longitude) / (180d / Math.PI);

            double con = 3963d;
            double a = Math.Sin(loc1_lat);
            double b = Math.Sin(loc2_lat);
            double c = Math.Cos(loc1_lat);
            double d = Math.Cos(loc2_lat);
            double e = Math.Cos(loc2_lon - loc1_lon);
            double a_times_b = a * b;
            double c_times_d_times_e = c * d * e;
            double ToArcCos = a_times_b + c_times_d_times_e;
            double ArcCos = Math.Acos(ToArcCos);
            double DistanceMiles = con * ArcCos;

            Distance ToReturn = Distance.FromMiles(Convert.ToSingle(DistanceMiles));
            return ToReturn;
        }
    }
}