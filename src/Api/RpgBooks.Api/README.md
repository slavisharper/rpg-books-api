# RPG Books Main API

This is the main API that contains all of the application modules and services.
This can be counted as modular monolith app that can be easily split into microservices.

## Included modules
	- Identity
	- Books
	- Catalogue
	- Orders
	- Statistics
	- Read


## Required secrets

```json
{
  "ApplicationSecrets:AuthenticationSecret": "{Authentication JWT secret key}",
  "ApplicationSecrets:TokenProtectionSecret": "{Security tokens secret key}",
  "ApplicationSecrets:PasswordProtectionSecret": "{Password protection secret key}",
  "ApplicationSecrets:DefaultConnectionString": "{Database connection string}",
  "ApplicationSecrets:StripePublishableKey": "Stripe public key.",
  "ApplicationSecrets:StripeSecretKey": "Stripe secret key",
  "ApplicationSecrets:CloudFileStorageSecret": "{File download tokens secret key}",
  "ApplicationSecrets:CloudFileStorageConnectionString": "{Azure storage account connection string}",
  "ApplicationSecrets:SendGridSecretKey": "{Send grid secret API key}"
}
```