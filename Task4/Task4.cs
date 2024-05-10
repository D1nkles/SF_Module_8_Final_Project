namespace Task4 
{
    class Program 
    {
        static void Main(string[] args)
        {
            List<Student> studentList = new List<Student>();

            string userName = System.Environment.UserName;

            string DesktopPATH = Path.Combine("C:", "Users", userName, "Desktop");
            string dirPATH = Path.Combine(DesktopPATH, "Students");

            Console.Write("Введите путь до бинарного файла со студентами: ");
            string filePATH = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(filePATH) || !File.Exists(filePATH))
            {
                Console.WriteLine("Ошибка: файла по такому пути не существует!!!\n" +
                                  "Нажмите любую клавишу, чтобы закрыть приложение...");
                Console.ReadKey();
                Environment.Exit(0);
            }
            else
            {
                using FileStream fsRead = new FileStream(filePATH, FileMode.Open);
                {
                    BinaryReader br = new BinaryReader(fsRead);

                    while (fsRead.Position < fsRead.Length)
                    {
                        Student student = new Student();

                        student.Name = br.ReadString();
                        student.Group = br.ReadString();
                        student.DateOfBirth = DateTime.FromBinary(br.ReadInt64());
                        student.GPA = br.ReadDecimal();

                        studentList.Add(student);
                    }
                }

                if (!Directory.Exists(dirPATH))
                {
                    Directory.CreateDirectory(dirPATH);
                }

                foreach (Student student in studentList)
                {
                    string groupFilePATH = Path.Combine(dirPATH, student.Group + ".txt");
                    FileInfo groupFile = new FileInfo(groupFilePATH);

                    using StreamWriter streamWriter = groupFile.AppendText();
                    {
                        if (!groupFile.Exists)
                        {
                            groupFile.Create();
                        }
                        streamWriter.WriteLine($"{student.Name}, {student.DateOfBirth}, {student.GPA}");
                    }
                }
            }
        }
    }
}