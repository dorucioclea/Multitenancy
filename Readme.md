##What this is

This is a multitenancy experiment.
It uses autofac for dependency injection and entity code first for migrations.

##What this is not

It's more of a demo project.  Not yet ready for production (i.e. use at own expense). 

##Usage 
Create a database called Multitenancy
Run the migrations
For this you need to run the command : 
Update-Database -Script -ConfigurationTypeName ContextConfiguration -Verbose -ConnectionString "Data Source=.;Initial Catalog=Multitenancy;Integrated Security=True;" -ConnectionProviderName "System.Data.SqlClient"
This assumes that you already have a database called Multitenancy on your local sql server instance

For references, if you want to modify the note class to further experiment, you can delete the initial migration and add another one (or simply add another migration) with this command: 

Add-Migration -Verbose -ConfigurationTypeName ContextConfiguration -ConnectionString "Data Source=.;Initial Catalog=Multitenancy;Integrated Security=True;" -ConnectionProviderName "System.Data.SqlClient" InitialMigration

Start the project