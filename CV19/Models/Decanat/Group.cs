using System.Collections.Generic;
using CV19.Models.Interfaces;

namespace CV19.Models.Decanat
{
    internal class Group : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Student> Students { get; set; }

        public string Description { get; set; }
    }
}