param location string = resourceGroup().location
param appName string

@secure()
param sqlAdminPassword string

var uniqueAppName = '${toLower(appName)}-${uniqueString(resourceGroup().id)}'
var appServicePlanName = '${uniqueAppName}-plan'

var sqlServerName = '${uniqueAppName}-sql'
var databaseName = 'ClipSnipDb'
var sqlAdminUsername = 'clipsnipadmin'

/// ------------------------------
/// App Service
/// ------------------------------

resource appServicePlan 'Microsoft.Web/serverfarms@2024-04-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: 'F1'
    tier: 'Basic'
  }
  kind: 'linux'
  properties: {
    reserved: true
  }
}

/// ------------------------------
/// SQL Server
/// ------------------------------

resource sqlServer 'Microsoft.Sql/servers@2023-08-01' = {
    name: sqlServerName
    location: location
    properties: {
        administratorLogin: sqlAdminUsername
        administratorLoginPassword: sqlAdminPassword
        minimalTlsVersion: '1.2'
        publicNetworkAccess: 'Enabled'
    }
}

/// ------------------------------
/// SQL Database
/// ------------------------------

resource sqlDatabase 'Microsoft.Sql/servers/databases@2023-08-01' = {
  parent: sqlServer
  name: databaseName
  location: location

  sku: {
    name: 'GP_S_Gen5_2'
    tier: 'GeneralPurpose'
    family: 'Gen5'
    capacity: 2
  }

  properties: {
    useFreeLimit: true
    freeLimitExhaustionBehavior: 'AutoPause'

    minCapacity: json('0.5')
    autoPauseDelay: 60

    maxSizeBytes: 34359738368 // 32 GiB
    requestedBackupStorageRedundancy: 'Local'
  }
}

resource allowAzureServices 'Microsoft.Sql/servers/firewallRules@2023-08-01' = {
  parent: sqlServer
  name: 'AllowAzureServices'
  properties: {
    startIpAddress: '0.0.0.0'
    endIpAddress: '0.0.0.0'
  }
}

/// ------------------------------
/// Web App
/// ------------------------------

resource webApp 'Microsoft.Web/sites@2024-04-01' = {
  name: uniqueAppName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      linuxFxVersion: 'DOTNETCORE|10.0'
      alwaysOn: false
      appSettings: [
        {
          name: 'ConnectionStrings__DefaultConnection'
          value: 'Server=tcp:${sqlServer.properties.fullyQualifiedDomainName},1433;Initial Catalog=${databaseName};User ID=${sqlAdminUsername};Password=${sqlAdminPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'
        }
      ]
    }
  }
}

output appServiceName string = webApp.name
output appServiceUrl string = 'https://${webApp.properties.defaultHostName}'
output sqlServerHost string = sqlServer.properties.fullyQualifiedDomainName