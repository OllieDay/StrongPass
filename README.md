# StrongPass
Middleware that validates passwords against a blacklist.

## Getting started
Install the NuGet package into your ASP.NET Core application.

### Package Manager
```
Install-Package StrongPass
```

### .NET CLI
```
dotnet add package StrongPass
```

### Usage
1. Register StrongPass in the _ConfigureServices_ method of _Startup.cs_.

```csharp
services.AddIdentity<ApplicationUser, IdentityRole>()
	.AddStrongPass<ApplicationUser>();
```

2. Optionally configure the _Code_ and _Description_ properties of the _IdentityError_ object and the passwords to blacklist.

```csharp
services.AddIdentity<ApplicationUser, IdentityRole>()
	.AddStrongPass<ApplicationUser>(options =>
	{
		options.Code = "StrongPass";
		options.Description = "Password is not strong enough.";

		// Hard-coded blacklist.
		options.FromCollection(new[]
		{
			"password",
			"12345678"
		});

		// Loaded from file; one password per line.
		options.FromFile("blacklist.txt");
	});
```

The default password blacklist uses the top 1000 from [SecLists](https://github.com/danielmiessler/SecLists).
