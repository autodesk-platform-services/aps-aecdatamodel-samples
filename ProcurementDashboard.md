## Furniture Procurement Dashboard Sample Workflow

Obtaining furniture procurement data can help building managers make more informed decisions about budgeting, maintenance, compliance, and planning, which can ultimately lead to a more efficient and effective use of resources.

To run the sample, please review [setup](./README.md#SETUP) instructions.

## Step 1: List all hubs

After login (top-right), click on `List Hubs` and take note of the hubId (`id`). [See C# code](/Controllers/HubsProjects.cs).
In case your hub is not in the first response and you receive a cursor value different that `null`, you can copy and paste this value inside the cursor input and click List Hubs button once more.

![Step 1](./images/hubs.png)

## Step 2: List all projects

Use the `HubId` from step 1 to list all projects and take note of the projectId (`id`). [See C# code](/Controllers/HubsProjects.cs).
In case your project is not in the first response and you receive a cursor value different that `null`, you can copy and paste this value inside the cursor input and click List Projects button once more.

![Step 2](./images/projects.png)

## Step 3: List all designs in a project

This step uses `projectId`. Take note of the `designId` of the desired file (in this image, `House.rvt`). [See C# code](/Controllers/Designs.cs).
In case your design is not in the first response and you receive a cursor value different that `null`, you can copy and paste this value inside the cursor input and click List all designs button once more.

![Step 3](./images/designs.png)

## Step 4: Generate furniture procurement data in a specific level

Use the `designId` from step 3. Click on generate schedule. You may adjust he `filter` field. [See C# code](/Controllers/Schedule.cs).

![Step 3](./images/furnitureprocurement.png)

Query used:

```
elements(designId: "your design id", filter: { query: "property.name.category=contains='Level' and 'property.name.Element Name'=contains='L5'"}) {
	pagination {
		pageSize
		cursor
	}
	results {
		id
		name
		properties {
			results {
				name
				value
			}
		}
		referencedBy (name: ""Level"", filter: { query: "property.name.category==Furniture"}) {
			results {
				name
				properties {
					results {
						name
						value
					}
				}
			}
		}
	}
}
```
