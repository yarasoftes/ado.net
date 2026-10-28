using System;

namespace AdoNetLesson7
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int GroupId { get; set; }

        // Переопределяем метод ToString для удобного вывода в консоль (View)
        public override string ToString()
        {
            return $"ID: {Id,-3} | ФИО: {FullName,-25} | ID Группы: {GroupId}";
        }
    }
}