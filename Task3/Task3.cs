class Task3
{
    static uint ClearFolder(string PATH)
    {
        var timeCheck = TimeSpan.FromMinutes(30);
        uint delCount = 0;

        DirectoryInfo directory = new DirectoryInfo(PATH);
        var directories = directory.GetDirectories();
        var files = directory.GetFiles();

        foreach (var file in files)
        {
            try
            {
                var timeUnused = DateTime.Now - file.LastAccessTime;
                if (timeUnused > timeCheck)
                {
                    file.Delete();
                    delCount++;
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Ошибка: Системе не удалось получить доступ к файлу {file.Name}!!!");
            }
        }

        foreach (var dir in directories)
        {
            try
            {
                var timeUnused = DateTime.Now - dir.LastAccessTime;
                if (timeUnused > timeCheck)
                {
                    delCount += ClearFolder(dir.FullName);
                    try
                    {
                        dir.Delete();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Ошибка: в папке, которую программа пыталась удалить, находится файл, не соответсвующий условию удаления!!!");
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Ошибка: Системе не удалось получить доступ к папке {dir.Name}!!!");
            }
        }
        return delCount;
    }

    static long GetDirectorySize(string PATH)
    {
        long dirSize = 0;

        var directory = new DirectoryInfo(PATH);
        var directories = directory.GetDirectories();
        var files = directory.GetFiles();

        if (files != null)
        {
            foreach (var file in files)
            {
                try
                {
                    dirSize += file.Length;
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine($"Ошибка: Системе не удалось получить доступ к файлу {file.Name}!!!");
                }
            }

            foreach (var dir in directories)
            {
                try
                {
                    dirSize += GetDirectorySize(dir.FullName);
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine($"Ошибка: Системе не удалось получить доступ к папке {dir.Name}!!!");
                }
            }
            return dirSize;
        }
        return dirSize;
    }

    static void StartCleaning(string PATH) 
    {
        if (Directory.Exists(PATH))
        {
            long initialSize = GetDirectorySize(PATH);
            uint delFiles = ClearFolder(PATH);
            long finalSize = GetDirectorySize(PATH);

            Console.WriteLine($"Исходный размер папки: {initialSize} байт\n" +
                              $"Удалено файлов: {delFiles}\n" +
                              $"Освобождено: {initialSize - finalSize} байт\n" +
                              $"Текущий размер папки: {finalSize} байт\n\n" +
                              $"Нажмите любую клавишу, чтобы продолжить...");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Ошибка: Папки по такому пути не существует!!!");
        }
    }

    static void Main(string[] args)
    {
        while (true) 
        {
            Console.Write("Введите путь к папке: ");
            string PATH = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(PATH))
            {
                StartCleaning(PATH);
            }
            else
            {
                Console.WriteLine("Вы ввели пустое значение, нажмите любую клавишу, чтобы попробовать снова...\n");
                Console.ReadKey();
            }
        }
        
    }
}