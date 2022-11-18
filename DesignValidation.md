# AEC CIM Samples in .NET

Review [readme](./README.md#SETUP) for setup instructions.

## Design Validation Sample Workflow

Validating data property names, units and types ( used across designs in a project is a valuable QA/QC process that can now be automated using the AEC CIM APIs.

### Step 1: List all hubs

After login (top-right), click on `List Hubs` and take note of the hubId (`id`).

![Step 1](./images/hubs.png)

### Step 2: List all projects

Use the `HubId` from step 1 to list all projects and take note of the projectId (`id`).

![Step 2](./images/projects.png)

### Step 3: List all properties

This step uses `hubId` and `projectId`. Click on List all properties.  

![Step 3](./images/allproperties.png)