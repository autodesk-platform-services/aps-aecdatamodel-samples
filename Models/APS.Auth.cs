using System;
using System.Threading.Tasks;
using Autodesk.Forge;

public partial class APS
{
    public string GetAuthorizationURL()
    {
			return new ThreeLeggedApi().Authorize(_clientId, "code", _callbackUri, InternalTokenScopes);
    }

    public async Task<Tokens> GenerateTokens(string code)
    {
			var threeleggedapi = new ThreeLeggedApi();
			dynamic internalAuth = await threeleggedapi.GettokenAsync(_clientId, _clientSecret, "authorization_code", code, _callbackUri);
			dynamic publicAuth = await threeleggedapi.RefreshtokenAsync(_clientId, _clientSecret, "refresh_token", internalAuth.refresh_token, PublicTokenScopes);
			return new Tokens
		{
			PublicToken = publicAuth.access_token,
			InternalToken = internalAuth.access_token,
			RefreshToken = publicAuth.refresh_token,
			ExpiresAt = DateTime.Now.ToUniversalTime().AddSeconds(internalAuth.expires_in)
		};
    }

    public async Task<Tokens> RefreshTokens(Tokens tokens)
    {
			var threeleggedapi = new ThreeLeggedApi();
			dynamic internalAuth = await threeleggedapi.RefreshtokenAsync(_clientId, _clientSecret, "refresh_token", tokens.RefreshToken, InternalTokenScopes);
			dynamic publicAuth = await threeleggedapi.RefreshtokenAsync(_clientId, _clientSecret, "refresh_token", internalAuth.refresh_token, PublicTokenScopes);
			return new Tokens
		{
			PublicToken = publicAuth.access_token,
			InternalToken = internalAuth.access_token,
			RefreshToken = publicAuth.refresh_token,
			ExpiresAt = DateTime.Now.ToUniversalTime().AddSeconds(internalAuth.expires_in).AddSeconds(-1700)
		};
    }

    public async Task<dynamic> GetUserProfile(Tokens tokens)
    {
        var api = new UserProfileApi();
        api.Configuration.AccessToken = tokens.InternalToken;
        dynamic profile = await api.GetUserProfileAsync();
        return profile;
    }
}
