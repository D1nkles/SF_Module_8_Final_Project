using System.IO;

class Task1
{
    static void ClearFolder(string PATH)
    {
        var timeCheck = TimeSpan.FromMinutes(30);

        if (Directory.Exists(PATH))
        {
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
                        ClearFolder(dir.FullName);
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
        }
        else
        {
            Console.WriteLine("Ошибка: Папки по такому пути не существует!!!");
        }

    }

    static void Main(string[] args)
    {

        Console.Write("Введите путь к папке: ");
        string PATH = Console.ReadLine();
        ClearFolder(PATH);
    }
}