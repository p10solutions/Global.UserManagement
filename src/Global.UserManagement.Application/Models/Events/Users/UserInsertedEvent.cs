using Global.UserManagement.Application.Entities;

namespace Global.UserAudit.Application.Models.Events.Users
{
    public class UserInsertedEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public EProfile Profile { get; set; }
        public bool Active { get; set; }

        public UserInsertedEvent(Guid id, string name, DateTime dateBirth, EProfile profile, bool active)
        {
            Id = id;
            Name = name;
            DateBirth = dateBirth;
            Profile = profile;
            Active = active;
        }
    }
}
