using System.Threading.Tasks;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GraphQL;

[ApiController]
[Route("api/graphql")]
public partial class AECCIMGraphQLController : ControllerBase
{
    private const string BASE_URL = "https://developer.api.autodesk.com/aeccloudinformationmodel/2022-11/graphql";
    private static GraphQLHttpClient GraphQLClient;
    private readonly ILogger<AuthController> _logger;
    private readonly APS _aps;

    public AECCIMGraphQLController(ILogger<AuthController> logger, APS aps)
    {
        _logger = logger;
        _aps = aps;

        if (GraphQLClient == null) GraphQLClient = new GraphQLHttpClient(BASE_URL, new NewtonsoftJsonSerializer());
    }

    public async Task<ActionResult<string>> Query(GraphQLRequest query)
    {
        var tokens = await AuthController.PrepareTokens(Request, Response, _aps);
        if (tokens == null)
        {
            return Unauthorized();
        }

        var client = new GraphQLHttpClient(BASE_URL, new NewtonsoftJsonSerializer());
        client.HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokens.InternalToken);
        var response = await client.SendQueryAsync<object>(query);

        return Ok(response.Data.ToString());
    }
}
