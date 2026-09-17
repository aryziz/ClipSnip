# ClipSnip

ClipSnip is an ASP.NET Core MVC application hosted in Microsoft Azure.

The application uses Azure App Service for hosting, Azure SQL Database for persistent storage, Azure Key Vault for secrets, and Azure Managed Identity for secure access to Azure resources.

---

## Architecture

The production environment is hosted in Azure and consists of an application layer, security layer, and data layer.

```mermaid
flowchart TB
    User["👤 User / Browser"]

    subgraph Azure["Azure Resource Group - ClipSnip-rg"]

        subgraph AppLayer["Application Layer"]
            App["🌐 Azure App Service<br/>clipsnip-webapp-klugg7uj4px3q"]
            Plan["⚙️ App Service Plan<br/>clipsnip-webapp-klugg7uj4px3q-plan"]
        end

        subgraph Security["Identity & Secrets"]
            MI["🔐 Managed Identity<br/>clipsnip-ado-mi"]
            KV["🔑 Azure Key Vault<br/>clipsnip-kv-msu2"]
        end

        subgraph DataLayer["Data Layer"]
            SQLServer["🗄️ Azure SQL Server<br/>clipsnip-webapp-klugg7uj4px3q-sql"]
            DB["💾 Azure SQL Database<br/>ClipSnipDb"]
        end

    end

    User -->|"HTTPS"| App

    Plan -.->|"Hosts / provides compute"| App

    App -->|"Application data"| DB
    DB -->|"Hosted on"| SQLServer

    App -->|"Retrieve secrets / configuration"| KV

    MI -.->|"Authenticated access"| KV
    MI -.->|"Deployment / resource access"| App
```

### Azure Resources

| Resource | Purpose |
|---|---|
| **Azure App Service** | Hosts the ASP.NET Core MVC application |
| **App Service Plan** | Provides compute resources for the App Service |
| **Azure SQL Server** | Logical SQL server hosting the application database |
| **ClipSnipDb** | Main production database |
| **Azure Key Vault** | Stores sensitive configuration and secrets |
| **Managed Identity** | Provides identity-based authentication to Azure resources |

---

## Technology Stack

- ASP.NET Core MVC
- .NET 10
- Entity Framework Core
- ASP.NET Core Identity
- Azure App Service
- Azure SQL Database
- Azure Key Vault
- Azure Managed Identity
- Azure DevOps
- Bicep
- Google MediaPipe
