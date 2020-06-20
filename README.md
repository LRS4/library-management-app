# Library Management App

## Background
Library management system to track branch assets and patrons. Adapted implementation of the project demonstrated by [Wes Doyle](https://www.youtube.com/watch?v=WTVcLFTgDqs). 

Main focus of the application is to implement:

* MVC architecture pattern
* Database first and migration approaches
* Service layer / Repository pattern
* Identity user authentication
* Authorised routes for admin users
* Publish to Azure App Service
* Azure Data Factory ETL 
* Azure Analysis Services dimensional model
* Power BI Embedded Row-Level Security 

## Features

* Users can log in and check out items etc.
* Users can view their checkout history / fees etc.
* Admin can add or remove library assets (books/movies)
* Admin can view a Power BI Embedded dashboard
* Admin can only view their own branch metrics

## Project setup
* Publish ILS database project to (localdb)MSSQLLocalDB
* Update-Database with migrations for Identity tables
* Run stored procedure PopulateReferenceTables
* Run application in Visual Studio on localhost

## Entity Framework Commands

* `Add-Migration [name]`
* `Remove-Migration`
* `Update-Database`

## References

* [ASP.NET Core Tutorials](https://www.youtube.com/watch?v=4IgC2Q5-yDE&list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
* [Power BI Embedded Playground](https://microsoft.github.io/PowerBI-JavaScript/demo/v2-demo/index.html)
* [Azure App Service](https://azure.microsoft.com/en-gb/services/app-service/#getting-started)
* [Azure Analysis Services](https://docs.microsoft.com/en-us/azure/analysis-services/analysis-services-overview)