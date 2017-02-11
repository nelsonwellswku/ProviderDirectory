Provider Directory
==================

A CRUD app inspired by 
the [NPI Data Dissemination](http://download.cms.gov/nppes/NPI_Files.html)
data set so that I can learn about [Angular 2](https://angular.io/)
and [Marten](http://jasperfx.github.io/marten/).

Development
-----------

**Requirements**
* Visual Studio 2015
* PostgreSQL 9.5+
* Node 6.9.5+
* NPM 3.10.10+

Create a new database in Postgres with the username _postgress_ and the password _password_.

In Visual Studio, build the solution to restore the Nuget packages and then run the Web project that hosts the API. 

In PowerShell, navigate to the /src/website directory and run _npm install_. Next, run _npm start_.
This will start the webserver, serve the index.html file, hook up browser sync, and open your
browser to the website. 

From there, create a new provider from the _create provider_ page.
That will cause Marten to create the table used to store provider documents.

Now your ready to write some code. I tend to use Visual Studio for the C# / server-side
and Visual Studio Code for the TypeScript / front-end code.