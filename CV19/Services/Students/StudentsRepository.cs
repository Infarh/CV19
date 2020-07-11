using CV19.Models.Decanat;
using CV19.Services.Base;

namespace CV19.Services.Students
{
    class StudentsRepository : RepositoryInMemory<Student>
    {
        public StudentsRepository() : base(TestData.Students) { }

        protected override void Update(Student Source, Student Destination)
        {
            Destination.Name = Source.Name;
            Destination.Surname = Source.Surname;
            Destination.Patronymic = Source.Patronymic;
            Destination.Birthday = Source.Birthday;
            Destination.Rating = Source.Rating;
        }
    }
}
