# RPG Books API

This is the API standing behind the RPG Books application.

This is a research project that uses the latest version of .NET 8.
I have tried to follow the best practises for web dot net development. 

Goals to achieve in this project
* Use .NET 8 and 8th version of microsoft libraries
* Clean architecture
* Domain driven design
* Command and query responsibility segregation
* Using performence oriented libraries
* Custom mediator implementation
* Custom domain events implementation
* Custom authentication and authortization implementation
* Create modulated monolith application
* Each module shoul be easily extracted to another API with few lines of  code for configuration
* Separate each domain context to be separate module
* Do not depend on single DB techology. Presistence layer should be able to use any of MSSQL, MySQL, PostgreSQL, SQLite
* Support multiple languages

# Modules

## Identity module

The Identity module is a crucial component of the RPG Books application, responsible for managing user authentication, authorization, and user profile information.
It provides a secure and seamless experience for users, ensuring their data is protected and accessible only to authorized individuals.

### API Endpoints

#### User registration
- **Endpoint**: `/api/identity`
- **Method**: POST
- **Access**: Public
- **Description**: Register a new user in the system.
- **Request Body**:
  ```json
  {
    "email": "example@example.com",
    "password": "example123"
  }
- **Returns**:  Returns a success message or appropriate error messages if registration fails.

#### User details
- **Endpoint**: `/api/identity`
- **Method**: GET
- **Authorization**: Bearer token required in the request headers.
- **Authorization type**: User
- **Description**: Retrieve the user's profile information.
- **Request Body**:
  ```json
  {
    "email": "example@example.com",
    "password": "example123"
  }
- **Returns**:  Returns the user's profile information if the token is valid or an appropriate error message if unauthorized or invalid token.


### Catalogue module

### Orders module

### Read module

### Authors module

### Statistics module



