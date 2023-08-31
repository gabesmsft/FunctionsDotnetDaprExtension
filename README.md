[![Deploy To Azure](https://raw.githubusercontent.com/Azure/azure-quickstart-templates/master/1-CONTRIBUTION-GUIDE/images/deploytoazure.svg?sanitize=true)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fgabesmsft%2FFunctionsDotnetDaprExtension%2Fmaster%2Fdeploy%2Fazuredeploy.json)  [![Visualize](https://raw.githubusercontent.com/Azure/azure-quickstart-templates/master/1-CONTRIBUTION-GUIDE/images/visualizebutton.svg?sanitize=true)](http://armviz.io/#/?load=https%3A%2F%2Fraw.githubusercontent.com%2Fgabesmsft%2FFunctionsDotnetDaprExtension%2Fmaster%2Fdeploy%2Fazuredeploy.json)

This sample Azure Resource Manager template deploys a Function App on Container Apps, a Service Bus Topic, and a PubSub Dapr component.

This application is only for demonstration purposes and is not intended as a production application or as official instructions. Use discretion and best practices in your usage of this application and template.

If you want to first test the local Functions project against the Service Bus topic, set the deployFunctionApp parameter to false.

## Prerequisites

- Deploy a Container App Environment to one of the regions listed [here](https://learn.microsoft.com/azure/azure-functions/functions-container-apps-hosting#considerations-for-container-apps-hosting). For now, it cannot be a workload profile Environment. You can use [this template](https://github.com/azureossd/Container-Apps/tree/master/ContainerAppEnvironment/deploy) to deploy a Container App Environment, but be sure to set the workloadProfileEnabled parameter to false.

## Functions

| Function name | Dapr component type(s) | HTTP method (if applicable) | HTTP route (if applicable) | Description |
| ------------- | ---------------------- | --------------------------- | -------------------------- | ----------- |
| HttpTriggerWithDaprPubSubExtensionOutput | Pubsub output | POST | /api/mypubsubpublisher | Publishes a message to the pubsub |
| DaprPubSubExtensionTrigger | Pubsub trigger | | | Is expected to trigger when a message is published to the pubsub |

## Local testing

If you want to test against the local Redis server provided by Dapr, leave the component files as-is.

If you want to test against a Service Bus Topic, do the following:
1. Deploy the template with the deployFunctionApp parameter set to false. Note: You can have multipe consumers, but this step is just for extra isolation in case it does not trigger as expected.
2. In the components folder, rename the pubsubredislocal.yaml to pubsubredislocal.yaml_
3. In the components folder, rename the pubsubservicebustopic.yaml_ to pubsubservicebustopic.yaml
4. In the pubsubservicebustopic.yaml file, replace the connection string value, using the ServiceBus namespace, Shared access key name, Shared access key, and topic name of your Service Bus topic.

To run the application locally. Change to the *project* directory (not solution directory), and then run the following command:
```
dapr run --app-id functionapp1 --dapr-http-port 3500 --app-port 3001 --components-path ./components -- func host start 
```

To publish a message to the pubsub, send a post request to http://localhost:7071/api/mypubsubpublisher with some json data. E.g.:

```
curl -H "Content-Type: application/json" -X POST http://localhost:7071/api/mypubsubpublisher -d "{"hello": "world"}"
```

If everything is configured correctly, the Dapr logs should show that the DaprPubSubExtensionTrigger executed.

## Azure Function App testing
1. Deploy the template with the deployFunctionApp parameter set to true.
2. In the portal, copy the URL of the **HttpTriggerWithDaprPubSubExtensionOutput** Function.
3. Send a post request with some json data to the HttpTriggerWithDaprPubSubExtensionOutput Function. E.g.:

```
curl -H "Content-Type: application/json" -X POST https://<FunctionUrl> -d "{"hello": "world"}"
```