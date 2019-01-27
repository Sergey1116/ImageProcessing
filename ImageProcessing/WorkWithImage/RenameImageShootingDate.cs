using System;
using System.IO;
using System.Drawing;
using ImageProcessing.Extension;

namespace ImageProcessing
{
    internal class RenameImageShootingDate : ImageProcessor
    {
        public RenameImageShootingDate(string pathdirectory, string namenewdirectory) : base(pathdirectory, namenewdirectory)
        { }

        public override void ProcessFiles()
        {
            foreach (var file in _Files)
            {
                try
                {
                    Image picture = Image.FromFile(file.FullName);

                    string capture_date = picture.GetDataPhoto();

                    file.CopyTo(_NewPathDirectory + capture_date + file.Name);
                }
                catch (ArgumentException)
                {
                    try
                    {
                        file.CopyTo(_NewPathDirectory + file.CreationTime.ToString("yyyy-MM-dd ") + file.Name);
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