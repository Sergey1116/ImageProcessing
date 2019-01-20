using System;
using System.Drawing;
using System.Text;

namespace ImageProcessing
{
    internal class AddMark : ImageProcessor
    {
        public AddMark(string path_directory, string name_new_directory) : base(path_directory, name_new_directory)
        { }

        public override void ProcessFiles()
        {
            foreach (var file in Files)
            {
                try
                {
                    using (Image picture = Image.FromFile(file.FullName))
                    {
                        var date_prop = picture.GetPropertyItem(0x9003);
                        var capture_date = Encoding.UTF8.GetString(date_prop
                            .Value)
                                                  .Remove(11)
                                                  .Replace(':', '-');

                        int width = picture.Width;
                        int height = picture.Height;

                        using (Graphics g = Graphics.FromImage(picture))
                        {
                            SizeF measured_size = g.MeasureString(capture_date,
                                                        new Font("Times New Roman", 150, FontStyle.Regular));

                            g.DrawString(capture_date, new Font("Times New Roman", 150),
                                             Brushes.Red, width - measured_size.Width, 0);
                        }
                        picture.Save(NewPathDirectory + file.Name);
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