using CV19.Models.Decanat;
using CV19.Services.Base;

namespace CV19.Services.Students
{
    class GroupsRepository : RepositoryInMemory<Group>
    {
        protected override void Update(Group Source, Group Destination)
        {
            Destination.Name = Source.Name;
            Destination.Description = Source.Description;
        }
    }
}