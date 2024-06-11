using System;
using System.Threading.Tasks;
using Autodesk.Authentication.Model;

public partial class APS
{
	public string GetAuthorizationURL()
	{
		return _authClient.Authorize(_clientId, ResponseType.Code, _callbackUri, InternalTokenScopes);
	}

	public async Task<Tokens> GenerateTokens(string code)
	{
		ThreeLeggedToken internalAuth = await _authClient.GetThreeLeggedTokenAsync(_clientId, _clientSecret, code, _callbackUri);
		RefreshToken publicAuth = await _authClient.GetRefreshTokenAsync(_clientId, _clientSecret, internalAuth.RefreshToken, PublicTokenScopes);
		return new Tokens
		{
			PublicToken = publicAuth.AccessToken,
			InternalToken = internalAuth.AccessToken,
			RefreshToken = publicAuth._RefreshToken,
			ExpiresAt = DateTime.Now.ToUniversalTime().AddSeconds((double)internalAuth.ExpiresIn)
		};
	}

	public async Task<Tokens> RefreshTokens(Tokens tokens)
	{
		RefreshToken internalAuth = await _authClient.GetRefreshTokenAsync(_clientId, _clientSecret, tokens.RefreshToken, InternalTokenScopes);
		RefreshToken publicAuth = await _authClient.GetRefreshTokenAsync(_clientId, _clientSecret, internalAuth._RefreshToken, PublicTokenScopes);
		return new Tokens
		{
			PublicToken = publicAuth.AccessToken,
			InternalToken = internalAuth.AccessToken,
			RefreshToken = publicAuth._RefreshToken,
			ExpiresAt = DateTime.Now.ToUniversalTime().AddSeconds((double)internalAuth.ExpiresIn).AddSeconds(-1700)
		};
	}

	public async Task<dynamic> GetUserProfile(Tokens tokens)
	{
		var userInfo = await _authClient.GetUserInfoAsync(tokens.InternalToken);
		return userInfo;
	}
}
