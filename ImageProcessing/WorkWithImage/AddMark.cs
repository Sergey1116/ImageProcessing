using ImageProcessing.Extension;
using System;
using System.Drawing;

namespace ImageProcessing
{
    internal class AddMark : ImageProcessor
    {
        public AddMark(string path_directory, string name_new_directory) : base(path_directory, name_new_directory)
        { }

        public override void ProcessFiles()
        {
            foreach (var file in _Files)
            {
                try
                {
                    using (Image picture = Image.FromFile(file.FullName))
                    {
                        string capture_date = picture.GetDataPhoto();

                        int width = picture.Width;
                        int height = picture.Height;

                        using (Graphics g = Graphics.FromImage(picture))
                        {
                            SizeF measured_size = g.MeasureString(capture_date,
                                                        new Font("Times New Roman", 150, FontStyle.Regular));

                            g.DrawString(capture_date, new Font("Times New Roman", 150),
                                             Brushes.Red, width - measured_size.Width, 0);
                        }
                        picture.Save(_NewPathDirectory + file.Name);
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