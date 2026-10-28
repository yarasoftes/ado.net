using System;
using System.Collections.Generic;

namespace AdoNetLesson7
{
    class Program
    {
        private static string connectionString = @"Server=localhost;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;";

        static void Main(string[] args)
        {
            // Создаем экземпляр репозитория (Слой доступа к данным)
            StudentRepository repository = new StudentRepository(connectionString);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Меню Учебного Заведения (Слоистая архитектура) ===");
                Console.WriteLine("1. Показать всех студентов");
                Console.WriteLine("2. Найти студента по имени");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var allStudents = repository.GetAll();
                        PrintStudents(allStudents);
                        break;
                    case "2":
                        Console.Write("Введите имя (или часть имени): ");
                        string name = Console.ReadLine();
                        var foundStudents = repository.FindByName(name);
                        PrintStudents(foundStudents);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // Вспомогательный метод View для вывода списка студентов на экран
        private static void PrintStudents(List<Student> students)
        {
            Console.WriteLine("\n--- Результат ---");
            if (students.Count == 0)
            {
                Console.WriteLine("Студенты не найдены.");
            }
            else
            {
                foreach (var student in students)
                {
                    // Здесь работает переопределенный метод ToString() из класса Student
                    Console.WriteLine(student.ToString());
                }
            }
            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }
    }
}