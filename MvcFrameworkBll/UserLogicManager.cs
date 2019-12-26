using Microsoft.Extensions.Logging;
using MvcFrameworkCml;
using MvcFrameworkCml.Infrastructure;
using MvcFrameworkCml.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFrameworkBll
{
    public class UserLogicManager : CustomBaseLogicManager
    {
        private readonly IUserRepository _userRepository_;

        public UserLogicManager(
            IUserRepository userRepository,
            IAppSettings appSettings,
            ILogger logger
            ) : base(appSettings, logger)
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
                _logger_.LogError(e, $"Unable to Get user with id {id}");
                return null;
            }
        }

        private async Task<EndUser> GetAsync(String email, Boolean isHashed = false)
        {
            try
            {
                if (String.IsNullOrEmpty(email))
                {
                    _logger_.LogError($"Unable to Get user - email null/empty.");
                    return null;
                }
                EndUser user;
                if (isHashed)
                {
                    user = await _userRepository_.GetAsync(email);
                }
                else
                {
                    var hashedEmail = BCrypt.Net.BCrypt.HashPassword(email, _appSettings_.Secret);
                    user = await _userRepository_.GetAsync(hashedEmail);
                }
                return user;
            }
            catch (Exception e)
            {
                _logger_.LogError(e, $"Unable to Get user with username {email}");
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
                _logger_.LogError(e, $"Unable to GetAll users.");
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
                var passwordHashed = BCrypt.Net.BCrypt.HashPassword(user.Password, salt);
                var emailHashed = BCrypt.Net.BCrypt.HashPassword(user.Email, _appSettings_.Secret);
                user.Salt = salt;
                user.Password = passwordHashed;
                user.Email = emailHashed;
                user.DateJoined = DateTime.UtcNow.Date;
                user.Role = !String.IsNullOrWhiteSpace(user.Role) ? user.Role : Role.USER;
                user.IsActive = true;

                //todo - send confirmation mail
                await _userRepository_.AddAsync(user);
                return true;
            }
            catch (Exception e)
            {
                _logger_.LogError(e, $"Unable to Add user.");
                return false;
            }
        }

        public async Task<Boolean> ChangeName(Int32 id, String name)
        {
            try
            {
                if (id <= 0)
                {
                    _logger_.LogError($"Unable to {nameof(ChangeName)} of user - id <= 0");
                    return false;
                }
                var user = await GetAsync(id);
                if (String.Equals(name, user.Name, StringComparison.InvariantCulture))
                {
                    _logger_.LogError($"Unable to {nameof(ChangeName)} of user - new value same as the old one");
                    return false;
                }
                user.Name = name;
                await _userRepository_.UpdateAsync(user);
                return true;
            }
            catch (Exception e)
            {
                _logger_.LogError(e, $"Unable to {nameof(ChangeName)} of user with id {id}");
                return false;
            }
        }

        public async Task<Boolean> ChangeLastName(Int32 id, String lastName)
        {
            try
            {
                if (id <= 0)
                {
                    _logger_.LogError($"Unable to {nameof(ChangeLastName)} of user - id <= 0");
                    return false;
                }
                var user = await GetAsync(id);

                if (String.Equals(lastName, user.LastName, StringComparison.InvariantCulture))
                {
                    _logger_.LogError($"Unable to {nameof(ChangeLastName)} of user - new value same as the old one");
                    return false;
                }
                user.LastName = lastName;
                await _userRepository_.UpdateAsync(user);
                return true;
            }
            catch (Exception e)
            {
                _logger_.LogError(e, $"Unable to {nameof(ChangeLastName)} of user with id {id}|");
                return false;
            }
        }

        public async Task<Boolean> ChangeEmail(Int32 id, String email)
        {
            try
            {
                if (id <= 0)
                {
                    _logger_.LogError($"Unable to {nameof(ChangeEmail)} of user - id <= 0");
                    return false;
                }
                var user = await GetAsync(id);
                var emailHashed = BCrypt.Net.BCrypt.HashPassword(email, _appSettings_.Secret);
                if (String.Equals(emailHashed, user.Email, StringComparison.InvariantCulture))
                {
                    _logger_.LogError($"Unable to {nameof(ChangeEmail)} of user - new value same as the old one");
                    return false;
                }
                user.Email = emailHashed;
                await _userRepository_.UpdateAsync(user);
                return true;
            }
            catch (Exception e)
            {
                _logger_.LogError(e, $"Unable to {nameof(ChangeEmail)} of user with id {id}|");
                return false;
            }
        }

        public async Task<Boolean> ChangePassword(Int32 id, String password)
        {
            try
            {
                if (id <= 0)
                {
                    _logger_.LogError($"Unable to {nameof(ChangePassword)} of user - id <= 0");
                    return false;
                }
                var user = await GetAsync(id);
                var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(password, user.Salt);
                if (String.Equals(encryptedPassword, user.Password, StringComparison.InvariantCulture))
                {
                    _logger_.LogError($"Unable to {nameof(ChangePassword)} of user - new value same as the old one");
                    return false;
                }
                user.Password = encryptedPassword;
                await _userRepository_.UpdateAsync(user);
                return true;
            }
            catch (Exception e)
            {
                _logger_.LogError(e, $"Unable to {nameof(ChangePassword)} of user with id {id}|");
                return false;
            }
        }

        public async Task<EndUser> AuthenticateAsync(String email, String password)
        {
            try
            {
                var emailHashed = BCrypt.Net.BCrypt.HashPassword(email, _appSettings_.Secret);
                var user = await GetAsync(emailHashed, true);

                if (user == null)
                {
                    _logger_.LogError($"User with email {email} not found");
                    return null;
                }
                var passwordHashed = BCrypt.Net.BCrypt.HashPassword(password, user.Salt);
                if (!(await _userRepository_.TryAuthenticateAsync(emailHashed, passwordHashed)))
                {
                    _logger_.LogError($"User with email {email} not authenticated");
                    return null;
                }
                // remove sensitive data before returning
                return user.ReturnWithoutSensitiveData();
            }
            catch (Exception e)
            {
                _logger_.LogError(e, $"Unable to authenticate user with email {email}");
                return null;
            }
        }
    }
}
