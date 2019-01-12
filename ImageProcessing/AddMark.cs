using System;
using System.IO;
using System.Drawing;
using System.Text;

namespace ImageProcessing
{
    internal class AddMark : MyBaseClass
    {
        public AddMark(string pathdirectory, string namenewdirectory) : base(pathdirectory, namenewdirectory)
        { }

        public void GetFiles()
        {
            foreach (var file in files)
            {
                try
                {
                    using (Image Picture = Image.FromFile(file.FullName))
                    {
                        var DateProp = Picture.GetPropertyItem(0x9003);
                        var CaptureDate = Encoding.UTF8.GetString(DateProp.Value)
                                                  .Remove(11)
                                                  .Replace(':', '-');

                        int width = Picture.Width;
                        int height = Picture.Height;

                        using (Graphics g = Graphics.FromImage(Picture))
                        {
                            SizeF measuredSize = g.MeasureString(CaptureDate,
                                                        new Font("Times New Roman", 150, FontStyle.Regular));

                            g.DrawString(CaptureDate, new Font("Times New Roman", 150),
                                             Brushes.Red, width - measuredSize.Width, 0);
                        }
                        Picture.Save(newpathdirectory + file.Name);
                    }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}