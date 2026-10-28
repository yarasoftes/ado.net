namespace AdoNetLesson8.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int GroupId { get; set; }

        public override string ToString()
        {
            return $"ID: {Id,-3} | ФИО: {FullName,-25} | ID Группы: {GroupId}";
        }
    }
}