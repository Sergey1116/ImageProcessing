using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace ImageProcessing
{
    class SortByYear : ImageProcessor
    {
        public SortByYear(string pathdirectory, string namenewdirectory) : base(pathdirectory, namenewdirectory)
        { }

        public override void ProcessFiles()
        {
            foreach (var file in Files)
            {
                using (Image picture = Image.FromFile(file.FullName))
                {
                    try
                    {
                        var date_prop = picture.GetPropertyItem(0x9003);
                        var capture_date = Encoding.UTF8.GetString(date_prop.Value)
                                                  .Remove(4);

                        var new_directory_year = new DirectoryInfo(NewPathDirectory + capture_date + @"\");
                        if (!new_directory_year.Exists)
                        {
                            new_directory_year.Create();
                        }
                        file.CopyTo(NewPathDirectory + capture_date + @"\" + file.Name);
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