using System;

namespace ImageProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            bool alive = true;
            while (alive)
            {
                Console.WriteLine("Введите путь к папке с изображениями");
                string PathDirectory = Console.ReadLine();

                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen;

                Console.WriteLine("Выберите действие:\n" +
                                  "1. Переименование изображений в соответствии с датой сьемки.\n" +
                                  "2. Добавления на изображение отметку, когда фото было сделано.\n" +
                                  "3. Сортировка изображений по папкам по годам.\n" +
                                  "4. Сортировка изображений по папкам по месту сьемки.\n5. Выйти из программы.");
                Console.ForegroundColor = color;

                ImageProcessor processor = null;
                try
                {
                    Console.WriteLine("Введите номер пункта:");
                    int command = Convert.ToInt32(Console.ReadLine());

                    switch (command)
                    {
                        case 1:
                            processor = new RenameImageShootingDate(PathDirectory, @"_RenameImageShootingDate\");
                            break;
                        case 2:
                            processor = new AddMark(PathDirectory, @"_AddMark\");
                            break;
                        case 3:
                            processor = new SortByYear(PathDirectory, @"_SortByYear\");
                            break;
                        case 4:
                            processor = new SortByLocation(PathDirectory, @"_SortByLocation\");
                            break;
                        case 5:
                            alive = false;
                            continue;
                    }
                    processor?.ProcessFiles();
                }
                catch (Exception ex)
                {
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = color;
                }
            }
        }
    }
}