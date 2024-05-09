class Task1
{
    static void Main(string[] args)
    {
        var timeCheck = TimeSpan.FromMinutes(30);

        while (true)
        {
            Console.Write("Введите путь к папке: ");
            string PATH = Console.ReadLine();

            if (Directory.Exists(PATH))
            {
                DirectoryInfo mainDirectory = new DirectoryInfo(PATH);

                var directories = mainDirectory.GetDirectories();
                var files = mainDirectory.GetFiles();

                foreach (var directory in directories)
                {
                    try
                    {
                        var timeUnused = DateTime.Now - directory.LastAccessTime;
                        if (timeUnused > timeCheck)
                        {
                            directory.Delete(true);
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine($"Ошибка: Системе не удалось получить доступ к папке {directory.Name}!!!");
                    }
                }

                foreach (var file in files)
                {
                    try
                    {
                        var timeUnused = DateTime.Now - file.LastAccessTime;
                        if (timeUnused > timeCheck)
                        {
                            file.Delete();
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine($"Ошибка: Системе не удалось получить доступ к файлу {file.Name}!!!");
                    }
                }
            }
            else
            {
                Console.WriteLine("Ошибка: Папки по такому пути не существует!!!");
            }
        }
    }
}