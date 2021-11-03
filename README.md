# Food Delivery REST API

## About the application
Application represents simulation of logged in user ordering food from a restaurant chain.
After signing up, logged in user can see food and it's details and order it from a nearest restaurant. 
Besides same actions as user, admin can add or delete food and add or delete restaurant from a chain. 
Inmemory database is used and all relevant data is added manually each time application starts. 
Since only admin can create and delete food and restaurants, creating admin is first request.  
Ordering is allowed for all logged in users.

## Important Notes
 - It is stated that delivery takes 15 minutes and for real-life simulation is needed uncommenting line which locks object in AddOrder in OrderService.cs 

## Tools for testing
 - Swagger, which is available on https://localhost:44383/swagger and starts when application is started, can be used only for endpoints that are open to everyone:
	- POST User/signup, POST User/login, 
	- GET Restaurant, GET Restaurant/{id}
	- GET Food, GET Food/{id}, GET Food/sortedbyrating
 - Postman can be used for all endpoints and example of requests can be seen in added collection

## Steps to reproduce to order food with Postman
 - Sign up as admin using POST /User/signup endpoint 
 - Log in as admin using POST /User/login endpoint and copy given jwt token
 - Create restaurant(s) using POST /Restaurant and adding admin's jwt token in Authorization tab as a value for Bearer token
 - Create food(s) using POST /Food and adding admin's jwt token in Authorization tab as a value for Bearer token
 - Create order using POST /Order endpoint and jwt token of logged in user
 
## Tech decisions
- [Web API written in .Net Core 5.0](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-5.0)
- [AutoMapper](https://github.com/AutoMapper/AutoMapper)
- [JWT token](https://jwt.io/)
- [NUnit](https://nunit.org/)

## Project structure
- FoodDelivery.Authorization - contains JwtUtils for handling JWT authorization and authentication
- FoodDelivery.AutoMapper - contains Profiles for mapping Models to corresponding DTOs and vise versa
- FoodDelivery.Controllers - contains Controllers
- FoodDelivery.DBAccess - contains DBContext
- FoodDelivery.DTOs - contains objects needed for CRUD actions
- FoodDelivery.Helpers - contains helpers
- FoodDelivery.Middlewares - contains ExceptionHandler and Jwt middleware
- FoodDelivery.Models - contains core entites
- FoodDelivery.Repositories - contains actions executed against database
- FoodDelivery.Services - contains services for communication with repositories
- FoodDelivery.Shared - contains data which is of use to multiple layers
- FoodDeliveryTests - contains unit tests splitted into folders representing layers

## Points for improvements
 - Current solution sees geolocation as (longitude, latitude) and if altitude is of crucial value it can be added 
 - In the assignment is stated that a restaurant has only one couirer and can deliver only one order at time so property IsAvailableForOrdering is used to handle it's availability
 - Add integration tests for all endpoints that need jwt token, which means calling the login endpoint, retrieving a token, then calling something else and seeing what happens
