function registerOnClick(id, cb) {
    if (document.getElementById(id)) document.getElementById(id).onclick = cb;
}

registerOnClick('getHubs', async () => {
    let cursor = document.getElementById('hubsCursor').value;
    let hubs = await query(`/api/graphql/hubs?cursor=${cursor}`);
    writeResponse(hubs)
});

registerOnClick('getProjects', async () => {
    let cursor = document.getElementById('projectsCursor').value;
    let hubid = document.getElementById('hubId').value;
    if (hubid === '') { writeResponse('Please provide the HubId'); return; }
    let projects = await query(`/api/graphql/hubs/${hubid}/projects?cursor=${cursor}`);
    writeResponse(projects)
});

registerOnClick('getDesigns', async () => {
    let cursor = document.getElementById('designsCursor').value;
    let hubid = document.getElementById('hubId').value;
    let projectId = document.getElementById('projectId').value;
    if (hubid === '') { writeResponse('Please provide the HubId'); return; }
    if (projectId === '') { writeResponse('Please provide the ProjectId'); return; }
    let designs = await query(`/api/graphql/projects/${projectId}/designs?=cursor${cursor}`);
    writeResponse(designs)
});

// Sample 1 Design validation
registerOnClick('getPropeties', async () => {
    let cursor = document.getElementById('propertiesCursor').value;
    let projectId = document.getElementById('projectId').value;
    if (projectId === '') { writeResponse('Please provide the ProjectId'); return; }
    let properties = await query(`/api/graphql/projects/${projectId}/properties?cursor=${cursor}`);
    writeResponse(properties)
});

// Sample 2 Quantity Take off
registerOnClick('getTakeOff', async () => {
    let cursor = document.getElementById('takeoffCursor').value;
    let designId = document.getElementById('designId').value;
    let elementsfilter = document.getElementById('elementsfilter').value;
    if (designId === '') { writeResponse('Please provide the designId'); return; }
    if (elementsfilter === '') { writeResponse('Please provide the elements filter'); return; }
    let elements = await query(`/api/graphql/designs/${designId}/takeoff/${elementsfilter}?cursor=${cursor}`);
    writeResponse(elements);
});

// Sample 3 Schedule
registerOnClick('getSchedule', async () => {
    let cursor = document.getElementById('scheduleCursor').value;
    let designId = document.getElementById('designId').value;
    let elementsfilter = document.getElementById('elementsFilter').value;
    if (designId === '') { writeResponse('Please provide the HubId'); return; }
    if (elementsfilter === '') { writeResponse('Please provide the elements filter'); return; }
    let elements = await query(`/api/graphql/designs/${designId}/schedule/${elementsfilter}?cursor=${cursor}`);
    writeResponse(elements);
});

// Sample 4 Procurement
registerOnClick('getProcurement', async () => {
    let cursor = document.getElementById('procurementCursor').value;
    let designId = document.getElementById('designId').value;
    let referencefilter = document.getElementById('referencefilter').value;
    let elementsfilter = document.getElementById('elementsfilter').value;
    if (designId === '') { writeResponse('Please provide the designId'); return; }
    if (referencefilter === '') { writeResponse('Please provide the reference filter'); return; }
    if (elementsfilter === '') { writeResponse('Please provide the elements filter'); return; }
    let elements = await query(`/api/graphql/designs/${designId}/procurement?elementsfilter=${elementsfilter}'&referencefilter=${referencefilter}&cursor=${cursor}`);
    writeResponse(elements);
});

// Sample 5 Schedule
registerOnClick('getComparision', async () => {
    let designId = document.getElementById('designId').value;
    let versionone = document.getElementById('versionone').value;
    let cursorOne = document.getElementById('compareCursorOne').value;
    let versiontwo = document.getElementById('versiontwo').value;
    let cursorTwo = document.getElementById('compareCursorTwo').value;
    if (designId === '') { writeResponse('Please provide the HubId'); return; }
    if (versionone === '') { writeResponse('Please provide the reference version'); return; }
    if (versiontwo === '') { writeResponse('Please provide the version to compare'); return; }
    let elementsv1 = await query(`/api/graphql/designs/${designId}/version/${versionone}?cursor=${cursorOne}`);
    let elementsv2 = await query(`/api/graphql/designs/${designId}/version/${versiontwo}?cursor=${cursorTwo}`);
    writeResponse(elementsv1);
    writeResponse2(elementsv2);
});

async function query(url) {
    writeResponse('Processing...');
    const resp = await fetch(url);
    if (resp.ok) {
        return await resp.json();
    }
    else if (resp.status == 401) {
        return resp.statusText + ' - Please login first';
    }
    else if (resp.status == 400)
        return resp.text();
    else
        return resp.statusText;
}

function writeResponse(response) {
    document.getElementById('jsonresult').innerText = JSON.stringify(response, null, 4)
}

function writeResponse2(response) {
    document.getElementById('jsonresult2').innerText = JSON.stringify(response, null, 4)
}