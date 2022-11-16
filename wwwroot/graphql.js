document.getElementById('getHubs').onclick = async () => {
    let hubs = await query('/api/graphql/hubs');
    document.getElementById('jsonresult').innerText = JSON.stringify(hubs, null, 4)
};

document.getElementById('getProjects').onclick = async () => {
    let hubid = document.getElementById('hubId').value;
    if (hubid === '') { alert('Please "List Hubs" and provide a HubId'); return; }
    let hubs = await query('/api/graphql/hubs/' + hubid + '/projects');
    document.getElementById('jsonresult').innerText = JSON.stringify(hubs, null, 4)
};

async function query(url) {
    const resp = await fetch(url);
    if (resp.ok) {
        return await resp.json();
    }
}