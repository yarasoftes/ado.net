using System;
using System.Collections.Generic;
using AdoNetLesson8.Data;
using AdoNetLesson8.Models;

namespace AdoNetLesson8
{
    class Program
    {
        private static string connectionString = @"Server=localhost;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;";

        static void Main(string[] args)
        {
            StudentRepository studentRepo = new StudentRepository(connectionString);
            GroupRepository groupRepo = new GroupRepository(connectionString);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Главное меню (Слоистая архитектура) ===");
                Console.WriteLine("1. Работа со студентами");
                Console.WriteLine("2. Работа с группами");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите раздел: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        StudentsMenu(studentRepo);
                        break;
                    case "2":
                        GroupsMenu(groupRepo);
                        break;
                    case "0":
                        return;
                }
            }
        }

        static void StudentsMenu(StudentRepository repo)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Студенты ===");
                Console.WriteLine("1. Показать всех");
                Console.WriteLine("2. Найти по имени");
                Console.WriteLine("0. Назад");
                Console.Write("Выбор: ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                List<Student> students = new List<Student>();

                if (choice == "1")
                {
                    students = repo.GetAll();
                }
                else if (choice == "2")
                {
                    Console.Write("Введите имя: ");
                    students = repo.FindByName(Console.ReadLine());
                }
                else continue;

                PrintList(students);
            }
        }

        static void GroupsMenu(GroupRepository repo)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Группы ===");
                Console.WriteLine("1. Показать все");
                Console.WriteLine("2. Найти по названию");
                Console.WriteLine("0. Назад");
                Console.Write("Выбор: ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                List<Group> groups = new List<Group>();

                if (choice == "1")
                {
                    groups = repo.GetAll();
                }
                else if (choice == "2")
                {
                    Console.Write("Введите название: ");
                    groups = repo.FindByName(Console.ReadLine());
                }
                else continue;

                PrintList(groups);
            }
        }

        // Универсальный метод для вывода любого списка (благодаря override ToString)
        static void PrintList<T>(List<T> items)
        {
            Console.WriteLine("\n--- Результат ---");
            if (items.Count == 0)
            {
                Console.WriteLine("Данные не найдены.");
            }
            else
            {
                foreach (var item in items)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}