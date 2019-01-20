using System;
using System.IO;

namespace ImageProcessing
{
    abstract class ImageProcessor
    {
        protected FileInfo[] Files;

        protected readonly string NewPathDirectory;

        public ImageProcessor(string path_directory, string name_new_directory)
        {
            NewPathDirectory = path_directory + name_new_directory;

            var old_directory = new DirectoryInfo(path_directory);

            if (old_directory.Exists)
            {
                var new_directory = new DirectoryInfo(NewPathDirectory);

                if (!new_directory.Exists)
                {
                    new_directory.Create();
                }
            }
            else
            {
                throw new Exception("Такой директории не существует!");
            }

            Files = old_directory.GetFiles(@"*.jpg", SearchOption.TopDirectoryOnly);
        }

        public abstract void ProcessFiles();
    }
}