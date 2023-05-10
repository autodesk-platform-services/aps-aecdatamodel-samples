## Window Schedule Sample Workflow

A schedule is typically used to identify, in most cases a chart or table providing descriptions of the windows , doors, finishes, lintels, footings, piers, etc. on a construction project. Let's take the same sample model and imagine that you would like to build a window schedule. You will need to retrieve properties like panel glazing, glass, frame material, height, width, etc. of all design entity instances of category windows.

To run the sample, please review [setup](./README.md#SETUP) instructions.

## Step 1: List all hubs

After login (top-right), click on `List Hubs` and take note of the hubId (`id`). [See C# code](/Controllers/HubsProjects.cs). 

![Step 1](./images/hubs.png)

## Step 2: List all projects

Use the `HubId` from step 1 to list all projects and take note of the projectId (`id`). [See C# code](/Controllers/HubsProjects.cs). 

![Step 2](./images/projects.png)

## Step 3: List all designs in a project

This step uses `projectId`. Take note of the `designId` of the desired file (in this image, `House.rvt`). [See C# code](/Controllers/Designs.cs). 

![Step 3](./images/designs.png)

## Step 4: Generate Schedule

Use the `designId` from step 3. Click on generate schedule. You may adjust he `filter` field. [See C# code](/Controllers/Schedule.cs). 

![Step 3](./images/schedule.png)

Query used:

```
elements (designId: "your design id",filter: { query: "property.name.category==Windows and 'property.name.Element Context'==Instance"}){
  pagination{
    pageSize
    cursor
  }
  results{
    id
    name
    properties{
      results{
        name
        value
        displayValue
        propertyDefinition{
          units
        }
      }
    }
    references{
      results{
        name
        value{
          properties{
            results{
              name
              value
              displayValue
              propertyDefinition{
                units
              }
            }
          }
        }
      }
    }
  }
}
```

