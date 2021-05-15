using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;

namespace Cml.DataModels
{
    public class EndUser
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
        public Boolean EmailConfirmed { get; set; }
        public DateTime DateJoined { get; set; }
        public String Role { get; set; }

        public override String ToString()
        {
            return $"{Name} {LastName}";
        }

        public EndUser()
        {

        }

        public EndUser(ClaimsPrincipal user)
        {
            var claims = user.Claims.ToList();

            var idClaim = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypesExt.Id));

            if (idClaim == null || !int.TryParse(idClaim.Value, out Int32 id))
            {
                //todo logging
                throw new NullReferenceException("Missing id claim");
            }
            else Id = id;

            Name = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value;
            LastName = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypesExt.LastName)).Value;
            Role = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role)).Value;
        }

        public EndUser(Int32 id, String name, String lastName, String email, String password)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public EndUser(EndUser user)
        {
            Id = user.Id;
            Name = user.Name;
            LastName = user.LastName;
            Email = user.Email;
            Password = user.Password;
            Salt = user.Salt;
            IsActive = user.IsActive;
            DateJoined = user.DateJoined;
            Role = user.Role;
            EmailConfirmed = user.EmailConfirmed;
        }

        public EndUser ReturnWithoutSensitiveData()
        {
            var userWithoutSensitiveData = new EndUser(this);
            userWithoutSensitiveData.Email = null;
            userWithoutSensitiveData.Password = null;
            userWithoutSensitiveData.Salt = null;
            return userWithoutSensitiveData;
        }
    }
}
