using ProfilerModels;
using ProfilerModels.Infrastructure;
using System;
using System.Collections.Generic;

namespace ProfilerLogic
{
    public class UserManager
    {
        IUserRepository _userRepository;
        AppSettings _appSettings;
        public UserManager(AppSettings appSettings, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _appSettings = appSettings;
        }

        public User GetUserById(Int32 id)
        {
            var user = _userRepository.Get(id);
            return user;
        }

        public User Authenticate(string email, string password)
        {
            var user = _userRepository.Get(email);

            // return null if user not found
            if (user == null)
            {
                //todo logging
                return null;
            }

            var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(password, user.Salt);

            if (!_userRepository.TryAuthenticate(email, encryptedPassword))
            {
                //todo logging
                return null;
            }

            //// authentication successful so generate jwt token
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim(ClaimTypes.Name, user.ToString()),
            //        new Claim(ClaimTypes.Role, user.Role)
            //    }),
            //    Expires = DateTime.UtcNow.AddDays(7),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};
            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;
            user.Salt = null;

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
            // return users without passwords
            //return _users.Select(x =>
            //{
            //    x.Password = null;
            //    return x;
            //});
        }

        public User GetById(Int32 id)
        {
            var user = _userRepository.Get(id);

            return user;
            // return user without password
            //if (user != null)
            //    user.Password = null;

            //return user;
        }

    }
}
