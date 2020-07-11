using System.Linq;
using CV19.Models.Decanat;
using CV19.Services.Base;

namespace CV19.Services.Students
{
    class GroupsRepository : RepositoryInMemory<Group>
    {
        public GroupsRepository() : base(TestData.Groups) { }

        public Group Get(string GroupName) => GetAll().FirstOrDefault(g => g.Name == GroupName);

        protected override void Update(Group Source, Group Destination)
        {
            Destination.Name = Source.Name;
            Destination.Description = Source.Description;
        }
    }
}