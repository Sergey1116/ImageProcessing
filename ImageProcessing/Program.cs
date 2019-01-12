using System;
using System.IO;

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

                Console.WriteLine("1. Переименование изображений в соответствии с датой сьемки.\n" +
                                  "2. Добавления на изображение отметку, когда фото было сделано.\n" +
                                  "3. Сортировка изображений по папкам по годам.\n" +
                                  "4. Сортировка изображений по папкам по месту сьемки.\n5. Выйти из программы.");

                Console.ForegroundColor = color;
                try
                {
                    Console.WriteLine("Введите номер пункта:");
                    int command = Convert.ToInt32(Console.ReadLine());

                    switch (command)
                    {
                        case 1:
                            var a = new RenameImageShootingDate(PathDirectory, @"_RenameImageShootingDate\");
                            a.GetFiles();
                            break;
                        case 2:
                            var b = new AddMark(PathDirectory, @"_AddMark\");
                            b.GetFiles();
                            break;
                        case 3:
                            var c = new SortByYear(PathDirectory, @"_SortByYear\");
                            c.GetFiles();
                            break;
                        case 4:
                            var d = new SortByLocation(PathDirectory, @"_SortByLocation\");
                            d.GetFiles();

                            break;
                        case 5:
                            alive = false;
                            continue;
                    }
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