using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;

public partial class AECCIMGraphQLController : ControllerBase
{
    [HttpGet("hubs")]
    public async Task<ActionResult<string>> GetHubs()
    {
        var hubs = new GraphQLRequest
        {
            Query = @"
			    query {
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
			    query GetProjects ($hubId: String!) {
			        projects (hubId: $hubId) {
                        results {
                            id
                            name
                        }
			        }
			    }", 
                Variables = new {
                    hubId = hubId
                }
        };

        return await Query(hubs);
    }
}