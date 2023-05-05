## Compare Designs Sample Workflow

Comparing designs can help architects and designers create more effective and functional buildings that meet the needs of their clients and occupants. By evaluating different options and approaches, they can find innovative solutions and create unique and inspiring spaces.

To run the sample, please review [setup](./README.md#SETUP) instructions.

## Step 1: List all hubs

After login (top-right), click on `List Hubs` and take note of the hubId (`id`). [See C# code](/Controllers/HubsProjects.cs). 

![Step 1](./images/hubs.png)

## Step 2: List all projects

Use the `HubId` from step 1 to list all projects and take note of the projectId (`id`). [See C# code](/Controllers/HubsProjects.cs). 

![Step 2](./images/projects.png)

## Step 3: List all designs in a project

This step uses `hubId` and `projectId`. Take note of the `designId` of the desired file (in this image, `House.rvt`). [See C# code](/Controllers/Designs.cs). 

![Step 3](./images/designs.png)

## Step 4: Generate versions elements

Use the `designId` from step 3. Click on generate schedule. You may adjust he `version` field. [See C# code](/Controllers/Schedule.cs). 

![Step 3](./images/comparedesigns.png)

Query used:

```
{
  aecDesignByVersionNumber(designId: "fbb418e4-d663-36fe-bf87-7c555acc4983" , versionNumber: 4 ){
		name
		elements{
			results{
				id
				name
				properties{
					results{
						name
						value
						propertyDefinition{
							id
							name
							units
						}
					}
				}
			}
		}
	}
}
```

