# Intandem-Registration-Project

Contains all code related to the new registration section for the InTandem website

## Working with this project

### Prerequisites

* .NET Core SDK 2.2
* Visual Studio 2019 (version 16)

### To avoid any database-related errors

Run the following command in the NuGet Package Manager (PMC) in Visual Studio 2019 (found in Tools > NuGet Package Manager > Package Manger Console)

    PM> Update-Database 

This command will create the database that this web application uses

If you encounter any database-related errors, you may drop the database by using the folliwing command in the PMC to see if it fixes the error

    PM> Drop-Database

In order to be able to deploy this web application the **Expressive Attributes** package must be installed.

Run the following command to install it in the Package Manager Console
    
    PM> Install-Package ExpressiveAnnotations


### Setup account confirmation

In order to be able to send account confirmation emails via SendGrid, run the following commands if you do not want to use your own SendGrid account

    dotnet user-secrets set SendGridUser faizan.jamil
    dotnet user-secrets set SendGridKey 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

If you would like to use your own SendGrid account, replace the API key and your username in the commands above

These two commands create secrets used by Visual Studio, they are stored in secrets.json which is located in `%APPDATA%/Microsoft/UserSecrets/<WebAppName-userSecretsId>`


### Setup Seed Data Account

In order to be able to sign in as the seeded users, you will need to set up a password using the Secrets Tool:

    1. In commmand prompt, go to the same directory as Program.cs in your solution.
	2. Enter: "dotnet user-secrets set SeedUserPW <pw>" - where <pw> is a password of your choice.  Note - this must be a combination of lower-case, upper-case, numbers and non-alpha characters, else the seeding may fail.
	3. To verify this password, in the IDE, right-click the Project and select "Manage User Secrets" - this will reveal a JSON with this password.  Note that this is stored on your hard drive and will never be checked into GitHib.
