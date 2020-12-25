using Tree.Core.Domain.Entities;

namespace Tree.Data.Entities.Test
{
    public class Student : Entity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Class { get; set; }
        public string Description { get; set; }
    }
}
