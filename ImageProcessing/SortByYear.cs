using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace ImageProcessing
{
    class SortByYear : MyBaseClass
    {
        public SortByYear(string pathdirectory, string namenewdirectory) : base(pathdirectory, namenewdirectory)
        { }

        public void GetFiles()
        {
            foreach (var file in files)
            {
                using (Image Picture = Image.FromFile(file.FullName))
                {
                    try
                    {
                        var DateProp = Picture.GetPropertyItem(0x9003);
                        var CaptureDate = Encoding.UTF8.GetString(DateProp.Value)
                                                  .Remove(4);

                        var NewDirectoryYear = new DirectoryInfo(newpathdirectory + CaptureDate + @"\");
                        if (!NewDirectoryYear.Exists)
                        {
                            NewDirectoryYear.Create();
                        }
                        file.CopyTo(newpathdirectory + CaptureDate + @"\" + file.Name);
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