param location string = resourceGroup().location
param appName string

var uniqueAppName = '${toLower(appName)}-${uniqueString(resourceGroup().id)}'
var appServicePlanName = '${uniqueAppName}-plan'

resource appServicePlan 'Microsoft.Web/serverfarms@2024-04-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: 'B1'
    tier: 'Basic'
  }
  kind: 'linux'
  properties: {
    reserved: true
  }
}

resource webApp 'Microsoft.Web/sites@2024-04-01' = {
  name: uniqueAppName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      linuxFxVersion: 'DOTNETCORE|10.0'
      alwaysOn: true
    }
  }
}

output appServiceName string = webApp.name
output appServiceUrl string = 'https://${webApp.properties.defaultHostName}'