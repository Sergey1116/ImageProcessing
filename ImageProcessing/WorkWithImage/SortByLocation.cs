using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using CityGPS;
using ImageProcessing.Extension;

namespace ImageProcessing
{
    class SortByLocation : ImageProcessor
    {
        public SortByLocation(string pathdirectory, string namenewdirectory) : base(pathdirectory, namenewdirectory)
        { }

        public override void ProcessFiles()
        {
            string sAttr1 = ConfigurationManager.AppSettings.Get("Key1");
            string sAttr2 = ConfigurationManager.AppSettings.Get("Key2");

            foreach (var file in _Files)
            {
                using (Image picture = Image.FromFile(file.FullName))
                {
                    try
                    {
                        string geocode = picture.GetLocationCoordinates();

                        var location = new Location();
                        string new_dir_name = location.ProcessLocation(sAttr1 + geocode + sAttr2);

                        var dir = new DirectoryInfo(_NewPathDirectory + new_dir_name + @"\");
                        if (!dir.Exists)
                        {
                            dir.Create();
                        }
                        file.CopyTo(_NewPathDirectory + new_dir_name + @"\" + file.Name);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}