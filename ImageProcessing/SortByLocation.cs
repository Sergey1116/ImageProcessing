using System;
using System.Drawing;
using System.IO;
using System.Net;

namespace ImageProcessing
{
    class SortByLocation : MyBaseClass
    {
        public SortByLocation(string pathdirectory, string namenewdirectory) : base(pathdirectory, namenewdirectory)
        { }

        public void GetFiles()
        {
            foreach (var file in files)
            {
                using (Image Picture = Image.FromFile(file.FullName))
                {
                    try
                    {
                        string newDirName;

                        string gpsLatitudeRef = BitConverter.ToChar(Picture.GetPropertyItem(1).Value, 0).ToString();
                        string latitude = Decode(Picture.GetPropertyItem(2));
                        string gpsLongitudeRef = BitConverter.ToChar(Picture.GetPropertyItem(3).Value, 0).ToString();
                        string longitude = Decode(Picture.GetPropertyItem(4));

                        string geocode = latitude + gpsLatitudeRef + "," + longitude + gpsLongitudeRef;

                        WebRequest request = WebRequest.Create(
                                        "https://geocode-maps.yandex.ru/1.x/" +
                                        "?geocode=" + geocode + "&kind=locality&results=1");

                        WebResponse response = request.GetResponse();
                        using (Stream stream = response.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                string str = reader.ReadToEnd();

                                int index1 = str.IndexOf("LocalityName");
                                int index2 = str.IndexOf("/LocalityName");
                                newDirName = str.Substring(index1 + 13, index2 - index1 - 14);
                            }
                        }
                        response.Close();

                        var dir3 = new DirectoryInfo(newpathdirectory + newDirName + @"\");
                        if (!dir3.Exists)
                        {
                            dir3.Create();
                        }
                        file.CopyTo(newpathdirectory + newDirName + @"\" + file.Name);

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
            else return string.Format($"{deg}°{min:0}'{sec:0.#}\"");
        }
    }
}