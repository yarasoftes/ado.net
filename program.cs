using System;
using System.Collections.Generic;
using ado.net.Data;
using ado.net.Models;

namespace ado.net
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
                Console.WriteLine("=== Главное меню (Работа с Dapper) ===");
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
                Console.WriteLine("1. Показать всех (Query)");
                Console.WriteLine("2. Найти по части имени (Query)");
                Console.WriteLine("3. Найти по ID (QueryFirstOrDefault)");
                Console.WriteLine("4. Найти по точному ФИО (QuerySingle)");
                Console.WriteLine("0. Назад");
                Console.Write("Выбор: ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                try
                {
                    if (choice == "1")
                    {
                        PrintList(repo.GetAll());
                    }
                    else if (choice == "2")
                    {
                        Console.Write("Введите часть имени: ");
                        PrintList(repo.FindByName(Console.ReadLine()));
                    }
                    else if (choice == "3")
                    {
                        Console.Write("Введите ID: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            Student student = repo.GetById(id);
                            Console.WriteLine("\n--- Результат ---");
                            Console.WriteLine(student == null ? "Студент не найден." : student.ToString());
                            Console.ReadKey();
                        }
                    }
                    else if (choice == "4")
                    {
                        Console.Write("Введите точное ФИО: ");
                        Student student = repo.GetByExactName(Console.ReadLine());
                        Console.WriteLine("\n--- Результат ---");
                        Console.WriteLine(student.ToString());
                        Console.ReadKey();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nОшибка: {ex.Message}");
                    Console.WriteLine("(QuerySingle выдает ошибку, если найдено 0 или больше 1 записи)");
                    Console.ReadKey();
                }
            }
        }

        static void GroupsMenu(GroupRepository repo)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Группы ===");
                Console.WriteLine("1. Показать все (Query)");
                Console.WriteLine("2. Найти по части названия (Query)");
                Console.WriteLine("3. Найти по ID (QueryFirstOrDefault)");
                Console.WriteLine("4. Найти по точному названию (QuerySingle)");
                Console.WriteLine("0. Назад");
                Console.Write("Выбор: ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                try
                {
                    if (choice == "1")
                    {
                        PrintList(repo.GetAll());
                    }
                    else if (choice == "2")
                    {
                        Console.Write("Введите часть названия: ");
                        PrintList(repo.FindByName(Console.ReadLine()));
                    }
                    else if (choice == "3")
                    {
                        Console.Write("Введите ID: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            Group group = repo.GetById(id);
                            Console.WriteLine("\n--- Результат ---");
                            Console.WriteLine(group == null ? "Группа не найдена." : group.ToString());
                            Console.ReadKey();
                        }
                    }
                    else if (choice == "4")
                    {
                        Console.Write("Введите точное название: ");
                        Group group = repo.GetByExactName(Console.ReadLine());
                        Console.WriteLine("\n--- Результат ---");
                        Console.WriteLine(group.ToString());
                        Console.ReadKey();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nОшибка: {ex.Message}");
                    Console.WriteLine("(QuerySingle выдает ошибку, если найдено 0 или больше 1 записи)");
                    Console.ReadKey();
                }
            }
        }

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