class Task2
{
    static void Main(string[] args)
    {
        Console.Write("Введите путь к папке: ");
        string PATH = Console.ReadLine();
        if (Directory.Exists(PATH))
        {

        }
        else
        {
            Console.WriteLine("Ошибка: Папки по такому пути не существует!!!");
        }
    }
}
