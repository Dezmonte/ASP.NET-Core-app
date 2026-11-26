namespace WebApplication2.Models
{
    public class Person
    {
        public string Name { get; set; }
        public string Topic { get; set; }
        public Person(string name, string topic)
        {
            Name = name;
            Topic = topic;
        }
    }
}
