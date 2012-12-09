Notekeeper is a small visual note-board keeping application.
========================================


----------


Visual Studio 2012
------------------
One of the main goals was to explore the RC for VS2012. So the project is build fully inside the new IDE.

Knockout JS
-----------
The other main goals was trying to learn Knockout as a client side MVVM framework. Knockout is used extensively in representing the data stored on the server to the clients using visual notes and boards.

It also handles most of the user interactions on the client side, and is responsible for communicating to the server using AJAX.

EF Code First
---------------------
Lastly the entity framework Code First aproach was used to create and interact with a very simple SQL 2008 R2 express database. A specific database instance is used, which is specified in the web config file.


----------

*What do you need to run the application*
---------------------------------------

 1. Preferably Visual Studio 2012 RC or the Web Express edition
 2. Preferably SQL Server 2008 R2 Express edition.
 3. Change the Db connection string in the web config file to point to your SQL Server instance.
 4. Run CodeFirst migrations.
 5. Start the app.

