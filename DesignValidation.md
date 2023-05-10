## Design Validation Sample Workflow

Validating data property names, units and types ( used across designs in a project is a valuable QA/QC process that can now be automated using the AEC CIM APIs.

To run the sample, please review [setup](./README.md#SETUP) instructions.

## Step 1: List all hubs

After login (top-right), click on `List Hubs` and take note of the hubId (`id`). [See C# code](/Controllers/HubsProjects.cs). 

![Step 1](./images/hubs.png)

## Step 2: List all projects

Use the `HubId` from step 1 to list all projects and take note of the projectId (`id`). [See C# code](/Controllers/HubsProjects.cs). 

![Step 2](./images/projects.png)

## Step 3: List all properties

This step uses `projectId`. Click on List all properties. [See C# code](/Controllers/DesignValidation.cs). 

![Step 3](./images/allproperties.png)

Query used:

```
aecDesignsByProject(projectId: $projectId) {
  pagination{
    pageSize
    cursor
  }
  results{
    name
    id
    propertyDefinitions{
      results{
        id
        name
        description
        specification
      }
    }
  }
}
``` 