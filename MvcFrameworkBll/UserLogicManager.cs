using Microsoft.Extensions.Logging;
using MvcFrameworkCml;
using MvcFrameworkCml.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFrameworkBll
{
    public class UserLogicManager : CustomBaseLogicManager
    {
        private readonly IUserRepository _userRepository_;

        public UserLogicManager(IUserRepository userRepository, ILogger logger) : base(logger)
        {
            _userRepository_ = userRepository;
        }

        public async Task<EndUser> GetAsync(Int32 id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger_.LogError($"Unable to Get user with id {id}");
                    return null;
                }
                var user = await _userRepository_.GetAsync(id);
                return user;
            }
            catch (Exception e)
            {
                _logger_.LogError($"Unable to Get user with id {id}|\n {e.Message} |\n {e.StackTrace}");
                return null;
            }
        }

        public async Task<EndUser> GetAsync(String email)
        {
            try
            {
                if (String.IsNullOrEmpty(email))
                {
                    _logger_.LogError($"Unable to Get user - email null/empty.");
                    return null;
                }
                var user = await _userRepository_.GetAsync(email);
                return user;
            }
            catch (Exception e)
            {
                _logger_.LogError($"Unable to Get user with username {email}|\n {e.Message} |\n {e.StackTrace}");
                return null;
            }
        }

        public async Task<IEnumerable<EndUser>> GetAllAsync()
        {
            try
            {
                var users = await _userRepository_.GetAllAsync();
                // return users without passwords
                return users.Select(u => u.ReturnWithoutSensitiveData());
            }
            catch (Exception e)
            {
                _logger_.LogError($"Unable to GetAll users |\n {e.Message} |\n {e.StackTrace}");
                return null;
            }
        }

        public async Task<Boolean> AddAsync(EndUser user)
        {
            try
            {
                if (String.IsNullOrEmpty(user.Password) || String.IsNullOrEmpty(user.Email))
                {
                    _logger_.LogError($"Unable to Authenticate user - user/password are null/empty.");
                    return false;
                }
                var salt = BCrypt.Net.BCrypt.GenerateSalt();
                var password = BCrypt.Net.BCrypt.HashPassword(user.Password, salt);
                user.Salt = salt;
                user.Password = password;
                user.DateJoined = DateTime.UtcNow.Date;
                user.Role = !String.IsNullOrWhiteSpace(user.Role) ? user.Role : Role.USER;
                user.IsActive = true;

                //todo - send confirmation mail
                await _userRepository_.AddAsync(user);
                return true;
            }
            catch (Exception e)
            {
                _logger_.LogError($"Unable to Add user |\n {e.Message} |\n {e.StackTrace}");
                return false;
            }
        }

        public async Task<Boolean> ChangeName(String activeEmail, String name)
        {
            try
            {
                if (String.IsNullOrEmpty(name))
                {
                    _logger_.LogError($"Unable to Change name of user - name null/empty for user {activeEmail}");
                    return false;
                }
                var user = await GetAsync(activeEmail);
                user.Name = name;
                await _userRepository_.UpdateAsync(user);
                return true;
            }
            catch (Exception e)
            {
                _logger_.LogError($"Unable to Change name of user with email {activeEmail}|\n {e.Message} |\n {e.StackTrace}");
                return false;
            }
        }

        public async Task<Boolean> ChangeLastName(String activeEmail, String lastName)
        {
            try
            {
                if (String.IsNullOrEmpty(lastName))
                {
                    _logger_.LogError($"Unable to Change lastname of user - lastname null/empty for user {activeEmail}");
                    return false;
                }
                var user = await GetAsync(activeEmail);
                user.LastName = lastName;
                await _userRepository_.UpdateAsync(user);
                return true;
            }
            catch (Exception e)
            {
                _logger_.LogError($"Unable to Change lastname of user with email {activeEmail}|\n {e.Message} |\n {e.StackTrace}");
                return false;
            }
        }

        public async Task<Boolean> ChangeEmail(String activeEmail, String email)
        {
            try
            {
                if (String.IsNullOrEmpty(email))
                {
                    _logger_.LogError($"Unable to Change email of user - email null/empty for user {activeEmail}");
                    return false;
                }
                var user = await GetAsync(activeEmail);
                user.Email = email;
                await _userRepository_.UpdateAsync(user);
                return true;
            }
            catch (Exception e)
            {
                _logger_.LogError($"Unable to Change email of user with email {activeEmail}|\n {e.Message} |\n {e.StackTrace}");
                return false;
            }
        }

        public async Task<Boolean> ChangePassword(String activeEmail, String password)
        {
            try
            {
                if (String.IsNullOrEmpty(password))
                {
                    _logger_.LogError($"Unable to Change password of user - password null/empty for user {activeEmail}");
                    return false;
                }
                var user = await GetAsync(activeEmail);
                var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(password, user.Salt);
                user.Password = encryptedPassword;
                await _userRepository_.UpdateAsync(user);
                return true;
            }
            catch (Exception e)
            {
                _logger_.LogError($"Unable to Change password of user with email {activeEmail}|\n {e.Message} |\n {e.StackTrace}");
                return false;
            }
        }

        public async Task<EndUser> Authenticate(String email, String password)
        {
            try
            {
                var user = await GetAsync(email);

                if (user == null)
                {
                    _logger_.LogError($"User with email {email} not found");
                    return null;
                }
                var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(password, user.Salt);
                if (!(await _userRepository_.TryAuthenticateAsync(email, encryptedPassword)))
                {
                    _logger_.LogError($"User with email {email} not authenticated");
                    return null;
                }
                // remove password before returning
                return user.ReturnWithoutSensitiveData();
            }
            catch (Exception e)
            {
                _logger_.LogError($"Unable to authenticate user with email {email}|\n {e.Message} |\n {e.StackTrace}");
                return null;
            }
        }
    }
}
