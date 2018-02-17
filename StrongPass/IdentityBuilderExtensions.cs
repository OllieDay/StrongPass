using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace StrongPass
{
	public static class IdentityBuilderExtensions
	{
		public static IdentityBuilder AddStrongPass<TUser>(this IdentityBuilder @this) where TUser : class
		{
			return @this.AddStrongPass<TUser>(options =>
			{
				options.Code = "StrongPass";
				options.Description = "Password is not strong enough.";
				options.FromCollection(Passwords.TopThousand);
			});
		}

		public static IdentityBuilder AddStrongPass<TUser>(this IdentityBuilder @this, Action<StrongPassOptions> optionsAction) where TUser : class
		{
			var options = new StrongPassOptions();

			optionsAction(options);

			@this.Services.AddSingleton<StrongPassOptions>(options);
			@this.AddPasswordValidator<PasswordValidator<TUser>>();

			return @this;
		}
	}
}
