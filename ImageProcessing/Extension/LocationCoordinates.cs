using System;
using System.Drawing;

namespace ImageProcessing.Extension
{
    static class LocationCoordinates
    {
        static public string GetLocationCoordinates(this Image picture)
        {
            string gps_latitude_ref = BitConverter.ToChar(picture.GetPropertyItem(1).Value, 0).ToString();
            string latitude = Decode(picture.GetPropertyItem(2));
            string gps_longitude_ref = BitConverter.ToChar(picture.GetPropertyItem(3).Value, 0).ToString();
            string longitude = Decode(picture.GetPropertyItem(4));

            string geocode = $"{latitude}{gps_latitude_ref},{longitude}{gps_longitude_ref}";
            return geocode;
        }

        private static string Decode(System.Drawing.Imaging.PropertyItem propertyItem)
        {
            uint dN = BitConverter.ToUInt32(propertyItem.Value, 0);
            uint dD = BitConverter.ToUInt32(propertyItem.Value, 4);
            uint mN = BitConverter.ToUInt32(propertyItem.Value, 8);
            uint mD = BitConverter.ToUInt32(propertyItem.Value, 12);
            uint sN = BitConverter.ToUInt32(propertyItem.Value, 16);
            uint sD = BitConverter.ToUInt32(propertyItem.Value, 20);

            decimal deg;
            decimal min;
            decimal sec;

            if (dD > 0) { deg = (decimal)dN / dD; } else { deg = dN; }
            if (mD > 0) { min = (decimal)mN / mD; } else { min = mN; }
            if (sD > 0) { sec = (decimal)sN / sD; } else { sec = sN; }

            if (sec == 0) return string.Format($"{deg}°{min:0.###}'");
            else return string.Format($"{deg}°{min:0}'{sec:0.#}");
        }
    }
}