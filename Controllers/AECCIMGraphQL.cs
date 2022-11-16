using System;
using System.Threading.Tasks;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using GraphQL;
using Newtonsoft.Json.Linq;

[ApiController]
[Route("api/graphql")]
public class AECCIMGraphQL : ControllerBase
{
    private const string BASE_URL = "https://developer.api.autodesk.com/aeccloudinformationmodel/2022-11/graphql/v1/graph";
    private static GraphQLHttpClient GraphQLClient;
    private readonly ILogger<AuthController> _logger;
    private readonly APS _aps;

    public AECCIMGraphQL(ILogger<AuthController> logger, APS aps)
    {
        _logger = logger;
        _aps = aps;

        if (GraphQLClient == null) GraphQLClient = new GraphQLHttpClient(BASE_URL, new NewtonsoftJsonSerializer());
    }

    [HttpGet("hubs")]
    public async Task<ActionResult<string>> GetHubs()
    {
        var hubs = new GraphQLRequest
        {
            Query = @"
			    query GetHubs {
			        hubs {
                        results {
                            id
                            name
                        }
			        }
			    }",
        };

        return await Query(hubs);
    }

    [HttpGet("hubs/{hubid}/projects")]
    public async Task<ActionResult<string>> GetProjects(string hubId)
    {
        var hubs = new GraphQLRequest
        {
            Query = @"
			    query GetProjects {
			        projects (hubId: ""$hubId"") {
                        results {
                            id
                            name
                        }
			        }
			    }".Replace("$hubId", hubId),
        };

        return await Query(hubs);
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
