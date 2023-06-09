# RPG Books API

This is the API standing behind the RPG Books application.

## What is RPG Books?

This is a new and fun application for reading and in the same time playing role playing games.
The idea is to make reading books interesting again. Here the reader will became the main character in the book
and will be able to make decisions that will affect the story.

## What is the purpose of this project?

This is a research project that I develop using the latest version of .NET 8.
I have tried to follow the best practices for a modern and secure dot net application. 

Goals to achieve in this project
* Use .NET 8 and 8th version of Microsoft libraries
* Clean architecture
* Domain driven design
* Command and query responsibility segregation
* Using performance oriented libraries
* Custom mediator implementation
* Custom domain events implementation
* Custom authentication and authorization implementation
* Create modulated monolith application
* Each module should be easily extracted to another API with few lines of  code for configuration
* Separate each domain context to be in a different stand-alone module
* Do not depend on single DB technology. Persistence layer should be able to use any of MSSQL, MySQL, PostgreSQL, SQLite
* Support multiple languages
* Take into account users security and privacy.

# Modules

## Identity module

The Identity module is a crucial component of the RPG Books application, responsible for managing user authentication, authorization, and user profile information.
It provides a secure and seamless experience for users, ensuring their data is protected and accessible only to authorized individuals.


### Catalogue module

The Catalogue module serves as a central repository for all available books in the application.
It provides users with a comprehensive listing of books, including details such as title, author, genre, and description.
Users can browse, search, and explore the vast collection of books offered. Each book can be purchased and added to the user's library.

### Orders module

The Orders module enables users to place orders for books they wish to purchase, see their order history, payments and download invoices.

### Read module

The Read module enables users to read the books they have purchased. It provides a seamless reading experience, allowing users to read books on any device, anywhere, anytime.

### Books module

This module serves as a central repository for all available books in the application. Here the authors create and release their books into the Catalogue.

### Statistics module
Here the authors can see the statistics for their books.
Also the readers can see statistics for their reading activity.



