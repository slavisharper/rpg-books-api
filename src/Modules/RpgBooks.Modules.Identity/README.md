# Identity module

## Database Migrations

```
Add-Migration 'Identity{Name}' -OutputDir "Infrastructure/Persistence/Migrations" -context IdentityDbContext
```

## API Endpoints

### User registration
- **Endpoint**: `/api/account`
- **Method**: POST
- **Access**: Public
- **Description**: Register a new user in the system.
- **Request Body**:
  ```json
  {
    "email": "example@example.com",
    "password": "example123"
  }
  ```
- **Returns**:  Returns a success message or appropriate error messages if registration fails.

### User details
- **Endpoint**: `/api/account`
- **Method**: GET
- **Authorization**: Bearer token required in the request headers.
- **Authorization type**: User
- **Description**: Retrieve the user's profile information.
- **Returns**:  Returns the user's profile information if the token is valid or an appropriate error message if unauthorized or invalid token.

### Login user
- **Endpoint**: `/api/account/login`
- **Method**: POST
- **Access**: Public
- **Description**: Logs in into the uses account.
- **Request Body**:
  ```json
  {
    "email": "example@example.com",
    "password": "example123"
  }
  ```
- **Returns**:  Returns the user's authentication and refresh tokens.

### Refresh user's authentication token
- **Endpoint**: `/api/account/refresh-token`
- **Method**: POST
- **Access**: Public
- **Description**: Logs in into the uses account.
- **Request Body**:
  ```json
  {
    "authenticationToken": "string",
    "refreshToken": "string"
  }
  ```
- **Returns**:  Returns the user's authentication and refresh tokens.