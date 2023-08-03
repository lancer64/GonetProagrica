# GonetProagrica

ProAgricaChallenge
Install the following packages:
-Microsoft.EntityFrameworkCore
-Microsoft.EntityFrameworkCore.Design
-Microsoft.EntityFrameworkCore.SqlServer
-Microsoft.EntityFrameworkCore.Tools

Before running anything go to \ProagricaChallenge\ProagricaChallenge\DatabaseLayer\TvShowDbContext.cs and modify the connection string so that it points to your Database. This application stores its data in a SQL database.
Then in the Package Manager Console run this commands:

> Add-Migration InitialCreate
> Update-Database

In order to run the application, first open the Terminal and go to \ProagricaChallenge\ProagricaChallenge
Then run the following command:

> dotnet run

You can input the folloeing commands:
-list: gives you all the tv shows.
-favorites: gives you all the tv shows marked as favorite -[id]: and integer that respresent the id of a tvshow, it favorites or unfavorites said tv show
-quit: To stop running the app.

In order to run the test go to the root folder and run:

> dotnet test
