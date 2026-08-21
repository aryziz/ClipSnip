# ClipSnip

In this app you can upload a picture of yourself and get a recommended hairstyle or see what you would look like with other hairstyles, log your barber visits and share hairstyles with your friends.


# For Developers

## Run the project

From the repository root, restore the project dependencies:

dotnet restore

Run the application:

```bash
dotnet run --project ClipSnip/
```

Or for hot reloading:

```bash
dotnet watch run --project ClipSnip/
```

Open the URL in your browser.

## Run tests

```bash
dotnet test
```

## Deployment

ClipSnip is being prepared for containerized deployment to Azure using Docker, Azure Container Registry, Azure App Service, and Azure SQL Database.

See [DEPLOYMENT.md](DEPLOYMENT.md) for local container setup and the deployment roadmap.