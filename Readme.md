# ASP.NET Core WebApi Sample with HATEOAS, Versioning & Swagger

This project is an extension of an ASP.NET Web API initially developed to manage food items by our Professor, allowing users to GET, POST, PUT, PATCH, and DELETE food items. As part of an assignment for the Developing Interactive Web Applications course, additional authentication features were implemented, including user registration and login functionality secured by JWT (JSON Web Token) authentication.
  
  Key Features:
  
  Food Item Management:
    
  The Web API provides endpoints for managing food items, allowing clients to retrieve, create, update, partially update, and delete food item records.
  Authentication & User Management:
  
  A new controller was added to handle user registration and login functionalities.
  Password Hashing:
  User passwords are hashed before storage to ensure sensitive information is kept secure.
  
  JWT-Based Authentication:
    Upon successful login, the API generates a JWT token that is sent to the client. This token is stored on the client side and must be included in requests to access protected endpoints.
  
  Login Endpoint:
  
  Returns 401 Unauthorized if the username and password are invalid.
  Returns 404 Not Found if the username does not exist.
  Returns 200 OK along with the generated JWT token if the login is successful.
  Register Endpoint:
  
  Returns 201 Created when a new user is successfully registered.
  Returns 500 Internal Server Error if there is an issue creating the user.
  This project demonstrates my ability to integrate authentication mechanisms into an existing API, using ASP.NET Core, JWT, and best practices for security like password hashing and status code handling for various outcomes.

