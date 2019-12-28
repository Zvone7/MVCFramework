using BCrypt.Net;
using Microsoft.Extensions.Logging;
using MvcFrameworkCml;
using MvcFrameworkCml.Infrastructure;
using MvcFrameworkCml.Infrastructure.Managers;
using MvcFrameworkCml.Infrastructure.Repository;
using MvcFrameworkCml.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFrameworkBll.Managers
{
    public class EndUserManager : CustomBaseLogicManager, IEndUserManager
    {
        private readonly IEndUserRepository _userRepository_;

        public EndUserManager(
          IEndUserRepository userRepository,
          IAppSettings appSettings,
          ILogger logger)
          : base(appSettings, logger)
        {
            _userRepository_ = userRepository;
        }

        public async Task<Content<EndUser>> GetEntityAsync(int id)
        {
            var content = await GetUserWithSensitiveDataAsync(id);
            if (!content.HasError)
                content.Data.ReturnWithoutSensitiveData();
            return content;
        }

        private async Task<Content<EndUser>> GetUserWithSensitiveDataAsync(int id)
        {
            var resultContent = new Content<EndUser>();
            try
            {
                if (id <= 0)
                {
                    var description = $"Unable to Get user with id {id}";
                    resultContent.AppendError(new ArgumentException(description), null);
                    _logger_.LogError(description);
                }
                else
                {
                    var user = await _userRepository_.GetEntityAsync(id);
                    if (user == null)
                        resultContent.AppendError(new KeyNotFoundException(), $"Id {id} not found.");
                    else
                        resultContent.SetData(user);
                }
            }
            catch (Exception e)
            {
                var message = $"Unable to Get user with id {id}";
                resultContent.AppendError(e, message);
                _logger_.LogError(e, message);
            }
            return resultContent;

        }

        public async Task<Content<EndUser>> GetUserAsync(
          string email,
          bool isHashed = false,
          bool requestOnlyActiveUsers = true)
        {
            var content = await GetUserAsync(email, isHashed, requestOnlyActiveUsers);
            if (!content.HasError)
                content.Data.ReturnWithoutSensitiveData();
            return content;
        }

        private async Task<Content<EndUser>> GetUserWithSensitiveDataAsync(
          string email,
          bool isHashed = false,
          bool requestOnlyActiveUsers = true)
        {
            var resultContent = new Content<EndUser>();
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    var message = $"Unable to Get user - email null/empty.";
                    resultContent.AppendError(new ArgumentNullException(message));
                    _logger_.LogError(message);
                }
                EndUser endUser = isHashed
                    ? await _userRepository_.GetUserWithSensitiveDataAsync(email, requestOnlyActiveUsers)
                    : await _userRepository_.GetUserWithSensitiveDataAsync(BCrypt.Net.BCrypt.HashPassword(email, _appSettings_.Secret));
                if (endUser == null)
                    resultContent.AppendError(new KeyNotFoundException(), $"User with email {email} not found");
                else
                    resultContent.SetData(endUser);
            }
            catch (Exception ex)
            {
                var message = "Unable to Get user with username " + email;
                resultContent.AppendError(ex, message);
                _logger_.LogError(ex, message);
            }
            return resultContent;
        }

        public async Task<IEnumerable<EndUser>> GetAllEntitiesAsync()
        {
            try
            {
                var users = await _userRepository_.GetAllEntitiesAsync();
                return users.Select(u => u.ReturnWithoutSensitiveData());
            }
            catch (Exception e)
            {
                _logger_.LogError(e, $"Unable to GetAll users.");
                return null;
            }
        }

        public async Task<bool> AddEntityAsync(EndUser user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.Password) ||
                    string.IsNullOrEmpty(user.Email) ||
                    string.IsNullOrEmpty(user.LastName) ||
                    string.IsNullOrEmpty(user.Name))
                {
                    _logger_.LogError($"Unable to add user - some properties are null/empty.");
                    return false;
                }
                var existingUser = await GetUserAsync(user.Email, false, false);
                if (existingUser != null)
                {
                    _logger_.LogError("Unable to add user - user with same email already exists.");
                    return false;
                }
                var salt = BCrypt.Net.BCrypt.GenerateSalt(SaltRevision.Revision2B);
                var passwordHashed = BCrypt.Net.BCrypt.HashPassword(user.Password, salt);
                var emailHashed = BCrypt.Net.BCrypt.HashPassword(user.Email, _appSettings_.Secret);
                user.Salt = salt;
                user.Password = passwordHashed;
                user.Email = emailHashed;
                user.DateJoined = DateTime.UtcNow.Date;
                user.Role = !string.IsNullOrWhiteSpace(user.Role) ? user.Role : Role.USER;
                user.IsActive = true;
                user.EmailConfirmed = true;
                await _userRepository_.AddEntityAsync(user);
                return true;
            }
            catch (Exception e)
            {
                _logger_.LogError(e, "Unable to Add user.");
                return false;
            }
        }

        ///// <summary>
        ///// Not used
        ///// </summary>
        //public async Task<Boolean> UpdateAsync(EndUser user)
        //{
        //    try
        //    {
        //        if (String.IsNullOrEmpty(user.Password) ||
        //            String.IsNullOrEmpty(user.Email) ||
        //            String.IsNullOrEmpty(user.LastName) ||
        //            String.IsNullOrEmpty(user.Name))
        //        {
        //            _logger_.LogError($"Unable to update user - some properties are null/empty.");
        //            return false;
        //        }

        //        var possibleExistingUserWithSameEmail = GetAsync(user.Email, isHashed: false, requestOnlyActiveUsers: false);
        //        if (possibleExistingUserWithSameEmail != null && possibleExistingUserWithSameEmail.Id != user.Id)
        //        {
        //            _logger_.LogError($"Unable to update user - user with same email already exists.");
        //            return false;
        //        }

        //        var userInDb = await GetAsync(user.Id);
        //        if (userInDb == null)
        //        {
        //            _logger_.LogError($"Unable to update user - active user not found.");
        //            return false;
        //        }


        //        if (!String.Equals(userInDb.Name, user.Name, StringComparison.Ordinal))
        //            userInDb.Name = user.Name;

        //        if (!String.Equals(userInDb.LastName, user.LastName, StringComparison.Ordinal))
        //            userInDb.LastName = user.LastName;

        //        var passwordHashed = BCrypt.Net.BCrypt.HashPassword(user.Password, userInDb.Salt);
        //        if (!String.Equals(userInDb.Password, passwordHashed, StringComparison.Ordinal))
        //            userInDb.Password = passwordHashed;

        //        var emailHashed = BCrypt.Net.BCrypt.HashPassword(user.Email, _appSettings_.Secret);
        //        if (!String.Equals(userInDb.Email, emailHashed, StringComparison.Ordinal))
        //        {
        //            userInDb.Email = emailHashed;
        //            //todo - send confirmation mail when updating email
        //        }

        //        await _userRepository_.UpdateAsync(userInDb);
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        _logger_.LogError(e, $"Unable to Update user.");
        //        return false;
        //    }
        //}

        public async Task<bool> DeleteEntityAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger_.LogError($"Unable to Delete user with id {id}");
                    return false;
                }
                return await _userRepository_.DeleteEntityAsync(id);
            }
            catch (Exception e)
            {
                _logger_.LogError(e, $"Unable to Delete user with id {id}");
                return false;
            }
        }

        //public async Task<Boolean> ChangeName(Int32 id, String name)
        //{
        //    try
        //    {
        //        if (id <= 0)
        //        {
        //            _logger_.LogError($"Unable to {nameof(ChangeName)} of user - id <= 0");
        //            return false;
        //        }
        //        var user = await GetAsync(id);
        //        if (String.Equals(name, user.Name, StringComparison.InvariantCulture))
        //        {
        //            _logger_.LogError($"Unable to {nameof(ChangeName)} of user - new value same as the old one");
        //            return false;
        //        }
        //        user.Name = name;
        //        await _userRepository_.UpdateAsync(user);
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        _logger_.LogError(e, $"Unable to {nameof(ChangeName)} of user with id {id}");
        //        return false;
        //    }
        //}

        //public async Task<Boolean> ChangeLastName(Int32 id, String lastName)
        //{
        //    try
        //    {
        //        if (id <= 0)
        //        {
        //            _logger_.LogError($"Unable to {nameof(ChangeLastName)} of user - id <= 0");
        //            return false;
        //        }
        //        var user = await GetAsync(id);

        //        if (String.Equals(lastName, user.LastName, StringComparison.InvariantCulture))
        //        {
        //            _logger_.LogError($"Unable to {nameof(ChangeLastName)} of user - new value same as the old one");
        //            return false;
        //        }
        //        user.LastName = lastName;
        //        await _userRepository_.UpdateAsync(user);
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        _logger_.LogError(e, $"Unable to {nameof(ChangeLastName)} of user with id {id}|");
        //        return false;
        //    }
        //}

        //public async Task<Boolean> ChangeEmail(Int32 id, String email)
        //{
        //    try
        //    {
        //        if (id <= 0)
        //        {
        //            _logger_.LogError($"Unable to {nameof(ChangeEmail)} of user - id <= 0");
        //            return false;
        //        }
        //        var user = await GetAsync(id);
        //        var emailHashed = BCrypt.Net.BCrypt.HashPassword(email, _appSettings_.Secret);
        //        if (String.Equals(emailHashed, user.Email, StringComparison.InvariantCulture))
        //        {
        //            _logger_.LogError($"Unable to {nameof(ChangeEmail)} of user - new value same as the old one");
        //            return false;
        //        }
        //        user.Email = emailHashed;
        //        await _userRepository_.UpdateAsync(user);
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        _logger_.LogError(e, $"Unable to {nameof(ChangeEmail)} of user with id {id}|");
        //        return false;
        //    }
        //}

        //public async Task<Boolean> ChangePassword(Int32 id, String password)
        //{
        //    try
        //    {
        //        if (id <= 0)
        //        {
        //            _logger_.LogError($"Unable to {nameof(ChangePassword)} of user - id <= 0");
        //            return false;
        //        }
        //        var user = await GetAsync(id);
        //        var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(password, user.Salt);
        //        if (String.Equals(encryptedPassword, user.Password, StringComparison.InvariantCulture))
        //        {
        //            _logger_.LogError($"Unable to {nameof(ChangePassword)} of user - new value same as the old one");
        //            return false;
        //        }
        //        user.Password = encryptedPassword;
        //        await _userRepository_.UpdateAsync(user);
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        _logger_.LogError(e, $"Unable to {nameof(ChangePassword)} of user with id {id}|");
        //        return false;
        //    }
        //}

        public async Task<Content<EndUser>> AuthenticateAsync(
          string email,
          string password)
        {
            var resultContent = new Content<EndUser>();
            try
            {
                var emailHashed = BCrypt.Net.BCrypt.HashPassword(email, _appSettings_.Secret);
                var dbContent = await GetUserWithSensitiveDataAsync(emailHashed, true);
                if (dbContent.HasError)
                {
                    resultContent.AppendError(dbContent);
                    _logger_.LogError(resultContent.Errors.Last()?.Exception, resultContent.Errors.Last()?.Description);
                }
                else
                {
                    var passwordHashed = BCrypt.Net.BCrypt.HashPassword(password, dbContent.Data.Salt);
                    var isAuthenticated = await _userRepository_.TryAuthenticateAsync(emailHashed, passwordHashed);
                    if (!isAuthenticated)
                    {
                        var message = $"User with email {email} not authenticated";
                        resultContent.AppendError(new KeyNotFoundException(), message);
                        _logger_.LogError(message);
                    }
                    else
                        resultContent.SetData(dbContent.Data.ReturnWithoutSensitiveData());
                }
            }
            catch (Exception e)
            {
                var message = $"Unable to authenticate user with email {email}";
                resultContent.AppendError(e, message);
                _logger_.LogError(e, message);
            }
            return resultContent;
        }
    }
}
