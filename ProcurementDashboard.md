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

## Step 3: List all elementgroups in a project

This step uses `projectId`. Take note of the `elementGroupId` of the desired file (in this image, `House.rvt`). [See C# code](/Controllers/ElementGroups.cs).
In case your elementgroup is not in the first response and you receive a cursor value different that `null`, you can copy and paste this value inside the cursor input and click List all elementgroups button once more.

![Step 3](./images/elementgroups.png)

## Step 4: Generate furniture procurement data in a specific level

Use the `elementGroupId` from step 3. Click on generate schedule. You may adjust he `filter` field. [See C# code](/Controllers/Schedule.cs).
In case your element is not in the first response and you receive a cursor value different that `null`, you can copy and paste this value inside the cursor input and click Generate Procurement button once more.

![Step 3](./images/furnitureprocurement.png)

Query used:

```
query GetFurnitureProcurement($elementGroupId: ID!, $elementsfilter: String!, $referencefilter: String!) {
	elements(elementGroupId: $elementGroupId, filter: { query: $elementsfilter}) {
		pagination {
			pageSize
			cursor
		}
		results {
			id
			name
			properties (filter: {names: ["Element Name"]}){
				results {
					name
					value
				}
			}
			referencedBy (name: "Level", filter: { query: $referencefilter}) {
				results {
					name
					properties (filter: {names: [""Family Name"", ""Element Name""]}) {
						results {
							name
							value
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
query GetFurnitureProcurement($elementGroupId: ID!, $elementsfilter: String!, $referencefilter: String!) {
	elements(elementGroupId: $elementGroupId, filter: { query: $elementsfilter}, pagination:{cursor:"cursor string here"}) {
		pagination {
			pageSize
			cursor
		}
		results {
			id
			name
			properties (filter: {names: ["Element Name"]}){
				results {
					name
					value
				}
			}
			referencedBy (name: "Level", filter: { query: $referencefilter}) {
				results {
					name
					properties (filter: {names: [""Family Name"", ""Element Name""]}) {
						results {
							name
							value
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
	elementGroupId = "Your elementGroup ID",
	elementsfilter = elementsfilter,
	referencefilter = referencefilter
}
```
