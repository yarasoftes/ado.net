namespace AdoNetLesson8.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"ID Группы: {Id,-3} | Название: {Name}";
        }
    }
}