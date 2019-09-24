using ProfilerModels;
using ProfilerModels.Infrastructure;
using System;
using System.Collections.Generic;

namespace ProfilerLogic
{
    public class UserLogicManager
    {
        IUserRepository _userRepository;
        AppSettings _appSettings;
        public UserLogicManager(AppSettings appSettings, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _appSettings = appSettings;
        }

        public EndUser Get(Int32 id)
        {
            if (id <= 0)
            {
                //todo logging
                throw new ArgumentNullException("Invalid ID !");
            }
            var user = _userRepository.Get(id);
            return user;
        }
        public EndUser Get(String email)
        {
            if (String.IsNullOrEmpty(email))
            {
                //todo logging
                throw new ArgumentNullException("Email can't be null/empty !");
            }
            var user = _userRepository.Get(email);
            return user;
        }

        public Boolean AddOrUpdate(EndUser user)
        {
            try
            {
                if (String.IsNullOrEmpty(user.Name) ||
                    String.IsNullOrEmpty(user.LastName) ||
                    String.IsNullOrEmpty(user.Password) ||
                    String.IsNullOrEmpty(user.Email))
                {
                    //todo logging
                    throw new ArgumentNullException("Field on enduser missing !");
                }


                // new user (Register)
                if (user.Id == 0)
                {
                    var salt = BCrypt.Net.BCrypt.GenerateSalt();
                    var password = BCrypt.Net.BCrypt.HashPassword(user.Password, salt);
                    user.Salt = salt;
                    user.Password = password;
                    user.DateJoined = DateTime.UtcNow.Date;
                    user.Role = Role.USER;
                    user.IsActive = true;

                    //todo - send confirmation mail
                    //todo logging
                    _userRepository.Add(user);

                }
                // edit user
                else
                {
                    //todo logging
                    var dbUser = Get(user.Email);
                    var encryptedPasswordForCheck = BCrypt.Net.BCrypt.HashPassword(user.Password, dbUser.Salt);

                    user.Id = dbUser.Id;
                    user.DateJoined = dbUser.DateJoined;
                    user.Role = dbUser.Role;
                    user.Salt = dbUser.Salt;
                    user.Password = encryptedPasswordForCheck;
                    if (String.IsNullOrWhiteSpace(user.Name)) user.Name = dbUser.Name;
                    if (String.IsNullOrWhiteSpace(user.LastName)) user.LastName = dbUser.LastName;
                    if (String.IsNullOrWhiteSpace(user.Email)) user.Email = dbUser.Email;

                    if (encryptedPasswordForCheck.Equals(dbUser.Password))
                    {
                        _userRepository.Update(user);
                    }
                    else
                    {
                        //todo logging
                        throw new Exception("Password not matching!");
                        return false;
                    }

                }

                return true;
            }
            catch (Exception e)
            {
                //todo logging
                return false;
            }
        }

        public EndUser Authenticate(string email, string password)
        {
            var user = Get(email);

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

        public IEnumerable<EndUser> GetAll()
        {
            return _userRepository.GetAll();
            // return users without passwords
            //return _users.Select(x =>
            //{
            //    x.Password = null;
            //    return x;
            //});
        }

    }
}
