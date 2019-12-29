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
using System.Text.RegularExpressions;
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

        public async Task<Content<EndUser>> GetEntityAsync(
          string email,
          bool isHashed = false,
          bool requestOnlyActiveUsers = true)
        {
            var content = await GetEntityAsync(email, isHashed, requestOnlyActiveUsers);
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

        public async Task<Content<IEnumerable<EndUser>>> GetAllEntitiesAsync()
        {
            var resultContent = new Content<IEnumerable<EndUser>>();
            try
            {
                var users = await _userRepository_.GetAllEntitiesAsync();
                resultContent.SetData(users.Select(u => u.ReturnWithoutSensitiveData()));
            }
            catch (Exception e)
            {
                string message = $"Unable to GetAll users.";
                resultContent.AppendError(e, message);
                _logger_.LogError(e, message);
            }
            return resultContent;
        }

        public async Task<Content<Boolean>> AddEntityAsync(EndUser user)
        {
            var resultContent = new Content<Boolean>();
            try
            {
                if (string.IsNullOrEmpty(user.Password) ||
                    string.IsNullOrEmpty(user.Email) ||
                    string.IsNullOrEmpty(user.LastName) ||
                    string.IsNullOrEmpty(user.Name))
                {
                    string message = $"Unable to add user - some properties are null/empty.";
                    resultContent.AppendError(new ArgumentNullException(), message);
                    _logger_.LogError(message);
                }
                else if (!PasswordFollowsComplexityRules(resultContent, user.Password))
                {
                    _logger_.LogError($"Password not complex enough.");
                }
                else
                {
                    var salt = BCrypt.Net.BCrypt.GenerateSalt(SaltRevision.Revision2B);
                    var passwordHashed = BCrypt.Net.BCrypt.HashPassword(user.Password, salt);
                    var emailHashed = BCrypt.Net.BCrypt.HashPassword(user.Email, _appSettings_.Secret);
                    user.Salt = salt;
                    user.Password = passwordHashed;
                    user.Email = emailHashed;
                    user.DateJoined = DateTime.UtcNow.Date;
                    user.Role = !String.IsNullOrWhiteSpace(user.Role) ? user.Role : Role.USER;
                    user.IsActive = true;
                    //todo email confirm
                    user.EmailConfirmed = true;
                    var result = await _userRepository_.AddEntityAsync(user);
                    resultContent.SetData(result);
                }
            }
            catch (Exception e)
            {
                var message = "Unable to Add user.";
                resultContent.AppendError(e, message);
                _logger_.LogError(e, message);
            }
            return resultContent;
        }

        public async Task<Content<Boolean>> UpdateEntityAsync(EndUser user)
        {
            var resultContent = new Content<Boolean>();
            try
            {
                if (String.IsNullOrEmpty(user.LastName) || String.IsNullOrEmpty(user.Name))
                {
                    string message = $"Unable to update user - some properties are null/empty.";
                    resultContent.AppendError(new ArgumentNullException(), message);
                    _logger_.LogError(message);
                }
                else
                {
                    var userContent = await GetEntityAsync(user.Id);
                    if (userContent.HasError)
                    {
                        string message = $"Unable to update user - active user not found.";
                        resultContent.AppendError(new KeyNotFoundException(), message);
                        _logger_.LogError(message);
                    }
                    else
                    {
                        if (String.Equals(userContent.Data.Name, user.Name, StringComparison.Ordinal) &&
                            String.Equals(userContent.Data.LastName, user.LastName, StringComparison.Ordinal))
                        {
                            string description = $"Tried updating user with same values";
                            resultContent.AppendError(new ArgumentException(), description);
                            _logger_.LogError(description);
                        }
                        else
                        {
                            var result = await _userRepository_.UpdateEntityAsync(user);
                            resultContent.SetData(result);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string message = $"Unable to Update user.";
                resultContent.AppendError(e, message);
                _logger_.LogError(message);
            }
            return resultContent;
        }

        public async Task<Content<Boolean>> DeleteEntityAsync(int id)
        {
            var resultContent = new Content<Boolean>();
            try
            {
                if (id <= 0)
                {
                    var message = $"Unable to Delete user with id {id}";
                    resultContent.AppendError(new ArgumentOutOfRangeException(), message);
                    _logger_.LogError(message);
                }
                else
                {
                    var result = await _userRepository_.DeleteEntityAsync(id);
                    resultContent.SetData(result);
                }
            }
            catch (Exception e)
            {
                var message = $"Unable to Delete user with id {id}";
                resultContent.AppendError(e, message);
                _logger_.LogError(e, message);
            }
            return resultContent;
        }

        public async Task<Content<Boolean>> UpdateUserEmailAsync(Int32 id, String newEmail)
        {
            var resultContent = new Content<Boolean>();
            try
            {
                if (id <= 0)
                {
                    var message = $"Unable to Update user email for user {id} - id must be > 0";
                    resultContent.AppendError(new ArgumentOutOfRangeException(), message);
                    _logger_.LogError(message);
                }
                else if (String.IsNullOrEmpty(newEmail))
                {
                    var message = $"Unable to Update user email for user {id} - email cant be empty.";
                    resultContent.AppendError(new ArgumentException(), message);
                    _logger_.LogError(message);
                }
                else
                {
                    var userContent = await GetUserWithSensitiveDataAsync(id);
                    if (userContent.HasError)
                    {
                        resultContent.AppendError(userContent.Errors);
                    }
                    else
                    {
                        var newEmailHashed = BCrypt.Net.BCrypt.HashPassword(newEmail, _appSettings_.Secret);
                        if (String.Equals(newEmailHashed, userContent.Data.Email, StringComparison.InvariantCulture))
                        {
                            var message = $"Unable to {nameof(UpdateUserEmailAsync)} of user - new value same as the old one";
                            resultContent.AppendError(new ArgumentException(), message);
                            _logger_.LogError(message);
                        }
                        else
                        {
                            await _userRepository_.UpdateUserEmailAsync(id, newEmailHashed);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string message = $"Unable to {nameof(UpdateUserEmailAsync)} of user with id {id}";
                resultContent.AppendError(e, message);
                _logger_.LogError(e, message);
            }
            return resultContent;
        }

        public async Task<Content<Boolean>> UpdateUserPasswordAsync(Int32 id, String newPassword)
        {
            var resultContent = new Content<Boolean>();
            try
            {
                if (id <= 0)
                {
                    var message = $"Unable to Update user password for user {id}";
                    resultContent.AppendError(new ArgumentOutOfRangeException(), message);
                    _logger_.LogError(message);
                }
                else if (String.IsNullOrEmpty(newPassword))
                {
                    var message = $"Unable to Update user password for user {id} - password cant be empty.";
                    resultContent.AppendError(new ArgumentException(), message);
                    _logger_.LogError(message);
                }
                else if (!PasswordFollowsComplexityRules(resultContent, newPassword))
                {
                    _logger_.LogError($"Password not complex enough.");
                }
                else
                {
                    var userContent = await GetUserWithSensitiveDataAsync(id);
                    if (userContent.HasError)
                    {
                        resultContent.AppendError(userContent.Errors);
                    }
                    else
                    {
                        var newPasswordHashed = BCrypt.Net.BCrypt.HashPassword(newPassword, userContent.Data.Salt);
                        if (String.Equals(newPasswordHashed, userContent.Data.Password, StringComparison.InvariantCulture))
                        {
                            var message = $"Unable to {nameof(UpdateUserPasswordAsync)} of user - new value same as the old one";
                            resultContent.AppendError(new ArgumentException(), message);
                            _logger_.LogError(message);
                        }
                        else
                        {
                            await _userRepository_.UpdateUserPasswordAsync(id, newPasswordHashed);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string message = $"Unable to {nameof(UpdateUserPasswordAsync)} of user with id {id}";
                resultContent.AppendError(e, message);
                _logger_.LogError(e, message);
            }
            return resultContent;
        }

        public async Task<Content<EndUser>> AuthenticateUserAsync(
          String email,
          String password)
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

        private Boolean PasswordFollowsComplexityRules<T>(Content<T> content, String password)
        {
            if (password.Length < _appSettings_.PasswordComplexitySettings.MinimumLength)
            {
                var message = $"Password must contain at least {_appSettings_.PasswordComplexitySettings.MinimumLength} characters long.";
                content.AppendError(new ArgumentException(), message);
                _logger_.LogError(message);
                return false;
            }
            foreach (var rule in _appSettings_.PasswordComplexitySettings.Rules)
            {
                var regex = new Regex(rule.Regex);
                var match = regex.Match(password);

                if (!match.Success)
                {
                    content.AppendError(new ArgumentException(), $"{rule.Name}");
                    _logger_.LogError($"{rule.Name}");
                    return false;
                }
            }
            return true;
        }
    }
}
