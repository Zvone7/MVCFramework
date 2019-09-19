using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;

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

        public User()
        {

        }

        public User(ClaimsPrincipal user)
        {
            var claims = user.Claims.ToList();

            if (!Int32.TryParse(claims.FirstOrDefault(x => x.Type.Equals(ClaimTypesExt.Id)).Value, out Int32 id))
            {
                //todo logging
                throw new NullReferenceException("Missing id claim");
            }
            else Id = id;

            Name = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value;
            LastName = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypesExt.LastName)).Value;
            Email = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email)).Value;
            Role = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role)).Value;

        }
    }
}
