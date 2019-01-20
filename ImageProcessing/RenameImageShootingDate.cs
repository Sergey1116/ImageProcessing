using System;
using System.IO;
using System.Drawing;
using System.Text;

namespace ImageProcessing
{
    internal class RenameImageShootingDate : ImageProcessor
    {
        public RenameImageShootingDate(string pathdirectory, string namenewdirectory) : base(pathdirectory, namenewdirectory)
        { }

        public override void ProcessFiles()
        {
            foreach (var file in Files)
            {
                try
                {
                    Image picture = Image.FromFile(file.FullName);
                    var date_prop = picture.GetPropertyItem(0x9003);
                    var capture_date = Encoding.UTF8.GetString(date_prop.Value)
                                        .Remove(11)
                                        .Replace(':', '-');
                    file.CopyTo(NewPathDirectory + capture_date + file.Name);
                }
                catch (ArgumentException)
                {
                    try
                    {
                        file.CopyTo(NewPathDirectory + file.CreationTime.ToString("yyyy-MM-dd ") + file.Name);
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