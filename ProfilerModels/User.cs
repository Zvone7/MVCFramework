using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfilerModels
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String LastName { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Salt { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime? DateJoined { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

        //public User(Int32 id, String username, String name, String lastName)
        //{
        //    Name = name;
        //    LastName = lastName;
        //    Username = username;
        //}

        //public override String ToString()
        //{
        //    return $"{Name ?? ""} {LastName ?? ""}";
        //}
    }
}
