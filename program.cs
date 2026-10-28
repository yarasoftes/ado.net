using System;
using Microsoft.Data.SqlClient;

namespace AdoNetLesson5
{
    class Program
    {
        private static string connectionString = @"Server=localhost;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Меню Учебного Заведения ===");
                Console.WriteLine("1. Показать всех студентов");
                Console.WriteLine("2. Найти студента по имени");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllStudents();
                        break;
                    case "2":
                        Console.Write("Введите имя (или часть имени): ");
                        string name = Console.ReadLine();
                        FindStudentByName(name);
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

        static void ShowAllStudents()
        {
            Console.Clear();
            Console.WriteLine("--- Список всех студентов ---");

            string query = @"
                SELECT s.Id, s.FullName, g.Name AS GroupName 
                FROM Students s
                LEFT JOIN Groups g ON s.GroupId = g.Id
                ORDER BY s.Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("Студенты не найдены.");
                        }
                        else
                        {
                            Console.WriteLine($"{"ID",-5} | {"ФИО",-25} | {"Группа",-10}");
                            Console.WriteLine(new string('-', 45));
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string fullName = reader.GetString(1);
                                string groupName = reader.IsDBNull(2) ? "Нет группы" : reader.GetString(2);
                                Console.WriteLine($"{id,-5} | {fullName,-25} | {groupName,-10}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        static void FindStudentByName(string name)
        {
            Console.Clear();
            Console.WriteLine($"--- Результаты поиска по запросу: '{name}' ---");

            string query = @"
                SELECT s.Id, s.FullName, g.Name AS GroupName 
                FROM Students s
                LEFT JOIN Groups g ON s.GroupId = g.Id
                WHERE s.FullName LIKE @Name
                ORDER BY s.Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", $"%{name}%");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                Console.WriteLine("Студенты с таким именем не найдены.");
                            }
                            else
                            {
                                Console.WriteLine($"{"ID",-5} | {"ФИО",-25} | {"Группа",-10}");
                                Console.WriteLine(new string('-', 45));
                                while (reader.Read())
                                {
                                    int id = reader.GetInt32(0);
                                    string fullName = reader.GetString(1);
                                    string groupName = reader.IsDBNull(2) ? "Нет группы" : reader.GetString(2);
                                    Console.WriteLine($"{id,-5} | {fullName,-25} | {groupName,-10}");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при поиске данных: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }
    }
}