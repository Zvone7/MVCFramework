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
        public String Email { get; set; }
        public String Password { get; set; }
        public String Salt { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime? DateJoined { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

        public override string ToString()
        {
            return $"{Name} {LastName}";
        }
    }
}
