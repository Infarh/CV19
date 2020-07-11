using System;
using System.Collections.Generic;
using System.Text;
using CV19.Models.Decanat;

namespace CV19.Services.Students
{
    class StudentsManager
    {
        private readonly StudentsRepository _Students;
        private readonly GroupsRepository _Groups;

        public IEnumerable<Student> Students => _Students.GetAll();

        public IEnumerable<Group> Groups => _Groups.GetAll();

        public StudentsManager(StudentsRepository Students, GroupsRepository Groups)
        {
            _Students = Students;
            _Groups = Groups;
        }

        public bool Create(Student Student, string GroupName)
        {
            if (Student is null) throw new ArgumentNullException(nameof(Student));
            if(string.IsNullOrWhiteSpace(GroupName)) throw new ArgumentException("Некорректное имя группы", nameof(GroupName));

            var group = _Groups.Get(GroupName);
            if (group is null)
            {
                group = new Group { Name = GroupName };
                _Groups.Add(group);
            }
            group.Students.Add(Student);
            _Students.Add(Student);
            return true;
        }

        public void Update(Student Student) => _Students.Update(Student.Id, Student);
    }
}
