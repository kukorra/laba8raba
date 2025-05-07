using System;

public class Program
{
    public static void Main()
    {
        var db = new GradeDatabase(@"C:\Users\Сергей\source\repos\8labrab\grades.bin");

        while (true)
        {
            Console.WriteLine("\n--- ЖУРНАЛ КЛАССА ---");
            Console.WriteLine("1. Просмотр базы");
            Console.WriteLine("2. Добавить запись");
            Console.WriteLine("3. Удалить по ID");
            Console.WriteLine("4. Запросы");
            Console.WriteLine("0. Выход");
            Console.Write("Выбор: ");

            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        db.View();
                        break;

                    case "2":
                        Console.Write("Имя: ");
                        var name = Console.ReadLine();

                        Console.Write("ID: ");
                        var id = int.Parse(Console.ReadLine());

                        Console.Write("Предмет: ");
                        var subject = Console.ReadLine();

                        Console.Write("Дата (дд.мм.гггг): ");
                        var date = DateTime.Parse(Console.ReadLine());

                        Console.Write("Оценка: ");
                        var grade = double.Parse(Console.ReadLine());

                        db.Add(new GradeRecord(name, id, subject, date, grade));
                        break;

                    case "3":
                        Console.Write("Введите ID для удаления: ");
                        var deleteId = int.Parse(Console.ReadLine());
                        db.Delete(deleteId);
                        break;

                    case "4":
                        Console.WriteLine("1. Оценки по предмету");
                        Console.WriteLine("2. Оценки ученика");
                        Console.WriteLine("3. Средняя оценка ученика");
                        Console.WriteLine("4. Кол-во оценок ниже заданного");
                        Console.Write("Выбор запроса: ");

                        var queryChoice = Console.ReadLine();
                        switch (queryChoice)
                        {
                            case "1":
                                Console.Write("Предмет: ");
                                var subj = Console.ReadLine();
                                foreach (var record in db.GetGradesBySubject(subj))
                                {
                                    Console.WriteLine(record);
                                }
                                break;

                            case "2":
                                Console.Write("Имя ученика: ");
                                var student = Console.ReadLine();
                                foreach (var record in db.GetGradesByStudent(student))
                                {
                                    Console.WriteLine(record);
                                }
                                break;

                            case "3":
                                Console.Write("ID ученика: ");
                                var sid = int.Parse(Console.ReadLine());
                                Console.WriteLine("Средняя: " + db.GetAverageGradeByStudent(sid));
                                break;

                            case "4":
                                Console.Write("Порог: ");
                                var threshold = double.Parse(Console.ReadLine());
                                Console.WriteLine("Количество: " + db.GetCountOfGradesBelow(threshold));
                                break;

                            default:
                                Console.WriteLine("Неверный запрос.");
                                break;
                        }
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }
    }
}
