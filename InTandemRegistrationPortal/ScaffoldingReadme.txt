Support for ASP.NET Core Identity was added to your project
- The code for adding Identity to your project was generated under Areas/Identity.

Configuration of the Identity related services can be found in the Areas/Identity/IdentityHostingStartup.cs file.

If your app was previously configured to use Identity, then you should remove the call to the AddIdentity method from your ConfigureServices method.

The generated UI requires support for static files. To add static files to your app:
1. Call app.UseStaticFiles() from your Configure method

To use ASP.NET Core Identity you also need to enable authentication. To authentication to your app:
1. Call app.UseAuthentication() from your Configure method (after static files)

The generated UI requires MVC. To add MVC to your app:
1. Call services.AddMvc() from your ConfigureServices method
2. Call app.UseMvc() from your Configure method (after authentication)

Change *all* instances of IdentityUser (except for in custom user class and db-related files)
in:
_LoginPartial.cshtml
Startup.cs
	services.AddDefaultIdentity -> services.AddIdentity<WebApp1User, IdentityRole>()
to WebApp1User (or different name depending on your project)

Make sure ApplicationDbContext in ApplicationDbContext.cs inherits from IdentityDbContext<WebApp1User>

Comment out services.AddDefaultIdentity in IdentityHostingStartup.cs
Found in ~/Areas/Identity


Add the AddDefaultTokenProviders method at the end of services.AddIdentity


The generated database code requires Entity Framework Core Migrations. Run the following commands:
1. dotnet ef migrations add CreateIdentitySchema
2. dotnet ef database update
 Or from the Visual Studio Package Manager Console:
1. Add-Migration CreateIdentitySchema
2. Update-Database


Apps that use ASP.NET Core Identity should also use HTTPS. To enable HTTPS see https://go.microsoft.com/fwlink/?linkid=848054.

After adding custom user data apply migrations to Db created by ApplicationDbContext
