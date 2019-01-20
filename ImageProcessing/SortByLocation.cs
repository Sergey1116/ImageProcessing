using System;
using System.Drawing;
using System.IO;
using CityGPS;


namespace ImageProcessing
{
    class SortByLocation : ImageProcessor
    {
        public SortByLocation(string pathdirectory, string namenewdirectory) : base(pathdirectory, namenewdirectory)
        { }

        public override void ProcessFiles()
        {
            foreach (var file in Files)
            {
                using (Image picture = Image.FromFile(file.FullName))
                {
                    try
                    {
                        string new_dir_name;

                        string gps_latitude_ref = BitConverter.ToChar(picture.GetPropertyItem(1).Value, 0).ToString();
                        string latitude = Decode(picture.GetPropertyItem(2));
                        string gps_longitude_ref = BitConverter.ToChar(picture.GetPropertyItem(3).Value, 0).ToString();
                        string longitude = Decode(picture.GetPropertyItem(4));

                        string geocode = $"{latitude}{gps_latitude_ref},{longitude}{gps_longitude_ref}";

                        new_dir_name = Location.ProcessLocation(geocode);

                        //WebRequest request = WebRequest.Create(
                        //                "https://geocode-maps.yandex.ru/1.x/" +
                        //                "?geocode=" + geocode + "&kind=locality&results=1");

                        //WebResponse response = request.GetResponse();
                        //using (Stream stream = response.GetResponseStream())
                        //{
                        //    using (StreamReader reader = new StreamReader(stream))
                        //    {
                        //        string str = reader.ReadToEnd();

                        //        int index1 = str.IndexOf("LocalityName");
                        //        int index2 = str.IndexOf("/LocalityName");
                        //        new_dir_name = str.Substring(index1 + 13, index2 - index1 - 14);
                        //    }
                        //}
                        //response.Close();

                        var dir = new DirectoryInfo(NewPathDirectory + new_dir_name + @"\");
                        if (!dir.Exists)
                        {
                            dir.Create();
                        }
                        file.CopyTo(NewPathDirectory + new_dir_name + @"\" + file.Name);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
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