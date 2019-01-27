using System;
using System.IO;

namespace ImageProcessing
{
    abstract class ImageProcessor
    {
        protected FileInfo[] _Files;

        protected readonly string _NewPathDirectory;

        public ImageProcessor(string path_directory, string name_new_directory)
        {
            _NewPathDirectory = path_directory + name_new_directory;

            var old_directory = new DirectoryInfo(path_directory);

            if (old_directory.Exists)
            {
                var new_directory = new DirectoryInfo(_NewPathDirectory);

                if (!new_directory.Exists)
                {
                    new_directory.Create();
                }
            }
            else
            {
                throw new Exception("Такой директории не существует!");
            }

            _Files = old_directory.GetFiles(@"*.jpg", SearchOption.TopDirectoryOnly);
        }

        public abstract void ProcessFiles();
    }
}