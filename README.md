Provider Directory
==================

A CRUD app inspired by 
the [NPI Data Dissemination](http://download.cms.gov/nppes/NPI_Files.html)
data set so that I can learn about [Angular 2](https://angular.io/)
and [Marten](http://jasperfx.github.io/marten/).

Development
-----------

**Requirements**
* Visual Studio 2017
* PostgreSQL 9.5+
* Node 8.1+
* NPM 5.0+

Create two new databases in Postgres with the username _postgress_ and the password _password_. They should be named ProviderDirectory and ProviderDirectory_test.

In Visual Studio, build the solution to restore the Nuget packages. Run all of the integration tests in Tests/Integration. 
If they've passed, then you've set up the test database correctly. Now, go ahead and start the WebAPI project to host the web service.

In PowerShell, navigate to the /src/website directory and run _npm install_. Next, run _ng serve_.
This will start the webserver. In your browser, navigate to locahost:4200. The application 
uses [Angular CLI](https://github.com/angular/angular-cli) if you want more information about the command line interface

From there, create a new provider from the _create provider_ page.
That will cause Marten to create the table used to store provider documents.

Now your ready to write some code. I tend to use Visual Studio for the C# / server-side
and Visual Studio Code for the TypeScript / front-end code.

Importing Data
-------------
For now, this app targets the NPI data from March 2017. You'll need CSV files that will ultimately imported into the app.

* [NPI Data Set, March 2017](http://download.cms.gov/nppes/NPPES_Data_Dissemination_March_2017.zip)
* [Taxonomy Codes](http://www.nucc.org/images/stories/CSV/nucc_taxonomy_170.csv)

Unzip the NPI data set file. Open the app.config in the NpiDataProcessor project. There are 2 app settings that need to be changed to point to the NPI data set .csv
and the taxonomy codes .csv, NpiFilePath and TaxonomyFilePath, respectively. Optionally, you can set the MaxRecordsToImport to something higher than the default 500.
500 is the default for local testing because loading all ~800,000 providers isn't instantaneous. 

Once you've configured app.config appropriately, just run the NpiDataProcessor command line app. It will insert the provider
records. At this point, you can load up the app and begin to play with it.