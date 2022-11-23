function registerOnClick(id, cb) {
    if (document.getElementById(id)) document.getElementById(id).onclick = cb;
}

registerOnClick('getHubs', async () => {
    let hubs = await query('/api/graphql/hubs');
    writeResponse(hubs)
});

registerOnClick('getProjects', async () => {
    let hubid = document.getElementById('hubId').value;
    if (hubid === '') { writeResponse('Please provide the HubId'); return; }
    let projects = await query('/api/graphql/hubs/' + hubid + '/projects');
    writeResponse(projects)
});

registerOnClick('getDesigns', async () => {
    let hubid = document.getElementById('hubId').value;
    let projectId = document.getElementById('projectId').value;
    if (hubid === '') { writeResponse('Please provide the HubId'); return; }
    if (projectId === '') { writeResponse('Please provide the ProjectId'); return; }
    let designs = await query('/api/graphql/hubs/' + hubid + '/projects/' + projectId + '/designs');
    writeResponse(designs)
});

// Sample 1 Design validation
registerOnClick('getPropeties', async () => {
    let hubid = document.getElementById('hubId').value;
    let projectId = document.getElementById('projectId').value;
    if (hubid === '') { writeResponse('Please provide the HubId'); return; }
    if (projectId === '') { writeResponse('Please provide the ProjectId'); return; }
    let properties = await query('/api/graphql/hubs/' + hubid + '/projects/' + projectId + '/properties');
    writeResponse(properties)
});

// Sample 2 Quantity Take off
registerOnClick('getTakeOff', async () => {
    let designId = document.getElementById('designId').value;
    let category = document.getElementById('category').value;
    if (designId === '') { writeResponse('Please provide the HubId'); return; }
    if (category === '') { writeResponse('Please provide the ProjectId'); return; }
    let designEntities = await query('/api/graphql/designs/' + designId + '/takeoff/' + category);
    writeResponse(designEntities)
});

// Sample 3 Schedule
registerOnClick('getSchedule', async () => {
    let designId = document.getElementById('designId').value;
    let category = document.getElementById('category').value;
    if (designId === '') { writeResponse('Please provide the HubId'); return; }
    if (category === '') { writeResponse('Please provide the ProjectId'); return; }
    let designEntities = await query('/api/graphql/designs/' + designId + '/schedule/' + category);
    writeResponse(designEntities)
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