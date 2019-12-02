using MvcFrameworkCml;
using MvcFrameworkCml.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcFrameworkBll
{
    public class UserLogicManager
    {
        IUserRepository _userRepository;
        public UserLogicManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

        public IEnumerable<EndUser> GetAll()
        {
            var users = _userRepository.GetAll();
            // return users without passwords
            return users.Select(u => u.ReturnWithoutSensitiveData());
        }

        public Boolean Add(EndUser user)
        {
            try
            {
                if (String.IsNullOrEmpty(user.Password) || String.IsNullOrEmpty(user.Email))
                {
                    //todo logging
                    throw new ArgumentNullException("Field on enduser missing !");
                }


                var salt = BCrypt.Net.BCrypt.GenerateSalt();
                var password = BCrypt.Net.BCrypt.HashPassword(user.Password, salt);
                user.Salt = salt;
                user.Password = password;
                user.DateJoined = DateTime.UtcNow.Date;
                user.Role = !String.IsNullOrWhiteSpace(user.Role) ? user.Role : Role.USER;
                user.IsActive = true;

                //todo - send confirmation mail
                //todo logging
                _userRepository.Add(user);

                return true;
            }
            catch (Exception e)
            {
                //todo logging
                return false;
            }
        }

        public Boolean ChangeName(String activeEmail, String name)
        {
            try
            {
                if (String.IsNullOrEmpty(name))
                {
                    //todo logging
                    throw new ArgumentNullException("Name cannot be null/empty!");
                }


                var user = Get(activeEmail);
                user.Name = name;

                _userRepository.Update(user);
                return true;
            }
            catch (Exception e)
            {
                //todo logging
                return false;
            }
        }

        public Boolean ChangeLastName(String activeEmail, String lastName)
        {
            try
            {
                if (String.IsNullOrEmpty(lastName))
                {
                    //todo logging
                    throw new ArgumentNullException("LastName cannot be null/empty!");
                }


                var user = Get(activeEmail);
                user.LastName = lastName;

                _userRepository.Update(user);
                return true;
            }
            catch (Exception e)
            {
                //todo logging
                return false;
            }
        }

        public Boolean ChangeEmail(String activeEmail, String email)
        {
            try
            {
                if (String.IsNullOrEmpty(email))
                {
                    //todo logging
                    throw new ArgumentNullException("Email cannot be null/empty!");
                }


                var user = Get(activeEmail);
                user.Email = email;

                _userRepository.Update(user);
                return true;
            }
            catch (Exception e)
            {
                //todo logging
                return false;
            }
        }

        public Boolean ChangePassword(String activeEmail, String password)
        {
            try
            {
                if (String.IsNullOrEmpty(password))
                {
                    //todo logging
                    throw new ArgumentNullException("Password cannot be null/empty!");
                }


                var user = Get(activeEmail);
                var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(password, user.Salt);
                user.Password = encryptedPassword;

                _userRepository.Update(user);
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

            // remove password before returning

            return user.ReturnWithoutSensitiveData();
        }

    }
}
