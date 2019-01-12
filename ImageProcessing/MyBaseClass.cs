using System;
using System.IO;

namespace ImageProcessing
{
    internal class MyBaseClass
    {
        public FileInfo[] files;

        public readonly string newpathdirectory;

        public MyBaseClass(string pathdirectory, string namenewdirectory)
        {
            newpathdirectory = pathdirectory + namenewdirectory;

            var olddirectory = new DirectoryInfo(pathdirectory);

            if (olddirectory.Exists)
            {
                var newdirectory = new DirectoryInfo(newpathdirectory);

                if (!newdirectory.Exists)
                {
                    newdirectory.Create();
                }
            }
            else
            {
                throw new Exception("Такой директории не существует!");
            }

            files = olddirectory.GetFiles(@"*.jpg", SearchOption.TopDirectoryOnly);
        }
    }
}