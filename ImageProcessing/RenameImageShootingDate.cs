using System;
using System.IO;
using System.Drawing;
using System.Text;

namespace ImageProcessing
{
    internal class RenameImageShootingDate : MyBaseClass
    {
        public RenameImageShootingDate(string pathdirectory, string namenewdirectory) : base(pathdirectory, namenewdirectory)
        { }

        public void GetFiles()
        {
            foreach (var file in files)
            {
                try
                {
                    Image Picture = Image.FromFile(file.FullName);
                    var DateProp = Picture.GetPropertyItem(0x9003);
                    var CaptureDate = Encoding.UTF8.GetString(DateProp.Value)
                                        .Remove(11)
                                        .Replace(':', '-');
                    file.CopyTo(newpathdirectory + CaptureDate + file.Name);
                }
                catch (ArgumentException)
                {
                    try
                    {
                        file.CopyTo(newpathdirectory + file.CreationTime.ToString("yyyy-MM-dd ") + file.Name);
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}