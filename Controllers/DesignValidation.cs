using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;

public partial class AECCIMGraphQLController : ControllerBase
{
    // Refer to AECCIMGraphQL class for other methods

    [HttpGet("hubs/{hubid}/projects/{projectId}/properties")]
    public async Task<ActionResult<string>> GetProperties(string hubId, string projectId)
    {
        var properties = new GraphQLRequest
        {
            Query = @"
			    query {
                    designs(filter: { hubId: ""$hubId"", projectId: ""$projectId""}) {
                        results {
                            id
                            propertyDefinitions {
                                results {
                                    type
                                    name
                                    description
                                    specification
                                    units
                                }
                            }
                        }
                    }
                }".Replace("$hubId", hubId).Replace("$projectId", projectId),
        };

        return await Query(properties);
    }

}