using System;
using System.Collections.Generic;
using Autodesk.Authentication;
using Autodesk.Authentication.Model;
using Autodesk.SDKManager;

public class Tokens
{
    public string InternalToken;
    public string PublicToken;
    public string RefreshToken;
    public DateTime ExpiresAt;
}

public partial class APS
{
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _callbackUri;
    private readonly AuthenticationClient _authClient;
    private readonly List<Scopes> InternalTokenScopes = new List<Scopes> { Scopes.DataRead, Scopes.ViewablesRead };
    private readonly List<Scopes> PublicTokenScopes = new List<Scopes> { Scopes.DataRead, Scopes.ViewablesRead };

    public APS(string clientId, string clientSecret, string callbackUri)
    {
        _clientId = clientId;
        _clientSecret = clientSecret;
        _callbackUri = callbackUri;
        SDKManager sdkManager = SdkManagerBuilder
        .Create() // Creates SDK Manager Builder itself.
        .Build();
        _authClient = new AuthenticationClient(sdkManager);
    }
}
