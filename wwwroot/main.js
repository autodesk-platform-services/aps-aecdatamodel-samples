const signin = document.getElementById('signin');
try {
    const resp = await fetch('/api/auth/profile');
    if (resp.ok) {
        const user = await resp.json();
        signin.innerText = `Sign out (${user.name})`;
        signin.onclick = () => {
            const iframe = document.createElement('iframe');
            iframe.style.visibility = 'hidden';
            iframe.src = 'https://accounts.autodesk.com/Authentication/LogOut';
            document.body.appendChild(iframe);
            iframe.onload = () => {
                window.location.replace('/api/auth/signout');
                document.body.removeChild(iframe);
            };
        }
    } else {
        signin.innerText = 'Sign in';
        signin.onclick = () => window.location.replace('/api/auth/signin');
    }
    signin.style.visibility = 'visible';
} catch (err) {
    alert('Could not initialize the application. See console for more details.');
    console.error(err);
}