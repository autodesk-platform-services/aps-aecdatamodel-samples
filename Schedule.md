## Window Schedule Sample Workflow

A schedule is typically used to identify, in most cases a chart or table providing descriptions of the windows , doors, finishes, lintels, footings, piers, etc. on a construction project. Let's take the same sample model and imagine that you would like to build a window schedule. You will need to retrieve properties like panel glazing, glass, frame material, height, width, etc. of all design entity instances of category windows.

To run the sample, please review [setup](./README.md#SETUP) instructions.

## Step 1: List all hubs

After login (top-right), click on `List Hubs` and take note of the hubId (`id`). [See C# code](/Controllers/HubsProjects.cs).
In case your hub is not in the first response and you receive a cursor value different that `null`, you can copy and paste this value inside the cursor input and click List Hubs button once more.

![Step 1](./images/hubs.png)

## Step 2: List all projects

Use the `HubId` from step 1 to list all projects and take note of the projectId (`id`). [See C# code](/Controllers/HubsProjects.cs).
In case your project is not in the first response and you receive a cursor value different that `null`, you can copy and paste this value inside the cursor input and click List Projects button once more.

![Step 2](./images/projects.png)

## Step 3: List all elementgroups in a project

This step uses `projectId`. Take note of the `elementGroupId` of the desired file (in this image, `House.rvt`). [See C# code](/Controllers/ElementGroups.cs).
In case your elementgroup is not in the first response and you receive a cursor value different that `null`, you can copy and paste this value inside the cursor input and click List all elementgroups button once more.

![Step 3](./images/designs.png)

## Step 4: Generate Schedule

Use the `elementGroupId` from step 3. Click on generate schedule. You may adjust he `filter` field. [See C# code](/Controllers/Schedule.cs).
In case your element is not in the first response and you receive a cursor value different that `null`, you can copy and paste this value inside the cursor input and click Generate scedule button once more.

![Step 3](./images/schedule.png)

Query used:

```
query GetSchedule($elementGroupId: ID!, $elementsfilter: String!){
  elementsByElementGroup(elementGroupId: $elementGroupId,filter: { query: $elementsfilter}){
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
}
```

Query used in case a valid cursor is provided:

```
query GetSchedule($elementGroupId: ID!, $elementsfilter: String!){
  elementsByElementGroup(elementGroupId: $elementGroupId,filter: { query: $elementsfilter}, pagination:{cursor:"cursor"}){
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
          definition{
            units{
              name
            }
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
                definition{
                  units{
                    name
                  }
                }
              }
            }
          }
        }
      }
    }
  }
}
```

The variables are the same in both cases:

```
{
  elementGroupId = elementGroupId,
  elementsfilter = elementsfilter
}
```
