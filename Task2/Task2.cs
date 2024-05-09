class Task2
{
    static long GetDirectorySize(string PATH)
    {
        long dirSize = 0;

        if (Directory.Exists(PATH))
        {
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
        else
        {
            Console.WriteLine("Ошибка: Папки по такому пути не существует!!!");
            return dirSize;
        }
    }

    static void Main(string[] args)
    {
        while (true) 
        {
            Console.Write("Введите путь к папке: ");
            string PATH = Console.ReadLine();
            var fileSize = GetDirectorySize(PATH);
            Console.WriteLine($"Размер папки по указанному пути равен: {fileSize} байт");
        }
    }
}
