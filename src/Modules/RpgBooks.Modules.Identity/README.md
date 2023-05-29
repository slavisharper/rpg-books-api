# Identity module

## API Endpoints

### User registration
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

### User details
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
