namespace Global.UserManagement.Application.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public EProfile Profile { get; set; }
        public bool Active { get; set; }

        public User()
        {

        }

        public User(string name, DateTime dateBirth, EProfile profile)
        {
            Id = Guid.NewGuid();
            Name = name;
            DateBirth = dateBirth;
            Profile = profile;
            Active = true;
        }
    }
}
