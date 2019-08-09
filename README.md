# Intandem-Registration-Project

Contains all code related to the new registration section for the InTandem website

## Working with this project

### Prerequisites

* .NET Core SDK 2.2
* Visual Studio 2019 (version 16)

### To avoid any database-realted errors

Run the following command in the NuGet Package Manager (PMC) in Visual Studio 2019 (found in Tools > NuGet Package Manager > Package Manger Console)

    PM> Update-Database 

This command will create the database that this web application uses

If you encounter any database-realted errors, you may drop the database by using the folliwing command in the PMC to see if it fixes the error

    PM> Drop-Database

In order to be able to deploy this web application the **Expressive Attributes** package must be installed
Run the following command to install it in the Package Manager Console\
    
    PM> Install-Package ExpressiveAnnotations


###Setup account confirmation

In order to be able to send account confirmation emails via SendGrid, run the following commands if you do not want to use your own SendGrid account

    dotnet user-secrets set SendGridUser faizan.jamil
    dotnet user-secrets set SendGridKey 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

If you would like to use your own SendGrid account, replace the API key and your username in the commands above

These two commands create secrets used by Visual Studio, they are stored in secrets.json which is located in `%APPDATA%/Microsoft/UserSecrets/<WebAppName-userSecretsId>`
