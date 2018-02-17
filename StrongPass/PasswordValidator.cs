using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace StrongPass
{
	internal sealed class PasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : class
	{
		private readonly StrongPassOptions _options;
		private readonly ISet<string> _passwords;

		public PasswordValidator(StrongPassOptions options)
		{
			_options = options;
			_passwords = options.BuildPasswords();
		}

		public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
		{
			IdentityResult result;

			if (_passwords.Contains(password))
			{
				result = IdentityResult.Failed(new IdentityError
				{
					Code = _options.Code,
					Description = _options.Description
				});
			}
			else
			{
				result = IdentityResult.Success;
			}

			return Task.FromResult(result);
		}
	}
}
