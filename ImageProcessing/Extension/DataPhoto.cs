using System.Drawing;
using System.Text;

namespace ImageProcessing.Extension
{
    static class DataPhoto
    {
        static public string GetDataPhoto(this Image picture)
        {
            var date_prop = picture.GetPropertyItem(0x9003);
            var capture_date = Encoding.UTF8.GetString(date_prop.Value)
                                .Remove(11)
                                .Replace(':', '-');

            return capture_date;
        }
    }
}