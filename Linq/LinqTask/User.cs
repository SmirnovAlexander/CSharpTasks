using System;

namespace LinqTask
{
    /// <summary>
    /// Class to store user information.
    /// </summary>
    public class User : IEquatable<User>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public User(int id, string name, string surname)
        {
            ID = id;
            Name = name;
            Surname = surname;
        }

        public override string ToString()
        {
            return string.Format("ID={0}: {1} {2}", ID, Name, Surname);
        }

        public bool Equals(User other)
        {
            return (this.ID == other.ID) && (this.Name == other.Name) && (this.Surname == other.Surname);
        }
    }
}
