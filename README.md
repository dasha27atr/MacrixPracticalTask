This project is a REST API client-server application developed as a part of a practical task for the Macrix company.

The REST API implementation consists of two projects: MacrixPracticalTask (server side) and MacrixPracticalTask_Client (client side).

MacrixPracticalTask is a ASP.NET Web Api application developed as a server side of the REST API.
It consists of 4 inner projects: MacrixPracticalTask (controllers and configuration), MacrixPracticalTask.Data (migrations, context and repository classes), MacrixPracticalTask.Models (models and DTO classes) and MacrixPracticalTask.Tests (test cases that test business logic of the application).

REST API uses EntityFramework to integrate with the SQLite database and to store necessary data, NLog library to log all the request and responces from the server side (the logs also are written to the database).
The application uses code-first approach - with the help of EntityFramework application models are transformed to database tables.

Installation instructions:
1) Find MacrixPracticalTask.zip file in the Releases folder of the current repository (https://github.com/dasha27atr/MacrixPracticalTask/releases/tag/1.0.0) and download it to your local machine.
2) Unzip the server archive and open MacrixPracticalTask.exe file.
3) Find MacrixPracticalTask_Client.zip file in the Releases folder of the Client repository (https://github.com/dasha27atr/MacrixPracticalTask_Client/releases/tag/1.0.0) and download it to your local machine.
4) Unzip the client archive and open MacrixPracticalTask_Client.exe file.

Hope you will find it useful :)
