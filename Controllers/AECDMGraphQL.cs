using System.Threading.Tasks;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GraphQL;
using System;

[ApiController]
[Route("api/graphql")]
public partial class AECDMGraphQLController : ControllerBase
{
    private const string BASE_URL = "https://developer.api.autodesk.com/aec/graphql";
    private static GraphQLHttpClient GraphQLClient;
    private readonly ILogger<AuthController> _logger;
    private readonly APS _aps;

    public AECDMGraphQLController(ILogger<AuthController> logger, APS aps)
    {
        _logger = logger;
        _aps = aps;

        if (GraphQLClient == null) GraphQLClient = new GraphQLHttpClient(BASE_URL, new NewtonsoftJsonSerializer());
    }

    public async Task<ActionResult<string>> Query(GraphQLRequest query, string? regionHeader)
    {
        var tokens = await AuthController.PrepareTokens(Request, Response, _aps);
        if (tokens == null)
        {
            return Unauthorized();
        }

        var client = new GraphQLHttpClient(BASE_URL, new NewtonsoftJsonSerializer());
        client.HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokens.InternalToken);
        if(!String.IsNullOrWhiteSpace(regionHeader))
            client.HttpClient.DefaultRequestHeaders.Add("region", regionHeader);
        var response = await client.SendQueryAsync<object>(query);

        if (response.Data == null) return BadRequest(response.Errors[0].Message);
        return Ok(response.Data.ToString());
    }
}
